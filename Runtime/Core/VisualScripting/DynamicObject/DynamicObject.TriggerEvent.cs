using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using LitJson;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        [ShowInInspector]
        [ReadOnly]
        List<TriggerEventTable.TriggerEventRow> TriggerEventRows = new();

        TriggerEventTable m_TriggerEventTable;
        InstructionTable m_InstructionTable;

        ConditionTable m_ConditionTable;

        VariablesTable m_VariablesTable;



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

        public void SetConditionInstructionsByRow(TriggerEvent triggerEvent)
        {
            switch (triggerEvent.ConditionType)
            {
                case ConditionTypeEnum.NoCondition:
                    var instructionList = DirectInstructionList.LoadInstructionList(triggerEvent.Row.InstructionList, m_InstructionTable);
                    if (instructionList != null)
                    {
                        triggerEvent.ConditionInstructions.Add(1, instructionList);
                    }
                    break;
                case ConditionTypeEnum.BoolCondition:
                    triggerEvent.Conditions = ConditionList.BuildConditionList(triggerEvent.Row.GetConditionList(), m_ConditionTable);
                    var defaultinstructionList = DirectInstructionList.LoadInstructionList(triggerEvent.Row.InstructionList, m_InstructionTable);
                    if (defaultinstructionList != null)
                    {
                        triggerEvent.ConditionInstructions.Add(1, defaultinstructionList);
                    }

                    var failedInstructionList = DirectInstructionList.LoadInstructionList(triggerEvent.Row.FailInstructionList, m_InstructionTable);
                    if (failedInstructionList != null)
                    {
                        triggerEvent.ConditionInstructions.Add(0, failedInstructionList);
                    }

                    break;
                case ConditionTypeEnum.StateCondition:
                    triggerEvent.Conditions = ConditionList.BuildConditionList(triggerEvent.Row.GetConditionList(), m_ConditionTable);
                    Dictionary<int, DirectInstructionList> StateInstructions = JsonMapper.ToObject<Dictionary<int, DirectInstructionList>>(triggerEvent.Row.Params);
                    foreach (var kv in StateInstructions)
                    {

                        triggerEvent.ConditionInstructions.Add(kv.Key, kv.Value.ToInstructionList(m_InstructionTable));
                    }
                    break;
            }

            foreach (var kv in triggerEvent.ConditionInstructions)
            {
                foreach (var instruction in kv.Value.Instructions)
                {
                    instruction.Trigger = triggerEvent;
                }
            }

        }



        public TriggerEvent CreateTriggerEvent(TriggerEventTable.TriggerEventRow row, bool buildInstructions = true)
        {
            var ret = new TriggerEvent();
            ret.Row = row;
            if (ret.TriggerType == TriggerTypeEnum.Unknown)
            {
                Debug.LogError($"Create Trigger Failed!{row.TriggerType}");
                return null;
            }

            ret.Parent = gameObject;
            ret.dynamicObject = this;
            ret.SetEnabled(row.Enabled);
            if (buildInstructions)
            {
                SetConditionInstructionsByRow(ret);

            }

            switch (ret.TriggerType)
            {
                case TriggerTypeEnum.OnInteract:
                    m_Tracker = CachedTransfrom.GetOrAddComponent<InteractionItemTracker>();
                    ret.EventRunInstructionsEnd -= OnInteractEnd;
                    ret.EventRunInstructionsEnd += OnInteractEnd;
                    break;
                case TriggerTypeEnum.OnTriggerEnter3D:
                    TriggerEnter3dEvent -= OnTriggerEnter3dEvent;
                    TriggerEnter3dEvent += OnTriggerEnter3dEvent;
                    break;
                case TriggerTypeEnum.OnTriggerExit3D:
                    TriggerExit3dEvent -= OnTriggerExit3dEvent;
                    TriggerExit3dEvent += OnTriggerExit3dEvent;
                    break;
                case TriggerTypeEnum.OnMouseLeft:
                    TriggerMouseLeftEvent -= OnInteractMouseLeft;
                    TriggerMouseLeftEvent += OnInteractMouseLeft;
                    break;
                case TriggerTypeEnum.OnPointerEnter:
                    PointerEnterEvent -= OnPointerEnterEvent;
                    PointerEnterEvent += OnPointerEnterEvent;
                    break;
                case TriggerTypeEnum.OnPointerExit:
                    PointerExitEvent -= OnPointerExitEvent;
                    PointerExitEvent += OnPointerExitEvent;
                    break;
                case TriggerTypeEnum.OnPointerClick:
                    PointerClickEvent -= OnPointerClickEvent;
                    PointerClickEvent += OnPointerClickEvent;
                    break;
            }

            TriggerEvents.Add(ret.Row.Id, ret);
            return ret;
        }

        public


        void DoAwakeTriggerEvent()
        {
            m_TriggerEventTable = TableService?.GetExcelTable<TriggerEventTable>();
            m_InstructionTable = TableService?.GetExcelTable<InstructionTable>();
            m_ConditionTable = TableService?.GetExcelTable<ConditionTable>();
            m_VariablesTable = TableService?.GetExcelTable<VariablesTable>();

            var triggerIds = Row.GetTriggerEventIdList();
            TriggerEventRows.Clear();
            TriggerEvents.Clear();

            DoAwakeTimeline();

            Debug.Log($"triggerIds:{triggerIds.Count}");

            foreach (var triggerId in triggerIds)
            {
                TriggerEventTable.TriggerEventRow row = TriggerEventRowExtension.GetById(triggerId, m_TriggerEventTable);
                Debug.Log($"Create TriggerId:{triggerId}  row:{row}");
                if (row != null)
                {
                    TriggerEventRows.Add(row);
                }
            }

            foreach (var triggerRow in TriggerEventRows)
            {
                CreateTriggerEvent(triggerRow);

            }

            DoAwakeDirectionInstruction();

            if (m_Tracker != null)
            {
                InteractEvent += OnInteractEvent;
                m_Tracker.EventInteract += OnInteract;
                m_Tracker.InteractOffset = Row.InteractOffset;
                m_Tracker.InteractRadian = Row.InteractRadian;
                m_Tracker.InteractRadius = Row.InteractRadius;
                var InteractTarget = CachedTransfrom.Find(Row.InteractTarget);
                if (InteractTarget != null)
                {
                    m_Tracker.Instance = InteractTarget.gameObject;
                }
            }
        }


    }
}