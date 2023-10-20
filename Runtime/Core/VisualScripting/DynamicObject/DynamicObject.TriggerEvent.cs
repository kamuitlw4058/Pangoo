using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        List<TriggerEventTable.TriggerEventRow> TriggerEventRows = new();

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

        void OnInteract(Characters.Character character, IInteractive interactive)
        {

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


        void DoAwakeTriggerEvent()
        {
            m_TriggerEventTable = TableService?.GetExcelTable<TriggerEventTable>();
            m_InstructionTable = TableService?.GetExcelTable<InstructionTable>();

            var triggerIds = Row.GetTriggerEventIdList();
            TriggerEventRows.Clear();
            TriggerEvents.Clear();

            foreach (var triggerId in triggerIds)
            {
                TriggerEventTable.TriggerEventRow row = GetTriggerEventRow(triggerId);
                Debug.Log($"Create TriggerId:{triggerId}  row:{row}");
                if (row != null)
                {
                    TriggerEventRows.Add(row);
                }
            }

            foreach (var triggerRow in TriggerEventRows)
            {
                var triggerInstance = ClassUtility.CreateInstance<TriggerEvent>(triggerRow.TriggerType);
                if (triggerInstance == null)
                {
                    return;
                }
                triggerInstance.Row = triggerRow;
                triggerInstance.Parent = gameObject;
                triggerInstance.dynamicObject = this;
                triggerInstance.Enabled = triggerRow.Enabled;
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


        void OnTriggerEnter3dEvent(Args eventParams)
        {
            Debug.Log($"OnTriggerEnter3dEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents)
            {
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnTriggerEnter3D:
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }


        void OnTriggerExit3dEvent(Args eventParams)
        {
            Debug.Log($"OnTriggerExit3dEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents)
            {
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnTriggerEnter3D:
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }



        void OnInteractEvent(Args eventParams)
        {
            Debug.Log($"OnInteractEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents)
            {
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnInteract:
                        // trigger.EventRunInstructionsEnd -= OnInteractEnd;
                        // trigger.EventRunInstructionsEnd += OnInteractEnd;
                        Debug.Log($"Trigger:{trigger?.Row?.Id} inovke ");
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
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