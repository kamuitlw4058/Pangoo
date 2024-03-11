using System;
using System.Collections.Generic;


using Pangoo.MetaTable;
using MetaTable;
using System.Linq;
using Pangoo.Common;
using LitJson;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public InstructionList GetDirectInstructionList(TriggerTypeEnum triggerType,
            DirectInstruction[] directInstructionList,
            TriggerEvent trigger,
            bool DiableOnFinish = false,
             bool DiableInteractOnFinish = false,
              bool DiableHotspotOnFinish = false,
              bool ShowOtherHotspotOnInteract = false
              )

        {
            List<Instruction> ret = new();

            if (triggerType == TriggerTypeEnum.OnInteract)
            {
                if (!ShowOtherHotspotOnInteract)
                {
                    ret.Add(DirectInstruction.GetSetPlayerEnabledHotspot(false));
                }

            }

            foreach (var directInstruction in directInstructionList)
            {
                var instruction = directInstruction.ToInstruction(m_InstructionHandler);
                if (instruction != null)
                {
                    ret.Add(instruction);
                }
            }

            if (DiableOnFinish)
            {
                var instruction = DirectInstruction.GetSelfTriggerEnabledInstruction(false);
                ret.Add(instruction);
            }

            if (DiableInteractOnFinish)
            {
                var disableInteractInstruction = DirectInstruction.GetDynamicObjectInteractEnable(Row.Uuid, false);
                ret.Add(disableInteractInstruction);
            }

            if (DiableHotspotOnFinish)
            {
                var disableInteractInstruction = DirectInstruction.GetDynamicObjectHotspotActive(Row.Uuid, false);
                ret.Add(disableInteractInstruction);
            }

            if (triggerType == TriggerTypeEnum.OnInteract)
            {
                if (!ShowOtherHotspotOnInteract)
                {
                    ret.Add(DirectInstruction.GetSetPlayerEnabledHotspot(true));
                }
            }

            foreach (var instruction in ret)
            {
                instruction.Trigger = trigger;
            }
            return new InstructionList(ret.ToArray());
        }

        public void BuildTriggerEvent(int i, DirectInstructionGroup directInstructionGroup)
        {
            switch (directInstructionGroup.ConditionType)
            {
                case ConditionTypeEnum.NoCondition:
                    if (directInstructionGroup.DirectInstructionList == null || directInstructionGroup.DirectInstructionList.Length == 0) return;
                    break;
                case ConditionTypeEnum.BoolCondition:
                    if ((directInstructionGroup.DirectInstructionList == null || directInstructionGroup.DirectInstructionList.Length == 0)
                        && (directInstructionGroup.FailedDirectInstructionList == null || directInstructionGroup.FailedDirectInstructionList.Length == 0)) return;
                    break;
                case ConditionTypeEnum.StateCondition:
                    if (directInstructionGroup.StateDirectInstructionDict == null || directInstructionGroup.StateDirectInstructionDict.Count == 0) return;
                    break;
            }

            ITriggerEventRow row = new Pangoo.MetaTable.TriggerEventRow();

            row.Uuid = UuidUtility.GetNewUuid();
            if (!directInstructionGroup.Name.IsNullOrWhiteSpace())
            {
                row.Name = directInstructionGroup.Name;
            }
            else
            {
                row.Name = $"DI_{row.TriggerType}_{directInstructionGroup.Uuid.ToShortUuid()}";
            }
            row.Params = "{}";
            row.Uuid = directInstructionGroup.Uuid;
            row.Enabled = directInstructionGroup.InitEnabled;
            if (directInstructionGroup.UseStringTarget)
            {
                row.Targets = directInstructionGroup.StringTargets.ToListString();
            }
            else
            {
                row.Targets = directInstructionGroup.Targets.ToListString();
            }

            row.TargetListType = (int)directInstructionGroup.TargetProcessType;
            row.TriggerType = directInstructionGroup.TriggerType.ToString();
            row.ConditionType = directInstructionGroup.ConditionType.ToString();
            try
            {
                row.Filter = JsonMapper.ToJson(directInstructionGroup.Filter);
            }
            catch
            {
                row.Filter = null;
            }

            var trigger = CreateTriggerEvent(row, false);
            switch (directInstructionGroup.ConditionType)
            {
                case ConditionTypeEnum.NoCondition:
                    trigger.ConditionInstructions.Add(1, GetDirectInstructionList(directInstructionGroup.TriggerType, directInstructionGroup.DirectInstructionList, trigger, directInstructionGroup.DisableOnFinish, directInstructionGroup.DisableInteractOnFinish, directInstructionGroup.DisableHotspotOnFinish));
                    break;
                case ConditionTypeEnum.BoolCondition:
                    if (directInstructionGroup.UseVariableCondition)
                    {
                        trigger.Conditions = ConditionList.BuildConditionListByBoolVariable(directInstructionGroup.BoolVariableUuds, m_ConditionHandler, m_VariableHandler);
                    }
                    else
                    {
                        trigger.Conditions = ConditionList.BuildConditionList(directInstructionGroup.ConditionUuids.ToList(), m_ConditionHandler);
                    }

                    trigger.ConditionInstructions.Add(1, GetDirectInstructionList(directInstructionGroup.TriggerType, directInstructionGroup.DirectInstructionList, trigger));
                    trigger.ConditionInstructions.Add(0, GetDirectInstructionList(directInstructionGroup.TriggerType, directInstructionGroup.FailedDirectInstructionList, trigger));
                    break;
                case ConditionTypeEnum.StateCondition:
                    if (directInstructionGroup.UseVariableCondition)
                    {
                        trigger.Conditions = ConditionList.BuildConditionListByIntVariable(directInstructionGroup.IntVariableUuid, m_ConditionHandler, m_VariableHandler);
                    }
                    else
                    {
                        trigger.Conditions = ConditionList.BuildConditionList(directInstructionGroup.ConditionUuids.ToList(), m_ConditionHandler);
                    }

                    foreach (var kv in directInstructionGroup.StateDirectInstructionDict)
                    {
                        trigger.ConditionInstructions.Add(kv.Key, GetDirectInstructionList(directInstructionGroup.TriggerType, kv.Value, trigger));
                    }
                    break;
            }

        }

        void DoAwakeDirectionInstruction()
        {
            var directInstructionGroups = DirectInstructionGroup.CreateArray(Row?.DirectInstructions);
            if (directInstructionGroups == null || directInstructionGroups.Length == 0) return;


            for (int i = 0; i < directInstructionGroups.Length; i++)
            {
                var directInstructionGroup = directInstructionGroups[i];
                BuildTriggerEvent(i, directInstructionGroup);
            }
        }




    }
}