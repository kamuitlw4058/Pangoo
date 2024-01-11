using System;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Core.Characters;
using GameFramework;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using Pangoo.MetaTable;
using System.Linq;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public partial class DynamicObject : MonoMasterService, IReference
    {
        public Args CurrentArgs { get; set; }

        public EntityDynamicObject Entity { get; set; }

        public MainService Main { get; set; }
        CharacterService m_CharacterService;

        RuntimeDataService m_RuntimeData;

        public CharacterService Character
        {
            get
            {
                if (m_CharacterService == null)
                {
                    m_CharacterService = Main?.GetService<CharacterService>();
                }
                return m_CharacterService;
            }
        }

        public RuntimeDataService RuntimeData
        {
            get
            {
                if (m_RuntimeData == null)
                {
                    m_RuntimeData = Main?.GetService<RuntimeDataService>();
                }
                return m_RuntimeData;
            }
        }


        DynamicObjectValue m_Variables;

        [ShowInInspector]
        public DynamicObjectValue Variables
        {
            get
            {
                if (m_Variables == null)
                {
                    m_Variables = Main?.GetOrCreateDynamicObjectValue(RuntimeKey, this);
                }
                return m_Variables;
            }
        }

        DynamicObjectService m_DynamicObjectService;

        public DynamicObjectService DynamicObjectService
        {
            get
            {
                if (m_DynamicObjectService == null)
                {
                    m_DynamicObjectService = Main?.GetService<DynamicObjectService>();
                }
                return m_DynamicObjectService;
            }
        }

        [ShowInInspector]
        public IDynamicObjectRow Row { get; set; }

        [ShowInInspector]

        public ExcelTableService TableService { get; set; }


        public string RuntimeKey
        {
            get
            {
                if (Row != null)
                {
                    return GameFramework.Utility.Text.Format("DO_{0}", Row.Uuid.ToString());
                }
                return null;
            }
        }
        [ShowInInspector]
        public GameObject Model { get; set; }



        public DynamicObject() : base() { }
        public DynamicObject(GameObject go) : base(go)
        {
        }

        public static DynamicObject Create(GameObject go)
        {
            var val = ReferencePool.Acquire<DynamicObject>();
            val.gameObject = go;
            return val;
        }

        public void Clear()
        {
            Row = null;
            IsAwaked = false;
            IsStarted = false;
            TableService = null;
            m_Variables = null;
            m_InstructionHandler = null;
            SubDynamicObjectDict.Clear();
            LoadingDynamicObject.Clear();
            m_Tracker = null;
            m_CachedTransfrom = null;
            m_SubDynamicObjectInfo = null;
            TriggerDict.Clear();

        }


        public IImmersed immersed;

        protected override void DoAwake()
        {
            CurrentArgs = new Args(this);
            CurrentArgs.Main = Main;
            Model = CachedTransfrom.Find("Model")?.gameObject;
            if (Row.DefaultHideModel)
            {
                Model?.SetActive(false);
            }
            else
            {
                Model?.SetActive(true);
            }

            immersed = gameObject.GetComponent<IImmersed>();



            DoAwakeTriggerEvent();
            DoAwakeHotspot();
            DoAwakeSubDynamicObject();
            Debug.Log($"Do awake m_Tracker:{m_Tracker}");
        }

        public void SetModelActive(bool val)
        {
            Model?.SetActive(val);
        }

        public void SetSubGameObjectsActive(string[] paths, bool val)
        {
            if (paths == null) return;

            for (int i = 0; i < paths.Length; i++)
            {
                SetSubGameObjectActive(paths[i], val);
            }
        }

        public Transform GetSubGameObjectTransformPath(string path)
        {
            if (path.IsNullOrWhiteSpace() || path.Equals("Self"))
            {
                return this.Entity.transform;
            }
            return this.CachedTransfrom.Find(path);
        }
        

        public void SetSubGameObjectActive(string path, bool val)
        {
            if (path.IsNullOrWhiteSpace())
            {
                return;
            }

            var childTransform = CachedTransfrom.Find(path);
            if (childTransform != null)
            {
                childTransform.gameObject.SetActive(val);
            }
        }
        
        public void SetModelMaterial(string path,int index)
        {
            if (!this.Entity.GetComponent<MaterialList>())
            {
                Debug.Log($"没有在{Entity.name}身上获取到MaterialList");
                return;
            }

            MaterialList materialList = Entity.GetComponent<MaterialList>();
            Transform target=GetSubGameObjectTransformPath(path);
            if (!target.GetComponent<Renderer>())
            {
                Debug.Log("没有在对象身上获取到Render");
                return;
            }
            Renderer meshRenderer=target.GetComponent<Renderer>();

            if (materialList.materialList!=null)
            {
                meshRenderer.material = materialList.materialList[index];
            }
            else
            {
                Debug.Log("请检查对象材质球列表是否配置");
            }
            
        }

        protected override void DoStart()
        {
            if (Variables != null)
            {
                var transformValue = Variables.transformValue;
                if (transformValue != null)
                {
                    CachedTransfrom.localPosition = transformValue.Value.Postion;
                    CachedTransfrom.localRotation = Quaternion.Euler(transformValue.Value.Rotation);
                    CachedTransfrom.localScale = transformValue.Value.Scale;
                }

                foreach (var kv in Variables.ChilernTransforms)
                {
                    var childTransform = CachedTransfrom.Find(kv.Key);
                    if (childTransform != null)
                    {
                        childTransform.localPosition = kv.Value.Postion;
                        childTransform.localRotation = Quaternion.Euler(kv.Value.Rotation);
                        childTransform.localScale = kv.Value.Scale;
                    }
                }

                foreach (var kv in Variables.TriggerEnabledDict)
                {
                    TriggerSetEnabled(kv.Key, kv.Value);

                }

                foreach (var kv in Variables.TriggerIndexDict)
                {
                    TriggerSetTargetIndex(kv.Key, kv.Value);
                }
            }

            TriggerInovke(TriggerTypeEnum.OnStart);
        }

        protected override void DoUpdate()
        {
            base.DoUpdate();
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                TriggerInovke(TriggerTypeEnum.OnMouseLeft);
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                TriggerInovke(TriggerTypeEnum.OnExit);
            }

            if (Input.GetKey(KeyCode.E))
            {
                TriggerInovke(TriggerTypeEnum.OnButtonE);
            }

            TriggerInovke(TriggerTypeEnum.OnUpdate);


            TriggerUpdate();

            if (immersed != null)
            {
                immersed.OnUpdate();
            }

            DoUpdateHotspot();
        }

        protected override void DoDisable()
        {
            if (m_Tracker != null)
            {
                Debug.Log($"Try disable:{Row.Name}");
                m_Tracker.EventInteract -= OnInteract;
                GameObject.DestroyImmediate(m_Tracker);
                m_Tracker = null;
                Debug.Log($"Try disable m_Tracker:{m_Tracker}");
            }

            DoDisableTimeineSignal();

            m_Variables = null;

            base.DoDisable();
        }


        protected override void DoDestroy()
        {
            base.DoDestroy();
            if (m_Tracker != null)
            {
                m_Tracker.EventInteract -= OnInteract;
            }
        }

        [Button("Hide")]
        public void Hide()
        {
            DynamicObjectService.Hide(Row.Uuid);
        }



    }
}