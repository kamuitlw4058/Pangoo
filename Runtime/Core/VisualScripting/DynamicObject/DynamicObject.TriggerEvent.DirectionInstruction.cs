using System;
using System.Collections.Generic;


using Pangoo.MetaTable;
using MetaTable;
using System.Linq;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public InstructionList GetDirectInstructionList(bool DiableOnFinish, DirectInstruction[] directInstructionList, TriggerEvent trigger)
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
            row.Targets = string.Empty;
            row.Enabled = directInstructionGroup.InitEnabled;
            row.TriggerType = directInstructionGroup.TriggerType.ToString();
            row.ConditionType = directInstructionGroup.ConditionType.ToString();

            var trigger = CreateTriggerEvent(row, false);
            switch (directInstructionGroup.ConditionType)
            {
                case ConditionTypeEnum.NoCondition:
                    trigger.ConditionInstructions.Add(1, GetDirectInstructionList(directInstructionGroup.DisableOnFinish, directInstructionGroup.DirectInstructionList, trigger));
                    break;
                case ConditionTypeEnum.BoolCondition:
                    trigger.Conditions = ConditionList.BuildConditionList(directInstructionGroup.ConditionUuids.ToList(), m_ConditionHandler);
                    trigger.ConditionInstructions.Add(1, GetDirectInstructionList(false, directInstructionGroup.DirectInstructionList, trigger));
                    trigger.ConditionInstructions.Add(0, GetDirectInstructionList(false, directInstructionGroup.FailedDirectInstructionList, trigger));
                    break;
                case ConditionTypeEnum.StateCondition:
                    trigger.Conditions = ConditionList.BuildConditionList(directInstructionGroup.ConditionUuids.ToList(), m_ConditionHandler);
                    foreach (var kv in directInstructionGroup.StateDirectInstructionDict)
                    {
                        trigger.ConditionInstructions.Add(kv.Key, GetDirectInstructionList(false, kv.Value, trigger));
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