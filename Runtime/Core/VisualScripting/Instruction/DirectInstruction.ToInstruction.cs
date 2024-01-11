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

        public static Instruction GetDynamicObjectPlayTimelineInstruction(string dynamicObjectUuid, string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectPlaySubTimeline>();
            instruction.ParamsRaw.DynamicObjectUuid = dynamicObjectUuid;
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetDynamicObjectPauseTimeline(string dynamicObjectUuid, string path)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectPauseSubTimeline>();
            instruction.ParamsRaw.DynamicObjectUuid = dynamicObjectUuid;
            instruction.ParamsRaw.Path = path;
            return instruction;
        }

        public static Instruction GetSetVariableBoolInstruction(string VariableUuid, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetVariableBool>();
            instruction.ParamsRaw.VariableUuid = VariableUuid;
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
            var instruction = Activator.CreateInstance<InstructionDynamicObjectPlaySubTimeline>();
            instruction.ParamsRaw.DynamicObjectUuid = ConstString.Self;
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetDynamicObjectModelActive(string dynamicObjectUuid, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectSetModelActive>();
            instruction.ParamsRaw.DynamicObjectUuid = dynamicObjectUuid;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetDynamicObjectRunExecute(string dynamicObjectUuid, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectRunExecute>();
            instruction.ParamsRaw.DynamicObjectUuid = dynamicObjectUuid;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }


        public static Instruction GetDynamicObjectHotspotActive(string dynamicObjectUuid, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectHotspotActive>();
            instruction.ParamsRaw.DynamicObjectUuid = dynamicObjectUuid;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetInstructionById(string uuid, InstructionGetRowByUuidHandler handler)
        {
            return InstructionList.BuildInstruction(uuid, handler);
        }


        public static Instruction GetChangeGameSectionInstruction(string uuid)
        {
            var instruction = Activator.CreateInstance<InstructionChangeGameSection>();
            instruction.ParamsRaw.GameSectionUuid = uuid;
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
            var instruction = Activator.CreateInstance<InstructionDynamicObjectPauseSubTimeline>();
            instruction.ParamsRaw.DynamicObjectUuid = ConstString.Self;
            instruction.ParamsRaw.Path = path;
            return instruction;
        }

        public static Instruction GetDynamicObjectTriggerEnabled(string dynamicObjectUuid, string TriggerEventUuid, bool enabled)
        {
            var instruction = Activator.CreateInstance<InstructionSetDOTriggerEnabled>();
            instruction.ParamsRaw.DynamicObjectUuid = dynamicObjectUuid;
            instruction.ParamsRaw.TriggerEventUuid = TriggerEventUuid;
            instruction.ParamsRaw.Enabled = enabled;
            return instruction;
        }

        public static Instruction GetPlaySound(string SoundUuid, bool loop, bool WaitToComplete, float fadeTime)
        {
            var instruction = Activator.CreateInstance<InstructionPlaySound>();
            instruction.ParamsRaw.SoundUuid = SoundUuid;
            instruction.ParamsRaw.Loop = loop;
            instruction.ParamsRaw.WaitToComplete = WaitToComplete;
            instruction.ParamsRaw.FadeTime = fadeTime;
            return instruction;
        }

        public static Instruction GetStopSound(string SoundUuid, float fadeTime)
        {
            var instruction = Activator.CreateInstance<InstructionStopSound>();
            instruction.ParamsRaw.SoundUuid = SoundUuid;
            instruction.ParamsRaw.FadeTime = fadeTime;
            return instruction;
        }

        public static Instruction GetDynamicObjectSubGameObjectEnabled(string dynamicObjectUuid, string path, bool enabled)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectSetSubGameObejctActive>();
            instruction.ParamsRaw.DynamicObjectUuid = dynamicObjectUuid;
            instruction.ParamsRaw.Path = !path.IsNullOrWhiteSpace() ? new string[] { path } : null;
            instruction.ParamsRaw.Val = enabled;
            return instruction;
        }

        public static Instruction GetShowHideCursor(CursorLockMode cursorLockMode)
        {
            var instruction = Activator.CreateInstance<InstructionShowHideCursor>();
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

        public static Instruction GetCheckBoolVariableList(List<string> variableList, string setVariableID)
        {
            var instruction = Activator.CreateInstance<InstructionCheckVariableBoolList>();
            instruction.ParamsRaw.VariableUuidList = variableList;
            instruction.ParamsRaw.SetVariableUuid = setVariableID;
            return instruction;
        }

        public static Instruction GetDynamicObjectInteractEnable(string dynamicObjectUuid, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectInteractEnable>();
            instruction.ParamsRaw.DynamicObjectUuid = dynamicObjectUuid;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetDynamicObjectPreview(string previewUuid, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionUIPreview>();
            instruction.ParamsRaw.PreivewUuid = previewUuid;
            instruction.ParamsRaw.WaitClosed = val;
            return instruction;
        }
        
        public static Instruction GetDynamicObjectInvokeEnter()
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectInvokeEnter>();
            return instruction;
        }
        public static Instruction GetDynamicObjectInvokeExit()
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectInvokeExit>();
            return instruction;
        }
        
        public static Instruction GetTweenLightIntensity(string targetPath,float value,float tweenTime,bool waitFinsh)
        {
            var instruction = Activator.CreateInstance<InstructionTweenLightIntensity>();
            instruction.ParamsRaw.TargetPath = targetPath;
            instruction.ParamsRaw.Value = value;
            instruction.ParamsRaw.TweenTime = tweenTime;
            instruction.ParamsRaw.WaitFinsh = waitFinsh;
            return instruction;
        }
        
        public static Instruction GetDynamicObjectSetMaterial(string targetPath,int index)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectSetMaterial>();
            instruction.ParamsRaw.TargetPath = targetPath;
            instruction.ParamsRaw.Index = index;
            return instruction;
        }

        public Instruction ToInstruction(InstructionGetRowByUuidHandler handler = null)
        {
            switch (InstructionType)
            {
                case DirectInstructionTypeEnum.DynamicObjectPlayTimeline:
                    return GetDynamicObjectPlayTimelineInstruction(Uuid, DropdownString1, Bool1);
                case DirectInstructionTypeEnum.DynamicObjectPauseTimeline:
                    return GetDynamicObjectPauseTimeline(Uuid, DropdownString1);
                case DirectInstructionTypeEnum.ChangeGameSection:
                    return GetChangeGameSectionInstruction(Uuid);
                case DirectInstructionTypeEnum.SetBoolVariable:
                    return GetSetVariableBoolInstruction(Uuid, Bool1);
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
                    return GetDynamicObjectModelActive(Uuid, Bool1);
                case DirectInstructionTypeEnum.DynamicObjectHotspotActive:
                    return GetDynamicObjectHotspotActive(Uuid, Bool1);
                case DirectInstructionTypeEnum.RunInstruction:
                    return GetInstructionById(Uuid, handler);
                case DirectInstructionTypeEnum.ShowSubtitle:
                    return GetShowSubtitleInstruction(String1, Float1);
                case DirectInstructionTypeEnum.CloseSelfTrigger:
                    return GetSelfTriggerEnabledInstruction(false);
                case DirectInstructionTypeEnum.WaitTime:
                    return GetWaitTimeInstruction(Float1);
                case DirectInstructionTypeEnum.SubGameObjectPauseTimeline:
                    return GetSubGameObjectPauseTimeline(DropdownString1);
                case DirectInstructionTypeEnum.DynamicObjectTriggerEnabled:
                    return GetDynamicObjectTriggerEnabled(Uuid, Uuid2, Bool1);
                case DirectInstructionTypeEnum.DynamicObjectRunExecute:
                    return GetDynamicObjectRunExecute(Uuid, Bool1);
                case DirectInstructionTypeEnum.PlaySound:
                    return GetPlaySound(Uuid, Bool1, Bool2, Float1);
                case DirectInstructionTypeEnum.StopSound:
                    return GetStopSound(Uuid, Float1);
                case DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled:
                    return GetDynamicObjectSubGameObjectEnabled(Uuid, DropdownString1, Bool1);
                case DirectInstructionTypeEnum.ImageFade:
                    return GetImageFadeInstruction(String1, Float1, Float2, String2);
                case DirectInstructionTypeEnum.ShowHideCursor:
                    return GetShowHideCursor(CursorLockMode1);
                case DirectInstructionTypeEnum.CanvasGroup:
                    return GetCanvasGroupFadeInstruction(String1, Float1, Float2);
                case DirectInstructionTypeEnum.WaitMsg:
                    return GetWaitMsg(String1);
                case DirectInstructionTypeEnum.DoTweenKill:
                    return GetDoTweenKill(String1);
                case DirectInstructionTypeEnum.SetGlobalGameObjectActive:
                    return GetSetGlobalGameObjectActive(String1, String2, Bool1);
                // case DirectInstructionTypeEnum.CheckBoolVariableList:
                //     return GetCheckBoolVariableList(ListInt1, Int1);
                case DirectInstructionTypeEnum.DynamicObjectInteractEnable:
                    return GetDynamicObjectInteractEnable(Uuid, Bool1);
                case DirectInstructionTypeEnum.DynamicObjectPreview:
                    return GetDynamicObjectPreview(Uuid, Bool1);
                case DirectInstructionTypeEnum.DynamicObjectEnter:
                    return GetDynamicObjectInvokeEnter();
                case DirectInstructionTypeEnum.DynamicObjectExit:
                    return GetDynamicObjectInvokeExit();
                case DirectInstructionTypeEnum.TweenLightIntensity:
                    return GetTweenLightIntensity(DropdownString1,Float1,Float2,Bool1);
                case DirectInstructionTypeEnum.DynamicObjectSetMaterial:
                    return GetDynamicObjectSetMaterial(DropdownString1,Int1);
            }

            return null;
        }
    }

}