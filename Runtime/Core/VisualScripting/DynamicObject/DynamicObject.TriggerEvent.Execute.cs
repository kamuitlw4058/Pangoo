using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using MetaTable;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        public void StartExternalInstruction(string instructionUuid, string targets = null)
        {

            Pangoo.MetaTable.TriggerEventRow row = new();
            DirectInstructionList directInstructionList = new DirectInstructionList();
            directInstructionList.DirectInstructions = new DirectInstruction[1];
            directInstructionList.DirectInstructions[0] = new DirectInstruction
            {
                InstructionType = DirectInstructionTypeEnum.RunInstruction,
                Uuid = instructionUuid,
            };
            row.Uuid = UuidUtility.GetNewUuid();
            row.Targets = targets;
            row.InstructionList = (directInstructionList.Save());
            Debug.Log($"Row:{row.InstructionList}");
            row.TriggerType = TriggerTypeEnum.OnExternalInstruction.ToString();
            row.ConditionType = ConditionTypeEnum.NoCondition.ToString();
            row.Enabled = true;

            TriggerClear(TriggerTypeEnum.OnExternalInstruction);
            var triggerEvent = CreateTriggerEvent(row, true);
            TriggerInovke(TriggerTypeEnum.OnExternalInstruction, null);
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