using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
using Pangoo.Common;
using Pangoo.MetaTable;
using MetaTable;
using Pangoo.Core.Characters;
using Pangoo.Core.Common;
using UnityEngine.Serialization;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public partial struct DirectInstruction
    {



        [TableTitleGroup("指令类型")]
        [HideLabel]
        [TableColumnWidth(200, resizable: false)]
        [JsonMember("InstructionType")]
        [GUIColor(0.3f, 0.9f, 0.3f)]
        public DirectInstructionTypeEnum InstructionType;


        public InstructionType InstructionRuntimeType
        {
            get
            {
                switch (InstructionType)
                {
                    case DirectInstructionTypeEnum.SetBoolVariable:
                    case DirectInstructionTypeEnum.ShowSubtitle:
                        return VisualScripting.InstructionType.Immediate;
                }
                return Pangoo.Core.VisualScripting.InstructionType.Coroutine;
            }
        }

        [TableTitleGroup("参数")]
        [LabelText("$Uuid1Label")]
        [ShowIf("$IsUuidShow")]
        [LabelWidth(50)]
        [JsonMember("Uuid")]
        [ValueDropdown("OnUuidDropdown")]
        public string Uuid;

        [TableTitleGroup("参数")]
        [LabelText("$Uuid2Label")]
        [ShowIf("$IsUuid2Show")]
        [LabelWidth(50)]
        [JsonMember("Uuid2")]
        [ValueDropdown("OnUuid2Dropdown")]
        public string Uuid2;

        [TableTitleGroup("参数")]
        [LabelText("$DropdownString1Label")]
        [ShowIf("$IsDropdownStringShow")]
        [LabelWidth(50)]
        [JsonMember("DropdownString1")]
        [ValueDropdown("OnDropdownStringValueDropdown")]
        public string DropdownString1;

        [TableTitleGroup("参数")]
        [LabelText("$String1Label")]
        [ShowIf("$IsMainStringShow")]
        [LabelWidth(50)]
        [JsonMember("String1")]
        public string String1;

        [TableTitleGroup("参数")]
        [LabelText("$String2Label")]
        [ShowIf("$IsString2Show")]
        [LabelWidth(80)]
        [JsonMember("String2")]
        public string String2;


        [TableTitleGroup("参数")]
        [LabelText("$ListInt1Label")]
        [ShowIf("$IsMainListIntShow")]
        [LabelWidth(80)]
        [JsonMember("ListInt1")]
        public List<int> ListInt1;

        [TableTitleGroup("参数")]
        [LabelText("$Int1Label")]
        [ShowIf("$IsMainIntShow")]
        [LabelWidth(80)]
        [JsonMember("Int1")]
        public int Int1;



        [TableTitleGroup("参数")]
        [LabelText("$Uuid3Label")]
        [ShowIf("$IsUuid3Show")]
        [LabelWidth(50)]
        [JsonMember("Uuid3")]
        [ValueDropdown("OnUuid3Dropdown")]
        public string Uuid3;

        // [ValueDropdown("OnInt2ValueDropdown")]
        [TableTitleGroup("参数")]
        [LabelText("$Int2Label")]
        [ShowIf("$IsInt2Show")]
        [LabelWidth(50)]
        [JsonMember("Int2")]
        public int Int2;


        [TableTitleGroup("参数")]
        [JsonNoMember]
        // [ReadOnly]
        // [LabelWidth(80)]
        [LabelText("参考预制体")]
        [HideInInspector]
        public GameObject ListPrefab;



        [TableTitleGroup("参数")]
        [LabelText("$Bool1Label")]
        [ShowIf("$IsMainBoolShow")]
        [LabelWidth(80)]
        [JsonMember("Bool1")]
        public bool Bool1;

        [TableTitleGroup("参数")]
        [LabelText("$Bool2Label")]
        [ShowIf("$IsBool2Show")]
        [LabelWidth(80)]
        [JsonMember("Bool2")]
        public bool Bool2;

        [TableTitleGroup("参数")]
        [LabelText("$Bool3Label")]
        [ShowIf("$IsBool3Show")]
        [LabelWidth(80)]
        [JsonMember("Bool3")]
        public bool Bool3;

        [TableTitleGroup("参数")]
        [LabelText("$Bool4Label")]
        [ShowIf("$IsBool4Show")]
        [LabelWidth(80)]
        [JsonMember("Bool4")]
        public bool Bool4;

        [TableTitleGroup("参数")]
        [LabelText("$Float1Label")]
        [ShowIf("$IsMainFloatShow")]
        [LabelWidth(80)]
        [JsonMember("Float1")]
        public float Float1;

        [TableTitleGroup("参数")]
        [LabelText("$Float2Label")]
        [ShowIf("$IsFloat2Show")]
        [LabelWidth(80)]
        [JsonMember("Float2")]
        public float Float2;

        [TableTitleGroup("参数")]
        [LabelText("$Float3Label")]
        [ShowIf("$IsFloat3Show")]
        [LabelWidth(80)]
        [JsonMember("Float3")]
        public float Float3;
        [TableTitleGroup("参数")]
        [LabelText("$Float4Label")]
        [ShowIf("$IsFloat4Show")]
        [LabelWidth(80)]
        [JsonMember("Float4")]
        public float Float4;
        [TableTitleGroup("参数")]
        [LabelText("$Vector3_1Label")]
        [ShowIf("$IsVector3_1Show")]
        [LabelWidth(80)]
        [JsonMember("Vector3_1")]
        public Vector3 Vector3_1;
        [TableTitleGroup("参数")]
        [LabelText("$Vector3_2Label")]
        [ShowIf("$IsVector3_2Show")]
        [LabelWidth(80)]
        [JsonMember("Vector3_2")]
        public Vector3 Vector3_2;

        [FormerlySerializedAs("CursorLockMode1")]
        [TableTitleGroup("参数")]
        //[LabelText("$CursorLockMode1Label")]
        [ShowIf("$IsMainCursorLockModeShow")]
        [LabelWidth(120)]
        [JsonMember("CursorLockMode1")]
        public CursorTypeEnum CursorTypeEnum1;

        [TableTitleGroup("参数")]
        [ShowIf("$IsMainDriverInfoShow")]
        [LabelWidth(120)]
        [JsonMember("DriverInfo1")]
        public DriverInfo DriverInfo1;

        [TableTitleGroup("参数")]
        [ShowIf("$IsMainInputMotionTypeShow")]
        [LabelWidth(120)]
        [JsonMember("InputMotionType")]
        public InputMotionType InputMotionType;

        [TableTitleGroup("参数")]
        [ShowIf("$IsDynamicObjectHotsoptStateShow")]
        [LabelWidth(120)]
        [JsonMember("DynamicObjectHotsoptState")]
        [LabelText("HotsoptState")]
        public DynamicObjectHotsoptState DynamicObjectHotsoptState;

        [TableTitleGroup("参数")]
        [ShowIf("$IsTimelineOperationTypeShow")]
        [LabelWidth(120)]
        [JsonMember("TimelineOperationType")]
        [LabelText("$TimelineOperationTypeLabel")]
        public TimelineOperationTypeEnum TimelineOperationType;

#if UNITY_EDITOR
        public void SetPrefab(GameObject go)
        {
            ListPrefab = go;
        }

        [JsonNoMember]
        bool IsMainIntShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.DynamicObjectSetMaterial => true,
                    DirectInstructionTypeEnum.SetIntVariable => true,
                    DirectInstructionTypeEnum.SetLocalIntVariable => true,
                    DirectInstructionTypeEnum.SetIntDelta => true,
                    DirectInstructionTypeEnum.SetGameSectionIntVariable => true,
                    DirectInstructionTypeEnum.DynamicObjectAnimatorInt => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsUuidShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.DynamicObjectPlayTimeline => true,
                    DirectInstructionTypeEnum.DynamicObjectPauseTimeline => true,

                    DirectInstructionTypeEnum.ChangeGameSection => true,
                    DirectInstructionTypeEnum.SetBoolVariable => true,
                    DirectInstructionTypeEnum.SetLocalBoolVariable => true,
                    DirectInstructionTypeEnum.SetIntVariable => true,
                    DirectInstructionTypeEnum.DynamicObjectModelActive => true,
                    DirectInstructionTypeEnum.DynamicObjectHotspotActive => true,
                    DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled => true,
                    DirectInstructionTypeEnum.DynamicObjectInteractEnable => true,

                    DirectInstructionTypeEnum.RunInstruction => true,

                    DirectInstructionTypeEnum.DynamicObjectTriggerEnabled => true,
                    DirectInstructionTypeEnum.DynamicObjectRunExecute => true,
                    DirectInstructionTypeEnum.PlaySound => true,
                    DirectInstructionTypeEnum.StopSound => true,
                    DirectInstructionTypeEnum.CheckBoolVariableList => true,
                    DirectInstructionTypeEnum.DynamicObjectPreview => true,
                    DirectInstructionTypeEnum.WaitVariableBool => true,
                    DirectInstructionTypeEnum.SetLocalIntVariable => true,
                    DirectInstructionTypeEnum.SetLocalVector3Variable => true,
                    DirectInstructionTypeEnum.StartDialogue => true,
                    DirectInstructionTypeEnum.ShowSceneModel => true,
                    DirectInstructionTypeEnum.SetIntDelta => true,
                    DirectInstructionTypeEnum.DynamicObjectSetColliderTriggerActive => true,
                    DirectInstructionTypeEnum.DynamicObjectRotationSetVariable => true,
                    DirectInstructionTypeEnum.CyclePlaySound => true,
                    DirectInstructionTypeEnum.ShowCase => true,
                    DirectInstructionTypeEnum.SetGameSectionIntVariable => true,
                    DirectInstructionTypeEnum.DynamicObjectAnimatorInt => true,

                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsInt2Show
        {
            get
            {
                return InstructionType switch
                {
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsUuid2Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.DynamicObjectTriggerEnabled => true,
                    DirectInstructionTypeEnum.SetLocalBoolVariable => true,
                    DirectInstructionTypeEnum.SetLocalIntVariable => true,
                    DirectInstructionTypeEnum.SetLocalVector3Variable => true,
                    DirectInstructionTypeEnum.CyclePlaySound => true,
                    DirectInstructionTypeEnum.SetGameSectionIntVariable => true,

                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsUuid3Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.CyclePlaySound => true,
                    _ => false,
                };
            }
        }


        [JsonNoMember]
        bool IsMainListIntShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.CheckBoolVariableList => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsMainBoolShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.SetBoolVariable => true,
                    DirectInstructionTypeEnum.SetPlayerIsControllable => true,
                    DirectInstructionTypeEnum.SetGameObjectActive => true,
                    DirectInstructionTypeEnum.ActiveCameraGameObject => true,
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => true,
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => true,
                    DirectInstructionTypeEnum.DynamicObjectModelActive => true,
                    DirectInstructionTypeEnum.DynamicObjectHotspotActive => true,
                    DirectInstructionTypeEnum.DynamicObjectTriggerEnabled => true,
                    DirectInstructionTypeEnum.DynamicObjectRunExecute => true,
                    DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled => true,
                    DirectInstructionTypeEnum.DynamicObjectInteractEnable => true,

                    DirectInstructionTypeEnum.PlaySound => true,
                    DirectInstructionTypeEnum.SetGlobalGameObjectActive => true,
                    DirectInstructionTypeEnum.DynamicObjectPreview => true,
                    DirectInstructionTypeEnum.DynamicObjectPlayTimeline => true,
                    DirectInstructionTypeEnum.WaitVariableBool => true,
                    DirectInstructionTypeEnum.TweenLightIntensity => true,
                    DirectInstructionTypeEnum.DynamicObjectSetAnimatorBoolParams => true,
                    DirectInstructionTypeEnum.SetLocalBoolVariable => true,
                    DirectInstructionTypeEnum.StartDialogue => true,
                    DirectInstructionTypeEnum.ShowSceneModel => true,
                    DirectInstructionTypeEnum.DynamicObjectSetColliderTriggerActive => true,
                    DirectInstructionTypeEnum.CyclePlaySound => true,
                    DirectInstructionTypeEnum.DynamicObjectDoRotationToTargetAngle => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsBool2Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.PlaySound => true,
                    DirectInstructionTypeEnum.StartDialogue => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsBool3Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.StartDialogue => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsBool4Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.StartDialogue => true,
                    _ => false,
                };
            }
        }


        [JsonNoMember]
        bool IsMainStringShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ShowSubtitle => true,
                    DirectInstructionTypeEnum.ImageFade => true,
                    DirectInstructionTypeEnum.CanvasGroup => true,
                    DirectInstructionTypeEnum.WaitMsg => true,
                    DirectInstructionTypeEnum.DoTweenKill => true,
                    DirectInstructionTypeEnum.SetGlobalGameObjectActive => true,
                    DirectInstructionTypeEnum.DynamicObjectAnimatorInt => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsString2Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ImageFade => true,
                    DirectInstructionTypeEnum.SetGlobalGameObjectActive => true,
                    DirectInstructionTypeEnum.DynamicObjectSetAnimatorBoolParams => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsDropdownStringShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ActiveCameraGameObject => true,
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => true,
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => true,
                    DirectInstructionTypeEnum.SubGameObjectPauseTimeline => true,
                    DirectInstructionTypeEnum.SetGameObjectActive => true,
                    DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled => true,
                    DirectInstructionTypeEnum.DynamicObjectPlayTimeline => true,
                    DirectInstructionTypeEnum.DynamicObjectPauseTimeline => true,
                    DirectInstructionTypeEnum.TweenLightIntensity => true,
                    DirectInstructionTypeEnum.DynamicObjectSetMaterial => true,
                    DirectInstructionTypeEnum.DynamicObjectSetAnimatorBoolParams => true,
                    DirectInstructionTypeEnum.ChangeHotspotState => true,
                    DirectInstructionTypeEnum.ManualTimeline => true,
                    DirectInstructionTypeEnum.TweenRotation => true,
                    DirectInstructionTypeEnum.DynamicObjectDoRotationToTargetAngle => true,
                    DirectInstructionTypeEnum.DynamicObjectRotationSetVariable => true,
                    DirectInstructionTypeEnum.ChangeTimelineUpdateMode => true,
                    DirectInstructionTypeEnum.ManualTimelineSpeedByMouseSpeed => true,
                    DirectInstructionTypeEnum.DynamicObjectAnimatorInt => true,
                    DirectInstructionTypeEnum.DynamicObjectSetTimelineTimeByMarkerTime => true,

                    _ => false,
                };
            }
        }


        [JsonNoMember]
        bool IsMainFloatShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ShowSubtitle => true,
                    DirectInstructionTypeEnum.WaitTime => true,
                    DirectInstructionTypeEnum.PlaySound => true,
                    DirectInstructionTypeEnum.StopSound => true,
                    DirectInstructionTypeEnum.ImageFade => true,
                    DirectInstructionTypeEnum.CanvasGroup => true,
                    DirectInstructionTypeEnum.TweenLightIntensity => true,
                    DirectInstructionTypeEnum.ChangeCharacterHeightByDynamicObjectDistance => true,
                    DirectInstructionTypeEnum.ManualTimeline => true,
                    DirectInstructionTypeEnum.TweenRotation => true,
                    DirectInstructionTypeEnum.DynamicObjectDoRotationToTargetAngle => true,
                    DirectInstructionTypeEnum.CyclePlaySound => true,
                    DirectInstructionTypeEnum.SetPlayerSpeed => true,
                    DirectInstructionTypeEnum.ChangeTimelineUpdateMode => true,
                    DirectInstructionTypeEnum.ManualTimelineSpeedByMouseSpeed => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsFloat2Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ImageFade => true,
                    DirectInstructionTypeEnum.CanvasGroup => true,
                    DirectInstructionTypeEnum.TweenLightIntensity => true,
                    DirectInstructionTypeEnum.ChangeCharacterHeightByDynamicObjectDistance => true,
                    DirectInstructionTypeEnum.SetPlayerSpeed => true,
                    DirectInstructionTypeEnum.ManualTimelineSpeedByMouseSpeed => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsFloat3Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ChangeCharacterHeightByDynamicObjectDistance => true,
                    _ => false,
                };
            }
        }
        [JsonNoMember]
        bool IsFloat4Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ChangeCharacterHeightByDynamicObjectDistance => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsVector3_1Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.TweenRotation => true,
                    DirectInstructionTypeEnum.DynamicObjectDoRotationToTargetAngle => true,
                    DirectInstructionTypeEnum.SetLocalVector3Variable => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsVector3_2Show
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.TweenRotation => true,
                    DirectInstructionTypeEnum.DynamicObjectDoRotationToTargetAngle => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsMainCursorLockModeShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ShowHideCursor => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsMainDriverInfoShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.SetDriverInfo => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsMainInputMotionTypeShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.SetPlayerInputMotion => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsDynamicObjectHotsoptStateShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ChangeHotspotState => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        bool IsTimelineOperationTypeShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ChangeTimelineUpdateMode => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        string Int1Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.DynamicObjectPlayTimeline => "动态物体Id",
                    DirectInstructionTypeEnum.ChangeGameSection => "GameSectionId",
                    DirectInstructionTypeEnum.SetBoolVariable => "变量Id",
                    DirectInstructionTypeEnum.DynamicObjectModelActive => "动态物体Id",
                    DirectInstructionTypeEnum.DynamicObjectHotspotActive => "动态物体Id",
                    DirectInstructionTypeEnum.RunInstruction => "指令Id",
                    DirectInstructionTypeEnum.ActiveCameraGameObject => "参考动态物体",
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => "参考动态物体",
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => "参考动态物体",
                    DirectInstructionTypeEnum.SubGameObjectPauseTimeline => "参考动态物体",
                    DirectInstructionTypeEnum.DynamicObjectTriggerEnabled => "动态物体Id",
                    DirectInstructionTypeEnum.DynamicObjectRunExecute => "动态物体Id",
                    DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled => "动态物体Id",
                    DirectInstructionTypeEnum.SetGameObjectActive => "参考动态物体",
                    DirectInstructionTypeEnum.PlaySound => "音频Id",
                    DirectInstructionTypeEnum.StopSound => "音频Id",
                    DirectInstructionTypeEnum.CheckBoolVariableList => "设置变量ID",
                    DirectInstructionTypeEnum.DynamicObjectSetMaterial => "列表索引",
                    _ => "Int1",
                };
            }
        }


        [JsonNoMember]
        string Int2Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.DynamicObjectTriggerEnabled => "触发器Id",
                    _ => "值",
                };
            }
        }

        [JsonNoMember]
        string ListInt1Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.CheckBoolVariableList => "检查列表",
                    _ => "ListInt1",
                };
            }
        }

        [JsonNoMember]
        string Uuid1Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.CyclePlaySound => "声音Uuid",
                    DirectInstructionTypeEnum.SetGameSectionIntVariable => "段落Uuid",
                    DirectInstructionTypeEnum.DynamicObjectAnimatorInt => "动态物体Uuid",
                    _ => "Uuid1",
                };
            }
        }
        [JsonNoMember]
        string Uuid2Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.CyclePlaySound => "计时器Uuid",
                    DirectInstructionTypeEnum.SetGameSectionIntVariable => "变量Uuid",
                    _ => "Uuid2",
                };
            }
        }
        [JsonNoMember]
        string Uuid3Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.CyclePlaySound => "播放标记Uuid",
                    _ => "Uuid3",
                };
            }
        }


        [JsonNoMember]
        string Bool1Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ActiveCameraGameObject => "等待完成",
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => "等待完成",
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => "等待完成",
                    DirectInstructionTypeEnum.DynamicObjectPlayTimeline => "等待完成",

                    DirectInstructionTypeEnum.PlaySound => "是否循环",
                    DirectInstructionTypeEnum.SetGlobalGameObjectActive => "状态",
                    DirectInstructionTypeEnum.TweenLightIntensity => "是否等待完成",
                    DirectInstructionTypeEnum.StartDialogue => "等待完成",
                    DirectInstructionTypeEnum.CyclePlaySound => "在开始时播放一次",
                    DirectInstructionTypeEnum.DynamicObjectDoRotationToTargetAngle => "是否使用阻尼",
                    _ => "设置值",
                };
            }
        }

        [JsonNoMember]
        string Bool2Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.PlaySound => "等待切换完成",
                    DirectInstructionTypeEnum.StartDialogue => "不控制玩家",
                    _ => "设置值",
                };
            }
        }

        [JsonNoMember]
        string Bool3Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.StartDialogue => "当结束时暂停对话",
                    _ => "设置值",
                };
            }
        }

        [JsonNoMember]
        string Bool4Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.StartDialogue => "显示鼠标",
                    _ => "设置值",
                };
            }
        }


        [JsonNoMember]
        string String1Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.SetGameObjectActive => "子对象",
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => "子对象",
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => "子对象",
                    DirectInstructionTypeEnum.ShowSubtitle => "字幕内容",
                    DirectInstructionTypeEnum.ImageFade => "目标节点名字",
                    DirectInstructionTypeEnum.CanvasGroup => "目标节点名字",
                    DirectInstructionTypeEnum.WaitMsg => "消息内容",
                    DirectInstructionTypeEnum.DoTweenKill => "TweenID",
                    DirectInstructionTypeEnum.SetGlobalGameObjectActive => "根节点",
                    DirectInstructionTypeEnum.DynamicObjectAnimatorInt => "动画参数名",
                    _ => "String1",
                };
            }
        }

        [JsonNoMember]
        string String2Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ImageFade => "TweenID",
                    DirectInstructionTypeEnum.SetGlobalGameObjectActive => "根节点子对象",
                    DirectInstructionTypeEnum.DynamicObjectSetAnimatorBoolParams => "动画参数",
                    _ => "String2",
                };
            }
        }

        [JsonNoMember]
        string DropdownString1Label
        {
            get
            {
                return InstructionType switch
                {
                    _ => "子对象",
                };
            }
        }

        [JsonNoMember]
        string Float1Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ShowSubtitle => "持续时间",
                    DirectInstructionTypeEnum.WaitTime => "等待时长",
                    DirectInstructionTypeEnum.PlaySound => "淡入时长",
                    DirectInstructionTypeEnum.StopSound => "淡出时长",
                    DirectInstructionTypeEnum.ImageFade => "目标Alpha值",
                    DirectInstructionTypeEnum.CanvasGroup => "目标Alpha值",
                    DirectInstructionTypeEnum.TweenLightIntensity => "目标值",
                    DirectInstructionTypeEnum.ChangeCharacterHeightByDynamicObjectDistance => "起始距离",
                    DirectInstructionTypeEnum.ManualTimeline => "播放速度",
                    DirectInstructionTypeEnum.TweenRotation => "补间时长",
                    DirectInstructionTypeEnum.DynamicObjectDoRotationToTargetAngle => "旋转速度",
                    DirectInstructionTypeEnum.CyclePlaySound => "周期时长",
                    DirectInstructionTypeEnum.SetPlayerSpeed => "走路速度",
                    DirectInstructionTypeEnum.ChangeTimelineUpdateMode => "播放速度",
                    DirectInstructionTypeEnum.ManualTimelineSpeedByMouseSpeed => "速度系数",
                    _ => "Float1",
                };
            }
        }

        [JsonNoMember]
        string Float2Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ImageFade => "过渡时间",
                    DirectInstructionTypeEnum.CanvasGroup => "过渡时间",
                    DirectInstructionTypeEnum.TweenLightIntensity => "过渡时间",
                    DirectInstructionTypeEnum.ChangeCharacterHeightByDynamicObjectDistance => "结束距离",
                    DirectInstructionTypeEnum.SetPlayerSpeed => "跑步速度",
                    DirectInstructionTypeEnum.ManualTimelineSpeedByMouseSpeed => "鼠标最大速度",
                    _ => "Float2",
                };
            }
        }

        [JsonNoMember]
        string Float3Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ChangeCharacterHeightByDynamicObjectDistance => "起始高度",
                    _ => "Float3",
                };
            }
        }

        [JsonNoMember]
        string Float4Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.ChangeCharacterHeightByDynamicObjectDistance => "结束高度",
                    _ => "Float4",
                };
            }
        }

        [JsonNoMember]
        string Vector3_1Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.TweenRotation => "初始旋转值",
                    DirectInstructionTypeEnum.DynamicObjectDoRotationToTargetAngle => "初始旋转值",
                    _ => "Vector3",
                };
            }
        }
        [JsonNoMember]
        string Vector3_2Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.TweenRotation => "目标旋转值",
                    DirectInstructionTypeEnum.DynamicObjectDoRotationToTargetAngle => "目标旋转值",
                    _ => "Vector3",
                };
            }
        }

        [JsonNoMember]
        string TimelineOperationTypeLabel
        {
            get
            {
                return InstructionType switch
                {
                    _ => "Timeline更新模式",
                };
            }
        }

        public IEnumerable OnDropdownStringValueDropdown()
        {
            switch (InstructionType)
            {
                case DirectInstructionTypeEnum.ActiveCameraGameObject:
                case DirectInstructionTypeEnum.UnactiveCameraGameObject:
                case DirectInstructionTypeEnum.SubGameObjectPlayTimeline:
                case DirectInstructionTypeEnum.SubGameObjectPauseTimeline:
                case DirectInstructionTypeEnum.SetGameObjectActive:
                case DirectInstructionTypeEnum.TweenLightIntensity:
                case DirectInstructionTypeEnum.DynamicObjectSetMaterial:
                case DirectInstructionTypeEnum.DynamicObjectSetAnimatorBoolParams:
                case DirectInstructionTypeEnum.ChangeHotspotState:
                case DirectInstructionTypeEnum.ManualTimeline:
                case DirectInstructionTypeEnum.TweenRotation:
                case DirectInstructionTypeEnum.DynamicObjectRotationSetVariable:
                case DirectInstructionTypeEnum.DynamicObjectDoRotationToTargetAngle:
                case DirectInstructionTypeEnum.ChangeTimelineUpdateMode:
                case DirectInstructionTypeEnum.ManualTimelineSpeedByMouseSpeed:
                case DirectInstructionTypeEnum.DynamicObjectSetTimelineTimeByMarkerTime:
                    return GameSupportEditorUtility.RefPrefabStringDropdown(ListPrefab);
                case DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled:
                case DirectInstructionTypeEnum.DynamicObjectPlayTimeline:
                case DirectInstructionTypeEnum.DynamicObjectPauseTimeline:
                case DirectInstructionTypeEnum.DynamicObjectAnimatorInt:
                    var prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(Uuid);
                    return GameSupportEditorUtility.RefPrefabStringDropdown(prefab);

            }

            return null;
        }



        public IEnumerable OnUuidDropdown()
        {
            switch (InstructionType)
            {

                case DirectInstructionTypeEnum.ChangeGameSection:
                case DirectInstructionTypeEnum.SetGameSectionIntVariable:
                    return GameSectionOverview.GetUuidDropdown();
                case DirectInstructionTypeEnum.SetBoolVariable:
                    return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString());
                case DirectInstructionTypeEnum.SetIntVariable:
                case DirectInstructionTypeEnum.SetIntDelta:
                    return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Int.ToString());
                case DirectInstructionTypeEnum.DynamicObjectRotationSetVariable:
                    return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Vector3.ToString());
                case DirectInstructionTypeEnum.RunInstruction:
                    return InstructionOverview.GetUuidDropdown();
                case DirectInstructionTypeEnum.DynamicObjectPlayTimeline:
                case DirectInstructionTypeEnum.DynamicObjectPauseTimeline:
                case DirectInstructionTypeEnum.DynamicObjectModelActive:
                case DirectInstructionTypeEnum.DynamicObjectHotspotActive:
                case DirectInstructionTypeEnum.ActiveCameraGameObject:
                case DirectInstructionTypeEnum.UnactiveCameraGameObject:
                case DirectInstructionTypeEnum.SubGameObjectPlayTimeline:
                case DirectInstructionTypeEnum.SubGameObjectPauseTimeline:
                case DirectInstructionTypeEnum.DynamicObjectTriggerEnabled:
                case DirectInstructionTypeEnum.SetGameObjectActive:
                case DirectInstructionTypeEnum.DynamicObjectRunExecute:
                case DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled:
                case DirectInstructionTypeEnum.DynamicObjectInteractEnable:
                case DirectInstructionTypeEnum.TweenLightIntensity:
                case DirectInstructionTypeEnum.SetLocalBoolVariable:
                case DirectInstructionTypeEnum.SetLocalIntVariable:
                case DirectInstructionTypeEnum.SetLocalVector3Variable:
                case DirectInstructionTypeEnum.DynamicObjectSetColliderTriggerActive:
                case DirectInstructionTypeEnum.DynamicObjectAnimatorInt:
                case DirectInstructionTypeEnum.DynamicObjectSetTimelineTimeByMarkerTime:
                    return DynamicObjectOverview.GetUuidDropdown(AdditionalOptions: new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>("Self","Self"),
                    });
                case DirectInstructionTypeEnum.PlaySound:
                case DirectInstructionTypeEnum.StopSound:
                case DirectInstructionTypeEnum.CyclePlaySound:
                    return SoundOverview.GetUuidDropdown();
                case DirectInstructionTypeEnum.CheckBoolVariableList:
                case DirectInstructionTypeEnum.WaitVariableBool:
                    return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString());
                // return GameSupportEditorUtility.GetVariableIds(VariableValueTypeEnum.Bool.ToString());

                case DirectInstructionTypeEnum.DynamicObjectPreview:
                    return DynamicObjectPreviewOverview.GetUuidDropdown();
                case DirectInstructionTypeEnum.StartDialogue:
                    return DialogueOverview.GetUuidDropdown();
                case DirectInstructionTypeEnum.ShowSceneModel:
                    return StaticSceneOverview.GetUuidDropdown();
                case DirectInstructionTypeEnum.ShowCase:
                    return CasesOverview.GetUuidDropdown();

            }

            return null;
        }


        public IEnumerable OnUuid2Dropdown()
        {
            switch (InstructionType)
            {
                case DirectInstructionTypeEnum.DynamicObjectTriggerEnabled:
                    List<Tuple<string, string>> includeUuids = new();
                    if (!Uuid.IsNullOrWhiteSpace())
                    {
                        var row = DynamicObjectOverview.GetUnityRowByUuid(Uuid);
                        if (row != null)
                        {
                            includeUuids.AddRange(row.Row.GetTriggerEventUuidList().Select(o => new Tuple<string, string>(o, "Trigger")));
                            var directInstructionGroups = DirectInstructionGroup.CreateArray(row.Row.DirectInstructions);
                            if (directInstructionGroups != null)
                            {
                                foreach (var group in directInstructionGroups)
                                {
                                    includeUuids.Add(new Tuple<string, string>(group.Uuid, group.Name));
                                }
                            }

                        }
                    }
                    return TriggerEventOverview.GetUuidDropdown(includeUuids: includeUuids);
                case DirectInstructionTypeEnum.SetLocalBoolVariable:
                    return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.DynamicObject.ToString());
                case DirectInstructionTypeEnum.SetLocalIntVariable:
                    return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Int.ToString(), VariableTypeEnum.DynamicObject.ToString());
                case DirectInstructionTypeEnum.SetLocalVector3Variable:
                    return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Vector3.ToString(), VariableTypeEnum.DynamicObject.ToString());
                case DirectInstructionTypeEnum.SetGameSectionIntVariable:
                    return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Int.ToString(), VariableTypeEnum.GameSection.ToString());
                case DirectInstructionTypeEnum.CyclePlaySound:
                    return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Float.ToString());
            }

            return null;
        }

        public IEnumerable OnUuid3Dropdown()
        {
            switch (InstructionType)
            {
                case DirectInstructionTypeEnum.CyclePlaySound:
                    return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString());
            }

            return null;
        }


#endif

        [JsonNoMember]
        public bool IsVaild
        {
            get
            {

                if (InstructionType == DirectInstructionTypeEnum.Unknown)
                {
                    return false;
                }


                return true;
            }
        }

    }

}