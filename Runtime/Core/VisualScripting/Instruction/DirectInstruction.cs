using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public partial struct DirectInstruction
    {

        [TableTitleGroup("指令类型")]
        [HideLabel]
        [TableColumnWidth(200, resizable: false)]
        [JsonMember("InstructionType")]

        public DirectInstructionTypeEnum InstructionType;

        // [ValueDropdown("OnMainIntValueDropdown")]
        [TableTitleGroup("参数")]
        [LabelText("$String1Label")]
        [ShowIf("$IsMainStringShow")]
        [LabelWidth(50)]
        [JsonMember("String1")]
        public string String1;



        [ValueDropdown("OnMainIntValueDropdown")]
        [OnValueChanged("OnMainIntValueChanged")]
        [TableTitleGroup("参数")]
        [LabelText("$Int1Label")]
        [ShowIf("$IsMainIntShow")]
        [LabelWidth(50)]
        [JsonMember("Int1")]
        public int Int1;


        [ValueDropdown("OnInt2ValueDropdown")]
        [TableTitleGroup("参数")]
        [LabelText("$Int2Label")]
        [ShowIf("$IsInt2Show")]
        [LabelWidth(50)]
        [JsonMember("Int2")]
        public int Int2;


        [TableTitleGroup("参数")]
        [LabelText("$GameObject1Label")]
        [LabelWidth(50)]
        [JsonNoMember]
        [HideInInspector]
        public GameObject Prefab;

        [TableTitleGroup("参数")]
        [LabelText("$DropdownString1Label")]
        [ShowIf("$IsDropdownStringShow")]
        [LabelWidth(50)]
        [JsonMember("DropdownString1")]
        [ValueDropdown("OnDropdownStringValueDropdown")]

        public string DropdownString1;

        [TableTitleGroup("参数")]
        [LabelText("$Bool1Label")]
        [ShowIf("$IsMainBoolShow")]
        [LabelWidth(80)]
        [JsonMember("Bool1")]
        public bool Bool1;

        [TableTitleGroup("参数")]
        [LabelText("$Float1Label")]
        [ShowIf("$IsMainFloatShow")]
        [LabelWidth(80)]
        [JsonMember("Float1")]
        public float Float1;



#if UNITY_EDITOR
        [JsonNoMember]
        bool IsMainIntShow
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
                    DirectInstructionTypeEnum.RunInstruction => true,
                    DirectInstructionTypeEnum.ActiveCameraGameObject => true,
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => true,
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => true,
                    DirectInstructionTypeEnum.SubGameObjectPauseTimeline => true,
                    DirectInstructionTypeEnum.DynamicObjectModelTriggerEnabled => true,
                    DirectInstructionTypeEnum.DynamicObjectRunExecute => true,
                    DirectInstructionTypeEnum.SetGameObjectActive => true,

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
                    DirectInstructionTypeEnum.DynamicObjectModelTriggerEnabled => true,
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
                    DirectInstructionTypeEnum.DynamicObjectModelTriggerEnabled => true,
                    DirectInstructionTypeEnum.DynamicObjectRunExecute => true,


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
                    DirectInstructionTypeEnum.DynamicObjectModelTriggerEnabled => "动态物体Id",
                    DirectInstructionTypeEnum.DynamicObjectRunExecute => "动态物体Id",
                    DirectInstructionTypeEnum.SetGameObjectActive => "参考动态物体",
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
                    DirectInstructionTypeEnum.DynamicObjectModelTriggerEnabled => "触发器Id",
                    _ => "Int1",
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
                    DirectInstructionTypeEnum.ActiveCameraGameObject => "等待切换完成",
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => "等待切换完成",
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => "等待切换完成",
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
                    _ => "String1",
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
                    DirectInstructionTypeEnum.ActiveCameraGameObject => "子对象",
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => "子对象",
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => "子对象",
                    DirectInstructionTypeEnum.SubGameObjectPauseTimeline => "子对象",
                    DirectInstructionTypeEnum.SetGameObjectActive => "子对象",
                    _ => "DropdownString1",
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
                    _ => "Float1",
                };
            }
        }



        public void OnMainIntValueChanged()
        {
            switch (InstructionType)
            {
                case DirectInstructionTypeEnum.ActiveCameraGameObject:
                case DirectInstructionTypeEnum.UnactiveCameraGameObject:
                case DirectInstructionTypeEnum.SubGameObjectPlayTimeline:
                case DirectInstructionTypeEnum.SubGameObjectPauseTimeline:
                case DirectInstructionTypeEnum.SetGameObjectActive:
                case DirectInstructionTypeEnum.DynamicObjectRunExecute:
                    Prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectId(Int1);
                    break;
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
                    return GameSupportEditorUtility.RefPrefabStringDropdown(Prefab);
            }

            return null;
        }



        public IEnumerable OnMainIntValueDropdown()
        {
            switch (InstructionType)
            {

                case DirectInstructionTypeEnum.ChangeGameSection:
                    return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<GameSectionTableOverview>();
                case DirectInstructionTypeEnum.SetBoolVariable:
                    return GameSupportEditorUtility.GetVariableIds(VariableValueTypeEnum.Bool.ToString());
                case DirectInstructionTypeEnum.RunInstruction:
                    return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<InstructionTableOverview>();
                case DirectInstructionTypeEnum.DynamicObjectPlayTimeline:
                case DirectInstructionTypeEnum.DynamicObjectModelActive:
                case DirectInstructionTypeEnum.DynamicObjectHotspotActive:
                case DirectInstructionTypeEnum.ActiveCameraGameObject:
                case DirectInstructionTypeEnum.UnactiveCameraGameObject:
                case DirectInstructionTypeEnum.SubGameObjectPlayTimeline:
                case DirectInstructionTypeEnum.SubGameObjectPauseTimeline:
                case DirectInstructionTypeEnum.DynamicObjectModelTriggerEnabled:
                case DirectInstructionTypeEnum.SetGameObjectActive:
                case DirectInstructionTypeEnum.DynamicObjectRunExecute:
                    return GameSupportEditorUtility.GetDynamicObjectIds(true);
            }

            return null;
        }

        public IEnumerable OnInt2ValueDropdown()
        {
            switch (InstructionType)
            {
                case DirectInstructionTypeEnum.DynamicObjectModelTriggerEnabled:
                    List<int> includeIds = null;
                    if (Int1 != 0)
                    {
                        var row = GameSupportEditorUtility.GetDynamicObjectRow(Int1);
                        if (row != null)
                        {
                            includeIds = row.GetTriggerEventIdList();
                        }
                    }
                    return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<TriggerEventTableOverview>(includeIds: includeIds);
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