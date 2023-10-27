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
    }

    [Serializable]
    public struct DirectInstruction
    {

        [TableTitleGroup("指令类型")]
        [HideLabel]
        [TableColumnWidth(200, resizable: false)]
        [JsonMember("InstructionType")]

        public DirectInstructionTypeEnum InstructionType;

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
        [LabelWidth(50)]
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
                    _ => "Bool1",
                };
            }
        }


        public IEnumerable OnMainIntValueDropdown()
        {
            switch (InstructionType)
            {
                case DirectInstructionTypeEnum.DynamicObjectPlayTimeline:
                    return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<DynamicObjectTableOverview>();
                case DirectInstructionTypeEnum.ChangeGameSection:
                    return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<GameSectionTableOverview>();
                case DirectInstructionTypeEnum.SetBoolVariable:
                    return GameSupportEditorUtility.GetVariableIds(VariableValueTypeEnum.Bool.ToString());
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