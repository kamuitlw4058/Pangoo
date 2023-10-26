using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        [ShowInInspector]
        [ReadOnly]
        List<TriggerEventTable.TriggerEventRow> TriggerEventRows = new();

        TriggerEventTable m_TriggerEventTable;
        InstructionTable m_InstructionTable;

        [HideReferenceObjectPicker]
        public Dictionary<int, TriggerEvent> TriggerEvents = new();




        public bool IsRunningTriggers
        {
            get
            {
                foreach (var triggerEvent in TriggerEvents.Values)
                {
                    if (triggerEvent.IsRunning)
                    {
                        return true;
                    }
                }
                return false;
            }
        }



        public void SetTriggerEnabled(int id, bool val)
        {
            Debug.Log($"SetTriggerEnabled:{id}:{val}");
            foreach (var trigger in TriggerEvents)
            {
                if (trigger.Value.Row.Id == id)
                {
                    trigger.Value.Enabled = val;
                }
            }
        }
        public T CreateTriggerEvent<T>(TriggerEventTable.TriggerEventRow row) where T : TriggerEvent
        {
            var ret = Activator.CreateInstance<T>();
            if (ret == null)
            {
                return null;
            }
            ret.Row = row;
            ret.Parent = gameObject;
            ret.dynamicObject = this;
            ret.SetEnabled(row.Enabled);
            ret.IsDirectInstuction = true;
            return ret;
        }


        public T CreateInstruction<T>() where T : Instruction
        {
            var ret = Activator.CreateInstance<T>();

            return ret;
        }



        public TriggerEvent CreateTriggerEvent(TriggerEventTable.TriggerEventRow row)
        {
            var ret = ClassUtility.CreateInstance<TriggerEvent>(row.TriggerType);
            if (ret == null)
            {
                Debug.LogError($"Create Trigger Failed!{row.TriggerType}");
                return null;
            }
            ret.Row = row;
            ret.Parent = gameObject;
            ret.dynamicObject = this;
            ret.SetEnabled(row.Enabled);
            ret.LoadParamsFromJson(row.Params);
            ret.RunInstructions = InstructionList.BuildInstructionList(ret, row.GetInstructionList(), m_InstructionTable);
            return ret;
        }


        void DoAwakeTriggerEvent()
        {
            m_TriggerEventTable = TableService?.GetExcelTable<TriggerEventTable>();
            m_InstructionTable = TableService?.GetExcelTable<InstructionTable>();

            var triggerIds = Row.GetTriggerEventIdList();
            TriggerEventRows.Clear();
            TriggerEvents.Clear();

            DoAwakeTimeline();

            Debug.Log($"triggerIds:{triggerIds.Count}");

            foreach (var triggerId in triggerIds)
            {
                TriggerEventTable.TriggerEventRow row = TriggerEventRowExtension.GetById(triggerId);
                Debug.Log($"Create TriggerId:{triggerId}  row:{row}");
                if (row != null)
                {
                    TriggerEventRows.Add(row);
                }
            }

            foreach (var triggerRow in TriggerEventRows)
            {
                var triggerInstance = CreateTriggerEvent(triggerRow);
                if (triggerInstance == null)
                {
                    Debug.LogError($"Create Trigger Failed!{triggerRow.TriggerType}");
                    return;
                }

                switch (triggerInstance.TriggerType)
                {
                    case TriggerTypeEnum.OnInteract:
                        m_Tracker = CachedTransfrom.GetOrAddComponent<InteractionItemTracker>();
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

                TriggerEvents.Add(triggerInstance.Row.Id, triggerInstance);
            }

            DoAwakeDirectionInstruction();

            if (m_Tracker != null)
            {
                InteractEvent += OnInteractEvent;
                m_Tracker.EventInteract += OnInteract;
            }
        }


    }
}