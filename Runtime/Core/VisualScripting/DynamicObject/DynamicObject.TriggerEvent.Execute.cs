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

        public void StartExternalInstruction(int instructionId, string targets = null)
        {
            if (TriggerEvents.ContainsKey(-100))
            {
                TriggerEvents.Remove(-100);
            }
            TriggerEventTable.TriggerEventRow row = new TriggerEventTable.TriggerEventRow();
            row.Id = -100;
            DirectInstructionList directInstructionList = new DirectInstructionList();
            directInstructionList.DirectInstructions = new DirectInstruction[1];
            directInstructionList.DirectInstructions[0] = new DirectInstruction
            {
                InstructionType = DirectInstructionTypeEnum.RunInstruction,
                Int1 = instructionId,
            };
            row.Targets = targets;
            row.InstructionList = (directInstructionList.Save());
            Debug.Log($"Row:{row.InstructionList}");
            row.TriggerType = TriggerTypeEnum.OnExternalInstruction.ToString();
            row.ConditionType = ConditionTypeEnum.NoCondition.ToString();
            row.Enabled = true;

            var triggerEvent = CreateTriggerEvent(row, true);
            triggerEvent?.OnInvoke(CurrentArgs);
        }

        public void StartExecuteEvent()
        {
            Debug.Log($"OnExecuteEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents.Values)
            {
                if (!trigger.Enabled)
                {
                    continue;
                }

                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnExecute:
                        Debug.Log($"Trigger:{trigger?.Row?.Id} inovke ");
                        trigger.OnInvoke(CurrentArgs);
                        break;
                }
            }
        }

        public void StopExecuteEvent()
        {
            Debug.Log($"OnExecuteEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents.Values)
            {
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnExecute:
                        trigger.IsStoped = true;
                        break;
                }
            }
        }






    }
}