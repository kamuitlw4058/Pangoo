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

        Instruction GetSelfTriggerEnabledInstruction(bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetSelfTriggerEnabled>();
            instruction.m_Params = new InstructionBoolParams();
            instruction.m_Params.Val = val;
            return instruction;
        }

        Instruction GetDynamicObjectPlayTimelineInstruction(int dynamicObjectId)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectPlayTimeline>();
            instruction.m_Params.Val = dynamicObjectId;
            return instruction;
        }

        public InstructionList GetDirectInstructionList(DirectInstruction directInstruction, TriggerEvent trigger)
        {
            List<Instruction> ret = new();

            switch (directInstruction.InstructionType)
            {
                case DirectInstructionTypeEnum.DynamicObjectPlayTimeline:
                    var instruction = GetDynamicObjectPlayTimelineInstruction(directInstruction.Int1);
                    ret.Add(instruction);
                    break;
            }

            if (directInstruction.DisableOnFinish)
            {
                var instruction = GetSelfTriggerEnabledInstruction(false);
                ret.Add(instruction);
            }
            foreach (var instruction in ret)
            {
                instruction.Trigger = trigger;
            }
            return new InstructionList(ret.ToArray());
        }


        void DoAwakeDirectionInstruction()
        {
            var directInstructions = DirectInstruction.CreateArray(Row?.DirectInstructions);
            foreach (var directInstruction in directInstructions)
            {
                switch (directInstruction.TriggerType)
                {
                    case TriggerTypeEnum.OnTriggerEnter3D:
                        TriggerEventTable.TriggerEventRow row = new TriggerEventTable.TriggerEventRow();
                        row.Id = 1;
                        row.Name = $"DI_{directInstruction.TriggerType}_{directInstruction.InstructionType}_{directInstruction.Int1}";
                        row.Params = "{}";
                        row.Targets = string.Empty;
                        row.Enabled = directInstruction.InitEnabled;
                        var triggerEvent = CreateTriggerEvent<TriggerEventOnTriggerEnter3d>(row);
                        triggerEvent.RunInstructions = GetDirectInstructionList(directInstruction, triggerEvent);
                        TriggerEvents.Add(triggerEvent);
                        break;
                }
            }
        }




    }
}