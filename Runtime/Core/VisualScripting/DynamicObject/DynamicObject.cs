using System;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Core.Characters;
using GameFramework;
using UnityEngine.Rendering;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public partial class DynamicObject : MonoMasterService, IReference
    {
        public Args CurrentArgs { get; set; }

        public EntityDynamicObject Entity { get; set; }

        public MainService Main { get; set; }
        CharacterService m_CharacterService;

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


        DynamicObjectValue m_Variables;

        [ShowInInspector]
        public DynamicObjectValue Variables
        {
            get
            {
                if (m_Variables == null)
                {
                    m_Variables = Main.GetOrCreateDynamicObjectValue(RuntimeKey, this);
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
        public DynamicObjectTable.DynamicObjectRow Row { get; set; }

        [ShowInInspector]

        public ExcelTableService TableService { get; set; }


        public string RuntimeKey
        {
            get
            {
                if (Row != null)
                {
                    return Utility.Text.Format("DO_{0}", Row.Id.ToString());
                }
                return null;
            }
        }




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
            m_TriggerEventTable = null;
            m_InstructionTable = null;
            TriggerEventRows.Clear();
            TriggerEvents.Clear();
            SubDynamicObjectDict.Clear();
            LoadingDynamicObject.Clear();
            m_Tracker = null;
            m_CachedTransfrom = null;
            m_SubDynamicObjectInfo = null;
            TriggerEnter3dEvent -= OnTriggerEnter3dEvent;
            TriggerExit3dEvent -= OnTriggerExit3dEvent;
        }




        protected override void DoAwake()
        {
            CurrentArgs = new Args(this);
            CurrentArgs.Main = Main;
            Debug.Log($"Do awake m_Tracker:{m_Tracker}");
            DoAwakeTriggerEvent();
            DoAwakeHotspot();
            DoAwakeSubDynamicObject();
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
                    if (TriggerEvents.ContainsKey(kv.Key))
                    {
                        TriggerEvents[kv.Key].SetEnabled(kv.Value);
                    }
                }

                foreach (var kv in Variables.TriggerIndexDict)
                {
                    if (TriggerEvents.ContainsKey(kv.Key))
                    {
                        TriggerEvents[kv.Key].SetTargetIndex(kv.Value);
                    }
                }
            }
        }

        protected override void DoUpdate()
        {
            base.DoUpdate();
            foreach (var trigger in TriggerEvents)
            {
                trigger.Value.OnUpdate();
            }
            DoUpdateHotspot();
        }

        protected override void DoDisable()
        {

            if (m_Tracker != null)
            {
                Debug.Log($"Try disable ");
                m_Tracker.EventInteract -= OnInteract;
                InteractEvent -= OnInteractEvent;
                GameObject.DestroyImmediate(m_Tracker);
                m_Tracker = null;
                Debug.Log($"Try disable m_Tracker:{m_Tracker}");

            }
            base.DoDisable();
        }


        protected override void DoDestroy()
        {
            base.DoDestroy();
            if (m_Tracker != null)
            {
                m_Tracker.EventInteract -= OnInteract;
                InteractEvent -= OnInteractEvent;
            }
        }

        [Button("Hide")]
        public void Hide()
        {
            DynamicObjectService.Hide(Row.Id);
        }



    }
}