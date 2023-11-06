using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;


namespace Pangoo.Core.VisualScripting
{
    public enum DirectInstructionTypeEnum
    {
        Unknown,
        [LabelText("动态物体播放Timeline")]
        DynamicObjectPlayTimeline,

        [LabelText("切换GameSection")]
        ChangeGameSection,

        [LabelText("设置Bool变量")]
        SetBoolVariable,

        [LabelText("设置玩家是否可以被控制")]
        SetPlayerIsControllable,

        [LabelText("设置子GameObject激活")]
        SetGameObjectActive,

        [LabelText("激活相机GameObject")]
        ActiveCameraGameObject,

        [LabelText("关闭相机GameObject")]
        UnactiveCameraGameObject,

        [LabelText("子物体播放Timeline")]
        SubGameObjectPlayTimeline,


        [LabelText("设置动态物体模型的Active")]
        DynamicObjectModelActive,

        [LabelText("动态物体设置Hotspot")]
        DynamicObjectHotspotActive,
    }

    [Serializable]
    public struct DirectInstruction
    {

        [TableTitleGroup("指令类型")]
        [HideLabel]
        [TableColumnWidth(200, resizable: false)]
        [JsonMember("InstructionType")]

        public DirectInstructionTypeEnum InstructionType;

        // [ValueDropdown("OnMainIntValueDropdown")]
        [TableTitleGroup("String参数1")]
        [LabelText("$String1Label")]
        [ShowIf("$IsMainStringShow")]
        [LabelWidth(50)]
        [JsonMember("String1")]
        public string String1;

        [ValueDropdown("OnMainIntValueDropdown")]
        [TableTitleGroup("Int参数1")]
        [LabelText("$Int1Label")]
        [ShowIf("$IsMainIntShow")]
        [LabelWidth(50)]
        [JsonMember("Int1")]
        public int Int1;

        [TableTitleGroup("Bool参数1")]
        [LabelText("$Bool1Label")]
        [ShowIf("$IsMainBoolShow")]
        [LabelWidth(80)]
        [JsonMember("Bool1")]
        public bool Bool1;



#if UNITY_EDITOR
        [JsonNoMember]
        public bool IsMainIntShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.DynamicObjectPlayTimeline => true,
                    DirectInstructionTypeEnum.ChangeGameSection => true,
                    DirectInstructionTypeEnum.SetBoolVariable => true,
                    DirectInstructionTypeEnum.DynamicObjectModelActive => true,
                    DirectInstructionTypeEnum.DynamicObjectHotspotActive => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        public bool IsMainBoolShow
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

                    _ => false,
                };
            }
        }

        [JsonNoMember]
        public bool IsMainStringShow
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.SetGameObjectActive => true,
                    DirectInstructionTypeEnum.ActiveCameraGameObject => true,
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => true,
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => true,
                    _ => false,
                };
            }
        }

        [JsonNoMember]
        public string Int1Label
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

                    _ => "Int1",
                };
            }
        }

        [JsonNoMember]
        public string Bool1Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.SetBoolVariable => "设置值",
                    DirectInstructionTypeEnum.SetGameObjectActive => "设置值",
                    DirectInstructionTypeEnum.SetPlayerIsControllable => "设置值",
                    DirectInstructionTypeEnum.ActiveCameraGameObject => "等待切换完成",
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => "等待切换完成",
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => "等待切换完成",
                    DirectInstructionTypeEnum.DynamicObjectModelActive => "设置值",
                    DirectInstructionTypeEnum.DynamicObjectHotspotActive => "设置值",

                    _ => "Bool1",
                };
            }
        }

        [JsonNoMember]
        public string String1Label
        {
            get
            {
                return InstructionType switch
                {
                    DirectInstructionTypeEnum.SetGameObjectActive => "子对象",
                    DirectInstructionTypeEnum.ActiveCameraGameObject => "子对象",
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => "子对象",
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => "子对象",
                    _ => "String1",
                };
            }
        }


        public IEnumerable OnMainIntValueDropdown()
        {
            switch (InstructionType)
            {
                case DirectInstructionTypeEnum.DynamicObjectPlayTimeline:
                    return GameSupportEditorUtility.GetDynamicObjectIds(true);
                case DirectInstructionTypeEnum.ChangeGameSection:
                    return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<GameSectionTableOverview>();
                case DirectInstructionTypeEnum.SetBoolVariable:
                    return GameSupportEditorUtility.GetVariableIds(VariableValueTypeEnum.Bool.ToString());
                case DirectInstructionTypeEnum.DynamicObjectModelActive:
                    return GameSupportEditorUtility.GetDynamicObjectIds(true);
                case DirectInstructionTypeEnum.DynamicObjectHotspotActive:
                    return GameSupportEditorUtility.GetDynamicObjectIds(true);
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