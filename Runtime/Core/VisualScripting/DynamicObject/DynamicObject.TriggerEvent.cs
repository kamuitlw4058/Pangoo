using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using LitJson;
using Pangoo.Core.Common;
using Pangoo.MetaTable;
using MetaTable;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {


        TriggerEventRowByUuidHandler m_TriggerHandler;
        InstructionGetRowByUuidHandler m_InstructionHandler;

        ConditionGetRowByUuidHandler m_ConditionHandler;

        VariableGetRowByUuidHandler m_VariableHandler;



        [ShowInInspector]
        public bool IsRunningTriggers
        {
            get
            {
                foreach (var triggers in TriggerDict.Values)
                {
                    foreach (var trigger in triggers)
                    {
                        if (trigger.IsRunning)
                        {
                            return true;
                        }
                    }

                }
                return false;
            }
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
                    var instructionList = DirectInstructionList.LoadInstructionList(triggerEvent.Row.InstructionList, m_InstructionHandler);
                    if (instructionList != null)
                    {
                        triggerEvent.ConditionInstructions.Add(1, instructionList);
                    }
                    break;
                case ConditionTypeEnum.BoolCondition:
                    triggerEvent.Conditions = ConditionList.BuildConditionList(triggerEvent.Row.GetConditionList(), m_ConditionHandler);
                    var defaultinstructionList = DirectInstructionList.LoadInstructionList(triggerEvent.Row.InstructionList, m_InstructionHandler);
                    if (defaultinstructionList != null)
                    {
                        triggerEvent.ConditionInstructions.Add(1, defaultinstructionList);
                    }

                    var failedInstructionList = DirectInstructionList.LoadInstructionList(triggerEvent.Row.FailInstructionList, m_InstructionHandler);
                    if (failedInstructionList != null)
                    {
                        triggerEvent.ConditionInstructions.Add(0, failedInstructionList);
                    }

                    break;
                case ConditionTypeEnum.StateCondition:
                    triggerEvent.Conditions = ConditionList.BuildConditionList(triggerEvent.Row.GetConditionList(), m_ConditionHandler);
                    Dictionary<int, DirectInstructionList> StateInstructions = JsonMapper.ToObject<Dictionary<int, DirectInstructionList>>(triggerEvent.Row.Params);
                    foreach (var kv in StateInstructions)
                    {

                        triggerEvent.ConditionInstructions.Add(kv.Key, kv.Value.ToInstructionList(m_InstructionHandler));
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



        public TriggerEvent CreateTriggerEvent(ITriggerEventRow row, bool buildInstructions = true)
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
                case TriggerTypeEnum.OnTimelineSignal:
                    DoAwakeTimeineSignal();
                    break;
            }
            TriggerRegister(ret);
            return ret;
        }

        [ShowInInspector]
        public bool InteractEnable
        {
            get
            {
                return m_Tracker?.InteractEnable ?? false;
            }
            set
            {
                if (m_Tracker != null)
                {
                    m_Tracker.InteractEnable = value;
                }
            }
        }

        void DoAwakeTriggerEvent()
        {
            if (Main.MetaTable != null)
            {
                m_InstructionHandler = Main.MetaTable.GetInstructionByUuid;
                m_ConditionHandler = Main.MetaTable.GetConditionByUuid;
                m_TriggerHandler = Main.MetaTable.GetTriggerEventByUuid;
                m_VariableHandler = Main.MetaTable.GetVariablesByUuid;
            }
            // m_VariablesTable = TableService?.GetExcelTable<VariablesTable>();


            var triggerUuids = Row.GetTriggerEventUuidList();

            DoAwakeTimeline();

            Debug.Log($"triggerIds:{triggerUuids.Count}");

            foreach (var triggerUuid in triggerUuids)
            {
                ITriggerEventRow row = TriggerEventRowExtension.GetByUuid(triggerUuid, m_TriggerHandler);
                Debug.Log($"Create TriggerId:{triggerUuid}  row:{row}");
                if (row != null)
                {
                    CreateTriggerEvent(row);
                }
            }



            DoAwakeDirectionInstruction();

            if (m_Tracker != null)
            {
                if (Row.DefaultDisableInteract)
                {
                    m_Tracker.InteractEnable = false;
                }
                else
                {
                    m_Tracker.InteractEnable = true;
                }
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