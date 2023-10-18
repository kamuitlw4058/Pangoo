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

        public MainSerice Main { get; set; }
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

        [ShowInInspector]
        public DynamicObjectTable.DynamicObjectRow Row { get; set; }

        [ShowInInspector]

        public ExcelTableService TableService { get; set; }




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