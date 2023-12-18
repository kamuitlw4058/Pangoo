using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using Sirenix.Utilities;
using UnityEngine;


namespace Pangoo.Core.VisualScripting
{

    public partial struct DirectInstruction
    {
        public static Instruction GetSelfTriggerEnabledInstruction(bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetSelfTriggerEnabled>();
            instruction.ParamsRaw = new InstructionBoolParams();
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetDynamicObjectPlayTimelineInstruction(int dynamicObjectId)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectPlayTimeline>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            return instruction;
        }

        public static Instruction GetSetVariableBoolInstruction(int VariableId, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetVariableBool>();
            instruction.ParamsRaw.VariableId = VariableId;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetSetPlayerIsControllable(bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetPlayerControllable>();
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetSetGameObjectActive(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionGameObjectActive>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetSetGlobalGameObjectActive(string root, string rootChild, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionGlobalGameObjectActive>();
            instruction.ParamsRaw.Root = root;
            instruction.ParamsRaw.RootChild = rootChild;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetActiveCameraGameObject(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionActiveCameraGameObject>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetUnactiveCameraGameObject(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionUnactiveCameraGameObject>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }


        public static Instruction GetSubGameObjectPlayTimeline(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSubGameObjectPlayTimeline>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetDynamicObjectModelActive(int dynamicObjectId, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectSetModelActive>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetDynamicObjectRunExecute(int dynamicObjectId, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectRunExecute>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }


        public static Instruction GetDynamicObjectHotspotActive(int dynamicObjectId, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectHotspotActive>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetInstructionById(int instructionId, InstructionTable instructionTable)
        {
            return InstructionList.BuildInstruction(instructionId, instructionTable);
        }


        public static Instruction GetChangeGameSectionInstruction(int val)
        {
            var instruction = Activator.CreateInstance<InstructionChangeGameSection>();
            instruction.ParamsRaw.GameSectionId = val;
            return instruction;
        }

        public static Instruction GetShowSubtitleInstruction(string val, float duration)
        {
            var instruction = Activator.CreateInstance<InstructionShowString>();
            instruction.ParamsRaw.Context = val;
            instruction.ParamsRaw.Duration = duration;
            return instruction;
        }
        public static Instruction GetImageFadeInstruction(string targetName, float alphaValue, float tweenTime, string tweenID)
        {
            var instruction = Activator.CreateInstance<InstructionImageFade>();
            instruction.ParamsRaw.TargetName = targetName;
            instruction.ParamsRaw.AlphaValue = alphaValue;
            instruction.ParamsRaw.TweenTime = tweenTime;
            instruction.ParamsRaw.TweenID = tweenID;
            return instruction;
        }

        public static Instruction GetCanvasGroupFadeInstruction(string targetName, float alphaValue, float tweenTime)
        {
            var instruction = Activator.CreateInstance<InstructionCanvasGroupFade>();
            instruction.ParamsRaw.TargetName = targetName;
            instruction.ParamsRaw.AlphaValue = alphaValue;
            instruction.ParamsRaw.TweenTime = tweenTime;
            return instruction;
        }

        public static Instruction GetWaitTimeInstruction(float duration)
        {
            var instruction = Activator.CreateInstance<InstructionWaitTime>();
            instruction.ParamsRaw.Val = duration;
            return instruction;
        }

        public static Instruction GetSubGameObjectPauseTimeline(string path)
        {
            var instruction = Activator.CreateInstance<InstructionSubGameObjectPauseTimeline>();
            instruction.ParamsRaw.Path = path;
            return instruction;
        }

        public static Instruction GetDynamicObjectModelTriggerEnabled(int dynamicObjectId, int triggerId, bool enabled)
        {
            var instruction = Activator.CreateInstance<InstructionSetDOTriggerEnabled>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            instruction.ParamsRaw.TriggerId = triggerId;
            instruction.ParamsRaw.Enabled = enabled;
            return instruction;
        }

        public static Instruction GetPlaySound(int SoundId, bool loop, bool WaitToComplete, float fadeTime)
        {
            var instruction = Activator.CreateInstance<InstructionPlaySound>();
            instruction.ParamsRaw.SoundId = SoundId;
            instruction.ParamsRaw.Loop = loop;
            instruction.ParamsRaw.WaitToComplete = WaitToComplete;
            instruction.ParamsRaw.FadeTime = fadeTime;
            return instruction;
        }

        public static Instruction GetStopSound(int SoundId, float fadeTime)
        {
            var instruction = Activator.CreateInstance<InstructionStopSound>();
            instruction.ParamsRaw.SoundId = SoundId;
            instruction.ParamsRaw.FadeTime = fadeTime;
            return instruction;
        }

        public static Instruction GetDynamicObjectSubGameObjectEnabled(int dynamicObjectId, string path, bool enabled)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectSetSubGameObejctActive>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            instruction.ParamsRaw.Path = !path.IsNullOrWhiteSpace() ? new string[] { path } : null;
            instruction.ParamsRaw.Val = enabled;
            return instruction;
        }

        public static Instruction GetShowHideCursor(bool isShow,CursorLockMode cursorLockMode)
        {
            var instruction = Activator.CreateInstance<InstructionShowHideCursor>();
            instruction.ParamsRaw.Visible = isShow;
            instruction.ParamsRaw.CursorLockMode = cursorLockMode;
            return instruction;
        }

        public static Instruction GetWaitMsg(string conditionString)
        {
            var instruction = Activator.CreateInstance<InstructionWaitMsg>();
            instruction.ParamsRaw.ConditionString = conditionString;
            return instruction;
        }

        public static Instruction GetDoTweenKill(string tweenID)
        {
            var instruction = Activator.CreateInstance<InstructionDoTweenKill>();
            instruction.ParamsRaw.TweenID = tweenID;
            return instruction;
        }
        
        public static Instruction GetCheckBoolVariableList(List<int> variableList,int setVariableID)
        {
            var instruction = Activator.CreateInstance<InstructionCheckVariableBoolList>();
            instruction.ParamsRaw.VariableIdList = variableList;
            instruction.ParamsRaw.SetVariableID = setVariableID;
            return instruction;
        }

        public static Instruction GetDynamicObjectInteractEnable(int dynamicObjectId, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectInteractEnable>();
            instruction.ParamsRaw.DynamicObjectId = dynamicObjectId;
            instruction.ParamsRaw.Val = val;
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
                case DirectInstructionTypeEnum.DynamicObjectRunExecute:
                    return GetDynamicObjectRunExecute(Int1, Bool1);
                case DirectInstructionTypeEnum.PlaySound:
                    return GetPlaySound(Int1, Bool1, Bool2, Float1);
                case DirectInstructionTypeEnum.StopSound:
                    return GetStopSound(Int1, Float1);
                case DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled:
                    return GetDynamicObjectSubGameObjectEnabled(Int1, DropdownString1, Bool1);
                case DirectInstructionTypeEnum.ImageFade:
                    return GetImageFadeInstruction(String1, Float1, Float2, String2);
                case DirectInstructionTypeEnum.ShowHideCursor:
                    return GetShowHideCursor(Bool1,CursorLockMode1);
                case DirectInstructionTypeEnum.CanvasGroup:
                    return GetCanvasGroupFadeInstruction(String1, Float1, Float2);
                case DirectInstructionTypeEnum.WaitMsg:
                    return GetWaitMsg(String1);
                case DirectInstructionTypeEnum.DoTweenKill:
                    return GetDoTweenKill(String1);
                case DirectInstructionTypeEnum.SetGlobalGameObjectActive:
                    return GetSetGlobalGameObjectActive(String1, String2, Bool1);
                case DirectInstructionTypeEnum.CheckBoolVariableList:
                    return GetCheckBoolVariableList(ListInt1,Int1);
                case DirectInstructionTypeEnum.DynamicObjectInteractEnable:
                    return GetDynamicObjectInteractEnable(Int1, Bool1);
            }

            return null;
        }
    }

}