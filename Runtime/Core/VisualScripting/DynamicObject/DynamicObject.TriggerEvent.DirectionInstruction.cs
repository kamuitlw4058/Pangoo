using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using UnityEditor;



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

        Instruction GetChangeGameSectionInstruction(int val)
        {
            var instruction = Activator.CreateInstance<InstructionChangeGameSection>();
            instruction.m_Params.Val = val;
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
                case DirectInstructionTypeEnum.ChangeGameSection:
                    var instructionGameSection = GetChangeGameSectionInstruction(directInstruction.Int1);
                    ret.Add(instructionGameSection);
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

        public TriggerEvent BuildTriggerEvent(int i, DirectInstruction directInstruction)
        {
            TriggerEvent ret = null;
            TriggerEventTable.TriggerEventRow row = new TriggerEventTable.TriggerEventRow();
            row.Id = (i * -1) - 1;
            row.Name = $"DI_{directInstruction.TriggerType}_{directInstruction.InstructionType}_{directInstruction.Int1}";
            row.Params = "{}";
            row.Targets = string.Empty;
            row.Enabled = directInstruction.InitEnabled;

            switch (directInstruction.TriggerType)
            {
                case TriggerTypeEnum.OnTriggerEnter3D:
                    ret = CreateTriggerEvent<TriggerEventOnTriggerEnter3d>(row);
                    break;
                case TriggerTypeEnum.OnInteract:
                    ret = CreateTriggerEvent<TriggerEventOnInteraction>(row);
                    m_Tracker = CachedTransfrom.GetOrAddComponent<InteractionItemTracker>();
                    ret.EventRunInstructionsEnd -= OnInteractEnd;
                    ret.EventRunInstructionsEnd += OnInteractEnd;
                    break;
            }

            if (ret != null)
            {
                ret.RunInstructions = GetDirectInstructionList(directInstruction, ret);
                TriggerEvents.Add(row.Id, ret);
            }

            return ret;
        }


        void DoAwakeDirectionInstruction()
        {
            var directInstructions = DirectInstruction.CreateArray(Row?.DirectInstructions);
            for (int i = 0; i < directInstructions.Length; i++)
            {
                var directInstruction = directInstructions[i];
                BuildTriggerEvent(i, directInstruction);
            }
        }




    }
}