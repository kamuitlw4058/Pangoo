using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;


namespace Pangoo.Core.VisualScripting
{

    public partial struct DirectInstruction
    {
        Instruction GetSelfTriggerEnabledInstruction(bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetSelfTriggerEnabled>();
            instruction.ParamsRaw = new InstructionBoolParams();
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetDynamicObjectPlayTimelineInstruction(int dynamicObjectId)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectPlayTimeline>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            return instruction;
        }

        Instruction GetSetVariableBoolInstruction(int VariableId, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetVariableBool>();
            instruction.ParamsRaw.VariableId = VariableId;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetSetPlayerIsControllable(bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetPlayerControllable>();
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetSetGameObjectActive(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionGameObjectActive>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetActiveCameraGameObject(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionActiveCameraGameObject>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetUnactiveCameraGameObject(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionUnactiveCameraGameObject>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }


        Instruction GetSubGameObjectPlayTimeline(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSubGameObjectPlayTimeline>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetDynamicObjectModelActive(int dynamicObjectId, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectSetModelActive>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetDynamicObjectHotspotActive(int dynamicObjectId, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectHotspotActive>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetInstructionById(int instructionId, InstructionTable instructionTable)
        {
            return InstructionList.BuildInstruction(instructionId, instructionTable);
        }


        Instruction GetChangeGameSectionInstruction(int val)
        {
            var instruction = Activator.CreateInstance<InstructionChangeGameSection>();
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetShowSubtitleInstruction(string val, float duration)
        {
            var instruction = Activator.CreateInstance<InstructionShowString>();
            instruction.ParamsRaw.Context = val;
            instruction.ParamsRaw.Duration = duration;
            return instruction;
        }

        Instruction GetWaitTimeInstruction(float duration)
        {
            var instruction = Activator.CreateInstance<InstructionWaitTime>();
            instruction.ParamsRaw.Val = duration;
            return instruction;
        }

        Instruction GetSubGameObjectPauseTimeline(string path)
        {
            var instruction = Activator.CreateInstance<InstructionSubGameObjectPauseTimeline>();
            instruction.ParamsRaw.Path = path;
            return instruction;
        }

        Instruction GetDynamicObjectModelTriggerEnabled(int dynamicObjectId, int triggerId, bool enabled)
        {
            var instruction = Activator.CreateInstance<InstructionSetDOTriggerEnabled>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            instruction.ParamsRaw.TriggerId = triggerId;
            instruction.ParamsRaw.Enabled = enabled;
            return instruction;
        }

        public Instruction ToInstruction(InstructionTable instructionTable = null)
        {
            switch (InstructionType)
            {
                case DirectInstructionTypeEnum.DynamicObjectPlayTimeline:
                    return GetDynamicObjectPlayTimelineInstruction(Int1);
                case DirectInstructionTypeEnum.ChangeGameSection:
                    return GetChangeGameSectionInstruction(Int1);
                case DirectInstructionTypeEnum.SetBoolVariable:
                    return GetSetVariableBoolInstruction(Int1, Bool1);
                case DirectInstructionTypeEnum.SetPlayerIsControllable:
                    return GetSetPlayerIsControllable(Bool1);
                case DirectInstructionTypeEnum.SetGameObjectActive:
                    return GetSetGameObjectActive(DropdownString1, Bool1);
                case DirectInstructionTypeEnum.ActiveCameraGameObject:
                    return GetActiveCameraGameObject(DropdownString1, Bool1);
                case DirectInstructionTypeEnum.UnactiveCameraGameObject:
                    return GetUnactiveCameraGameObject(DropdownString1, Bool1);
                case DirectInstructionTypeEnum.SubGameObjectPlayTimeline:
                    return GetSubGameObjectPlayTimeline(DropdownString1, Bool1);
                case DirectInstructionTypeEnum.DynamicObjectModelActive:
                    return GetDynamicObjectModelActive(Int1, Bool1);
                case DirectInstructionTypeEnum.DynamicObjectHotspotActive:
                    return GetDynamicObjectHotspotActive(Int1, Bool1);
                case DirectInstructionTypeEnum.RunInstruction:
                    return GetInstructionById(Int1, instructionTable);
                case DirectInstructionTypeEnum.ShowSubtitle:
                    return GetShowSubtitleInstruction(String1, Float1);
                case DirectInstructionTypeEnum.CloseSelfTrigger:
                    return GetSelfTriggerEnabledInstruction(false);
                case DirectInstructionTypeEnum.WaitTime:
                    return GetWaitTimeInstruction(Float1);
                case DirectInstructionTypeEnum.SubGameObjectPauseTimeline:
                    return GetSubGameObjectPauseTimeline(DropdownString1);
                case DirectInstructionTypeEnum.DynamicObjectModelTriggerEnabled:
                    return GetDynamicObjectModelTriggerEnabled(Int1, Int2, Bool1);
            }

            return null;
        }
    }

}