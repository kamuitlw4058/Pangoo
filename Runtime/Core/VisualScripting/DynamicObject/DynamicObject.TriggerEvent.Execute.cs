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
        public const int ExternalInstructionId = -100;
        public void StartExternalInstruction(int instructionId, string targets = null)
        {

            TriggerEventTable.TriggerEventRow row = new TriggerEventTable.TriggerEventRow();
            row.Id = ExternalInstructionId;
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

            TriggerClear(TriggerTypeEnum.OnExternalInstruction);
            var triggerEvent = CreateTriggerEvent(row, true);
            TriggerInovke(TriggerTypeEnum.OnExternalInstruction, ExternalInstructionId);
        }

        public void StartExecuteEvent()
        {
            Debug.Log($"On StartExecuteEvent:{gameObject.name}");
            TriggerInovke(TriggerTypeEnum.OnExecute);
        }

        public void StopExecuteEvent()
        {
            Debug.Log($"On StopExecuteEvent:{gameObject.name}");
            TriggerStop(TriggerTypeEnum.OnExecute);
        }


    }
}