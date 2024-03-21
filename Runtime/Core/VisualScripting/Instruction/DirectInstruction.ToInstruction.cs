using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using Pangoo.Core.Characters;
using Pangoo.Core.Common;
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


        public static Instruction GetSetVariableIntInstruction(string VariableUuid, int val)
        {
            var instruction = Activator.CreateInstance<InstructionSetVariableInt>();
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

        public static Instruction GetShowHideCursor(CursorTypeEnum e_cursorType)
        {
            var instruction = Activator.CreateInstance<InstructionShowHideCursor>();
            instruction.ParamsRaw.e_CursorType = e_cursorType;
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

        public static Instruction GetTweenLightIntensity(string targetPath, float val, float tweenTime, bool waitFinsh)
        {
            var instruction = Activator.CreateInstance<InstructionTweenLightIntensity>();
            instruction.ParamsRaw.TargetPath = targetPath;
            instruction.ParamsRaw.Val = val;
            instruction.ParamsRaw.TweenTime = tweenTime;
            instruction.ParamsRaw.WaitFinsh = waitFinsh;
            return instruction;
        }

        public static Instruction GetDynamicObjectSetMaterial(string targetPath, int index)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectSetMaterial>();
            instruction.ParamsRaw.TargetPath = targetPath;
            instruction.ParamsRaw.Index = index;
            return instruction;
        }

        public static Instruction GetSetDriverInfo(DriverInfo driverInfo)
        {
            var instruction = Activator.CreateInstance<InstructionSetPlayerDriverInfo>();
            instruction.ParamsRaw.DriverInfo = driverInfo;
            return instruction;
        }

        public static Instruction GetChangeCharacterHeightByDynamicObjectDistance(float startDistance, float endDistance, float minHeight, float maxHeight)
        {
            var instruction = Activator.CreateInstance<InstructionChangeHightByDynamicObjectDistance>();
            instruction.ParamsRaw.EndDistance = endDistance;
            instruction.ParamsRaw.StartDistance = startDistance;
            instruction.ParamsRaw.StartHeight = minHeight;
            instruction.ParamsRaw.EndHeight = maxHeight;
            return instruction;
        }

        public static Instruction GetDynamicObjectSetAnimatorBoolParams(string targetPath, string paramsName, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectSetAnimatorBoolParams>();
            instruction.ParamsRaw.TargetPath = targetPath;
            instruction.ParamsRaw.ParamsName = paramsName;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetSetPlayerInputMotion(InputMotionType inputMotionType)
        {
            var instruction = Activator.CreateInstance<InstructionSetPlayerInputMotion>();
            instruction.ParamsRaw.InputMotionType = inputMotionType;
            return instruction;
        }

        public static Instruction GetSetLocalBoolVariable(string dynamicObjectUuid, string localVariableUuid, bool value)
        {
            var instruction = Activator.CreateInstance<InstructionSetLocalBoolVariable>();
            instruction.ParamsRaw.DynamicObjectUuid = dynamicObjectUuid;
            instruction.ParamsRaw.LocalVariableUuid = localVariableUuid;
            instruction.ParamsRaw.Value = value;
            return instruction;
        }

        public static Instruction GetSetLocalIntVariable(string dynamicObjectUuid, string localVariableUuid, int value)
        {
            var instruction = Activator.CreateInstance<InstructionSetLocalIntVariable>();
            instruction.ParamsRaw.DynamicObjectUuid = dynamicObjectUuid;
            instruction.ParamsRaw.LocalVariableUuid = localVariableUuid;
            instruction.ParamsRaw.Value = value;
            return instruction;
        }

        public static Instruction GetDynamicObjectInvokeLeftMouseDrag()
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectInvokeMouseDrag>();
            return instruction;
        }

        public static Instruction GetChangeImageSprite(string targetName, DynamicObjectHotsoptState state)
        {
            var instruction = Activator.CreateInstance<InstructionChangeHotspotSprite>();
            instruction.ParamsRaw.TargetName = targetName;
            instruction.ParamsRaw.State = state;
            return instruction;
        }

        public static Instruction GetStartDialogue(string dialogueUuid, bool val, bool playerControllable, bool stopDialogueWhenFinish, bool ShowCursor)
        {
            var instruction = Activator.CreateInstance<InstructionUIStartDialogue>();
            instruction.ParamsRaw.DialogueUuid = dialogueUuid;
            instruction.ParamsRaw.WaitClosed = val;
            instruction.ParamsRaw.DontControllPlayer = playerControllable;
            instruction.ParamsRaw.StopDialogueWhenFinish = stopDialogueWhenFinish;
            instruction.ParamsRaw.ShowCursor = ShowCursor;
            return instruction;
        }

        public static Instruction GetSceneModelShow(string uuid, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSceneShow>();
            instruction.ParamsRaw.SceneUuid = uuid;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetSetPlayerEnabledHotspot(bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetPlayerEnabledHotspot>();
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public static Instruction GetManualTimeline(string path, float timeFactor)
        {
            var instruction = Activator.CreateInstance<InstructionManualTimeline>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.TimeFactor = timeFactor;
            return instruction;
        }
        public static Instruction GetTweenRotation(string path, Vector3 rotation, float duration)
        {
            var instruction = Activator.CreateInstance<InstructionTweenRotation>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Rotation = rotation;
            instruction.ParamsRaw.Duration = duration;
            return instruction;
        }

        public static Instruction GetIntDelta(string uuid, int deltaValue)
        {
            var instruction = Activator.CreateInstance<InstructionSetVariableIntDelta>();
            instruction.ParamsRaw.VariableUuid = uuid;
            instruction.ParamsRaw.DeltaValue = deltaValue;
            return instruction;
        }

        public static Instruction GetColliderTriggerActive(string uuid, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectColliderTriggerActive>();
            instruction.ParamsRaw.DynamicObjectUuid = uuid;
            instruction.ParamsRaw.Val = val;
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
                case DirectInstructionTypeEnum.SetIntVariable:
                    return GetSetVariableIntInstruction(Uuid, Int1);
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
                    return GetShowHideCursor(CursorTypeEnum1);
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
                    return GetTweenLightIntensity(DropdownString1, Float1, Float2, Bool1);
                case DirectInstructionTypeEnum.DynamicObjectSetMaterial:
                    return GetDynamicObjectSetMaterial(DropdownString1, Int1);
                case DirectInstructionTypeEnum.SetDriverInfo:
                    return GetSetDriverInfo(DriverInfo1);
                case DirectInstructionTypeEnum.ChangeCharacterHeightByDynamicObjectDistance:
                    return GetChangeCharacterHeightByDynamicObjectDistance(Float1, Float2, Float3, Float4);
                case DirectInstructionTypeEnum.DynamicObjectSetAnimatorBoolParams:
                    return GetDynamicObjectSetAnimatorBoolParams(DropdownString1, String2, Bool1);
                case DirectInstructionTypeEnum.SetPlayerInputMotion:
                    return GetSetPlayerInputMotion(InputMotionType);
                case DirectInstructionTypeEnum.SetLocalBoolVariable:
                    return GetSetLocalBoolVariable(Uuid, Uuid2, Bool1);
                case DirectInstructionTypeEnum.SetLocalIntVariable:
                    return GetSetLocalIntVariable(Uuid, Uuid2, Int1);
                case DirectInstructionTypeEnum.DynamicObjectMouseDrag:
                    return GetDynamicObjectInvokeLeftMouseDrag();
                case DirectInstructionTypeEnum.ChangeHotspotState:
                    return GetChangeImageSprite(DropdownString1, DynamicObjectHotsoptState);
                case DirectInstructionTypeEnum.StartDialogue:
                    return GetStartDialogue(Uuid, Bool1, Bool2, Bool3, Bool4);
                case DirectInstructionTypeEnum.ShowSceneModel:
                    return GetSceneModelShow(Uuid, Bool1);
                case DirectInstructionTypeEnum.ManualTimeline:
                    return GetManualTimeline(DropdownString1, Float1);
                case DirectInstructionTypeEnum.TweenRotation:
                    return GetTweenRotation(DropdownString1, Vector3_1, Float1);
                case DirectInstructionTypeEnum.SetIntDelta:
                    return GetIntDelta(Uuid, Int1);
                case DirectInstructionTypeEnum.DynamicObjectSetColliderTriggerActive:
                    return GetColliderTriggerActive(Uuid, Bool1);

            }

            return null;
        }
    }

}