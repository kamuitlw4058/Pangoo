using System;
using System.Collections.Generic;


using Pangoo.MetaTable;
using MetaTable;
using System.Linq;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public InstructionList GetDirectInstructionList(DirectInstruction[] directInstructionList, TriggerEvent trigger, bool DiableOnFinish = false, bool DiableInteractOnFinish = false, bool DiableHotspotOnFinish = false)
        {
            List<Instruction> ret = new();

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



            foreach (var instruction in ret)
            {
                instruction.Trigger = trigger;
            }
            return new InstructionList(ret.ToArray());
        }

        public void BuildTriggerEvent(int i, DirectInstructionGroup directInstructionGroup)
        {

            if (directInstructionGroup.DirectInstructionList == null || directInstructionGroup.DirectInstructionList.Length == 0) return;

            ITriggerEventRow row = new Pangoo.MetaTable.TriggerEventRow();

            row.Uuid = UuidUtility.GetNewUuid();
            row.Name = $"DI_{row.TriggerType}_{directInstructionGroup.DirectInstructionList?.Length ?? 0}";
            row.Params = "{}";
            row.Uuid = directInstructionGroup.Uuid;
            row.Targets = string.Empty;
            row.Enabled = directInstructionGroup.InitEnabled;
            row.TriggerType = directInstructionGroup.TriggerType.ToString();
            row.ConditionType = directInstructionGroup.ConditionType.ToString();

            var trigger = CreateTriggerEvent(row, false);
            switch (directInstructionGroup.ConditionType)
            {
                case ConditionTypeEnum.NoCondition:
                    trigger.ConditionInstructions.Add(1, GetDirectInstructionList(directInstructionGroup.DirectInstructionList, trigger, directInstructionGroup.DisableOnFinish, directInstructionGroup.DisableInteractOnFinish, directInstructionGroup.DisableHotspotOnFinish));
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

                    trigger.ConditionInstructions.Add(1, GetDirectInstructionList(directInstructionGroup.DirectInstructionList, trigger));
                    trigger.ConditionInstructions.Add(0, GetDirectInstructionList(directInstructionGroup.FailedDirectInstructionList, trigger));
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
                        trigger.ConditionInstructions.Add(kv.Key, GetDirectInstructionList(kv.Value, trigger));
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