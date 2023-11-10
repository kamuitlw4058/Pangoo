using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using UnityEditor;
using Sirenix.Utilities;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public InstructionList GetDirectInstructionList(DirectInstructionGroup diGroup, TriggerEvent trigger)
        {
            List<Instruction> ret = new();

            foreach (var directInstruction in diGroup.DirectInstructionList)
            {
                var instruction = directInstruction.ToInstruction(m_InstructionTable);
                if (instruction != null)
                {
                    ret.Add(instruction);
                }
            }

            if (diGroup.DisableOnFinish)
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

            TriggerEventTable.TriggerEventRow row = new TriggerEventTable.TriggerEventRow();

            row.Id = (i * -1) - 1;
            row.Name = $"DI_{row.TriggerType}_{directInstructionGroup.DirectInstructionList?.Length ?? 0}";
            row.Params = "{}";
            row.ConditionType = ConditionTypeEnum.NoCondition.ToString();
            row.Targets = string.Empty;
            row.Enabled = directInstructionGroup.InitEnabled;
            row.TriggerType = directInstructionGroup.TriggerType.ToString();


            var trigger = CreateTriggerEvent(row, false);
            trigger.ConditionInstructions.Add(1, GetDirectInstructionList(directInstructionGroup, trigger));
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