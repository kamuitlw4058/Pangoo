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

        [ShowInInspector]
        public DynamicObjectTable.DynamicObjectRow Row { get; set; }

        [ShowInInspector]
        List<TriggerEventTable.TriggerEventRow> TriggerEventRows = new();

        public ExcelTableService TableService { get; set; }


        TriggerEventTable m_TriggerEventTable;
        InstructionTable m_InstructionTable;

        public List<TriggerEvent> TriggerEvents = new List<TriggerEvent>();


        public InteractionItemTracker m_Tracker = null;

        public Action<Args> TriggerEnter3dEvent;

        public Action<Args> TriggerExit3dEvent;


        public Action<Args> InteractEvent;

        public bool IsRunningTriggers
        {
            get
            {
                foreach (var triggerEvent in TriggerEvents)
                {
                    if (triggerEvent.IsRunning)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public DynamicObject() : base() { }
        public DynamicObject(GameObject go) : base(go)
        {
        }
        // public Vector3 HotSpotPosition => this.CachedTransfrom.TransformPoint(Row?.);


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

        public TriggerEventTable.TriggerEventRow GetTriggerEventRow(int id)
        {
            TriggerEventTable.TriggerEventRow row = null;
#if UNITY_EDITOR
            if (Application.isPlaying && m_TriggerEventTable != null)
            {
                Debug.Log($"GetRowByTriggerEventTable");
                row = m_TriggerEventTable.GetRowById(id);
            }
            else
            {
                row = GameSupportEditorUtility.GetTriggerEventRowById(id);
            }
#else
            row = m_TriggerEventTable.GetRowById(id);
#endif
            return row;
        }

        public InstructionTable.InstructionRow GetInstructionRow(int id)
        {
            InstructionTable.InstructionRow instructionRow = null;

#if UNITY_EDITOR
            if (Application.isPlaying && m_InstructionTable != null)
            {
                Debug.Log($"GetRowByInstructionTable");
                instructionRow = m_InstructionTable.GetRowById(id);
            }
            else
            {
                instructionRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<InstructionTableOverview, InstructionTable.InstructionRow>(id);
            }

#else
            instructionRow = m_InstructionTable.GetRowById(id);
#endif
            return instructionRow;
        }


        protected override void DoAwake()
        {
            CurrentArgs = new Args(this);
            CurrentArgs.Main = Main;
            var triggerIds = Row.GetTriggerEventIdList();
            m_TriggerEventTable = TableService?.GetExcelTable<TriggerEventTable>();
            m_InstructionTable = TableService?.GetExcelTable<InstructionTable>();

            TriggerEventRows.Clear();
            foreach (var triggerId in triggerIds)
            {
                TriggerEventTable.TriggerEventRow row = GetTriggerEventRow(triggerId);
                Debug.Log($"Create TriggerId:{triggerId}  row:{row}");
                if (row != null)
                {
                    TriggerEventRows.Add(row);
                }
            }

            TriggerEvents.Clear();
            foreach (var triggerRow in TriggerEventRows)
            {
                var triggerInstance = ClassUtility.CreateInstance<TriggerEvent>(triggerRow.TriggerType);
                if (triggerInstance == null)
                {
                    return;
                }
                triggerInstance.Row = triggerRow;
                triggerInstance.LoadParamsFromJson(triggerRow.Params);
                triggerInstance.RunInstructions = GetInstructionList(triggerRow.GetInstructionList());

                switch (triggerInstance.TriggerType)
                {
                    case TriggerTypeEnum.OnInteract:
                        m_Tracker = CachedTransfrom.GetOrAddComponent<InteractionItemTracker>();
                        m_Tracker.EventInteract += OnInteract;
                        triggerInstance.EventRunInstructionsEnd -= OnInteractEnd;
                        triggerInstance.EventRunInstructionsEnd += OnInteractEnd;
                        break;
                    case TriggerTypeEnum.OnTriggerEnter3D:
                        if (TriggerEnter3dEvent == null)
                        {
                            TriggerEnter3dEvent += OnTriggerEnter3dEvent;
                        }
                        break;
                    case TriggerTypeEnum.OnTriggerExit3D:
                        if (TriggerExit3dEvent == null)
                        {
                            TriggerExit3dEvent += OnTriggerExit3dEvent;
                        }
                        break;

                }

                TriggerEvents.Add(triggerInstance);
            }

            if (m_Tracker != null)
            {
                InteractEvent += OnInteractEvent;
            }

        }

        protected override void DoUpdate()
        {
            base.DoUpdate();
            foreach (var trigger in TriggerEvents)
            {
                trigger.OnUpdate();
            }
        }



        // private void UpdateHotSpot()
        // {
        //     bool wasActive = this.IsActive;

        //     if (this.Target == null)
        //     {
        //         this.IsActive = false;
        //         this.Distance = float.MaxValue;
        //     }
        //     else
        //     {
        //         this.Distance = Vector3.Distance(
        //             this.Target.transform.position,
        //             this.Position
        //         );

        //         this.IsActive = this.Distance <= this.Radius;
        //     }

        //     this.Transition = Mathf.SmoothDamp(
        //         this.Transition,
        //         this.IsActive ? 1f : 0f,
        //         ref this.m_Velocity,
        //         TRANSITION_SMOOTH_TIME
        //     );

        //     this.m_Spots.OnUpdate(this);

        //     switch (wasActive)
        //     {
        //         case false when this.IsActive: this.EventOnActivate?.Invoke(); break;
        //         case true when !this.IsActive: this.EventOnDeactivate?.Invoke(); break;
        //     }
        // }



        void OnInteract(Characters.Character character, IInteractive interactive)
        {
            // if (CheckInteract != null && CheckInteract(null))
            // {

            // }

            Debug.Log($"OnInteract:{gameObject.name}");
            if (InteractEvent != null)
            {
                InteractEvent.Invoke(CurrentArgs);
            }
        }

        [Button("触发交互指令")]
        void OnInteract()
        {
            OnInteract(null, null);
        }



        void OnInteractEnd()
        {
            m_Tracker?.Stop();
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

        public void TriggerEnter3d(Collider collider)
        {
            TriggerEnter3dEvent?.Invoke(CurrentArgs);
        }

        public void TriggerExit3d(Collider collider)
        {
            TriggerExit3dEvent?.Invoke(CurrentArgs);
        }




        InstructionList GetInstructionList(List<int> ids)
        {
            List<Instruction> instructions = new();

            foreach (var instructionId in ids)
            {
                InstructionTable.InstructionRow instructionRow = GetInstructionRow(instructionId);
                if (instructionRow == null || instructionRow.InstructionType == null)
                {
                    continue;
                }

                var InstructionInstance = ClassUtility.CreateInstance<Instruction>(instructionRow.InstructionType);
                InstructionInstance.LoadParams(instructionRow.Params);

                instructions.Add(InstructionInstance);
            }

            return new InstructionList(instructions.ToArray());
        }


    }
}