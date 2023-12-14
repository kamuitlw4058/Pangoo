using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
using Pangoo.Common;


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

        // [ValueDropdown("OnMainIntValueDropdown")]
        [TableTitleGroup("参数")]
        [LabelText("$String1Label")]
        [ShowIf("$IsMainStringShow")]
        [LabelWidth(50)]
        [JsonMember("String1")]
        public string String1;



        [ValueDropdown("OnMainIntValueDropdown")]
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
        [JsonNoMember]
        // [ReadOnly]
        // [LabelWidth(80)]
        [LabelText("参考预制体")]
        [HideInInspector]
        public GameObject ListPrefab;

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
        [LabelText("$Bool2Label")]
        [ShowIf("$IsBool2Show")]
        [LabelWidth(80)]
        [JsonMember("Bool2")]
        public bool Bool2;

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
                    DirectInstructionTypeEnum.DynamicObjectPlayTimeline => true,
                    DirectInstructionTypeEnum.ChangeGameSection => true,
                    DirectInstructionTypeEnum.SetBoolVariable => true,
                    DirectInstructionTypeEnum.DynamicObjectModelActive => true,
                    DirectInstructionTypeEnum.DynamicObjectHotspotActive => true,
                    DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled => true,
                    DirectInstructionTypeEnum.RunInstruction => true,

                    DirectInstructionTypeEnum.DynamicObjectModelTriggerEnabled => true,
                    DirectInstructionTypeEnum.DynamicObjectRunExecute => true,
                    DirectInstructionTypeEnum.PlaySound => true,
                    DirectInstructionTypeEnum.StopSound => true,
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
                    DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled => true,
                    DirectInstructionTypeEnum.ShowHideCursor => true,
                    DirectInstructionTypeEnum.PlaySound => true,
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
                    DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled => true,

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
                    DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled => "动态物体Id",
                    DirectInstructionTypeEnum.SetGameObjectActive => "参考动态物体",
                    DirectInstructionTypeEnum.PlaySound => "音频Id",
                    DirectInstructionTypeEnum.StopSound => "音频Id",
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
                    DirectInstructionTypeEnum.ActiveCameraGameObject => "等待完成",
                    DirectInstructionTypeEnum.UnactiveCameraGameObject => "等待完成",
                    DirectInstructionTypeEnum.SubGameObjectPlayTimeline => "等待完成",
                    DirectInstructionTypeEnum.PlaySound => "是否循环",
                    DirectInstructionTypeEnum.ShowHideCursor => "显示鼠标光标",
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
                    DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled => "子对象",

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
                    DirectInstructionTypeEnum.PlaySound => "淡入时长",
                    DirectInstructionTypeEnum.StopSound => "淡出时长",
                    DirectInstructionTypeEnum.ImageFade=>"目标Alpha值",
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
                    DirectInstructionTypeEnum.ImageFade=>"过渡时间",
                    _ => "Float1",
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
                    return GameSupportEditorUtility.RefPrefabStringDropdown(ListPrefab);
                case DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled:
                    var prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectId(Int1);
                    return GameSupportEditorUtility.RefPrefabStringDropdown(prefab);

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
                case DirectInstructionTypeEnum.DynamicObjectSubGameObjectEnabled:
                    return GameSupportEditorUtility.GetDynamicObjectIds(true);
                case DirectInstructionTypeEnum.PlaySound:
                case DirectInstructionTypeEnum.StopSound:
                    return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<SoundTableOverview>();
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