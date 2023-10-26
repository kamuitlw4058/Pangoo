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
            TableService = null;
            m_TriggerEventTable = null;
            m_InstructionTable = null;
            TriggerEventRows.Clear();
            TriggerEvents.Clear();
            m_Tracker = null;
        }




        protected override void DoAwake()
        {
            CurrentArgs = new Args(this);
            CurrentArgs.Main = Main;
            DoAwakeTriggerEvent();
            DoAwakeHotspot();
        }

        protected override void DoStart()
        {
            var dynamicObjectValue = Main.GetDynamicObjectValue(RuntimeKey);
            if (dynamicObjectValue != null)
            {
                var transformValue = dynamicObjectValue.transformValue;
                if (transformValue != null)
                {
                    CachedTransfrom.localPosition = transformValue.Value.Postion;
                    CachedTransfrom.localRotation = Quaternion.Euler(transformValue.Value.Rotation);
                    CachedTransfrom.localScale = transformValue.Value.Scale;
                }

                foreach (var kv in dynamicObjectValue.ChilernTransforms)
                {
                    var childTransform = CachedTransfrom.Find(kv.Key);
                    if (childTransform != null)
                    {
                        childTransform.localPosition = kv.Value.Postion;
                        childTransform.localRotation = Quaternion.Euler(kv.Value.Rotation);
                        childTransform.localScale = kv.Value.Scale;
                    }
                }
            }
        }

        protected override void DoUpdate()
        {
            base.DoUpdate();
            foreach (var trigger in TriggerEvents)
            {
                trigger.OnUpdate();
            }
            DoUpdateHotspot();
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




    }
}