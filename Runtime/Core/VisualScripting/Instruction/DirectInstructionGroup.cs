using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using MetaTable;
using UnityEngine;
using Pangoo.MetaTable;


namespace Pangoo.Core.VisualScripting
{


    public struct DirectInstructionGroup
    {
        [JsonMember("Uuid")]
        [ReadOnly]
        public string Uuid;

        [LabelText("触发器类型")]
        // [HideLabel]
        [JsonMember("TriggerType")]
        [BoxGroup("组配置")]
        public TriggerTypeEnum TriggerType;

        [ShowInInspector]
        [LabelText("条件类型")]
        [JsonMember("ConditionType")]
        [BoxGroup("条件配置")]


        public ConditionTypeEnum ConditionType;

        [LabelText("条件Uuid")]
        [BoxGroup("条件配置")]

        [ValueDropdown("ConditionUuidValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.BoolCondition || this.ConditionType == ConditionTypeEnum.StateCondition")]
        [JsonMember("ConditionUuids")]
        public string[] ConditionUuids;



        [JsonMember("DirectInstructionList")]
        [HideReferenceObjectPicker]
        [LabelText("指令列表")]
        [TableList]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.BoolCondition || this.ConditionType == ConditionTypeEnum.NoCondition")]
        [ListDrawerSettings(Expanded = true)]
        [BoxGroup("指令配置")]


        public DirectInstruction[] DirectInstructionList;


        [JsonMember("FailedDirectInstructionList")]
        [HideReferenceObjectPicker]
        [LabelText("失败指令列表")]
        [BoxGroup("指令配置")]

        [TableList]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.BoolCondition")]
        [ListDrawerSettings(Expanded = true)]

        public DirectInstruction[] FailedDirectInstructionList;


        [JsonMember("StateDirectInstructionList")]
        [HideReferenceObjectPicker]
        [LabelText("状态指令列表")]
        [BoxGroup("指令配置")]

        [TableList]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.StateCondition")]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
        public Dictionary<int, DirectInstruction[]> StateDirectInstructionDict;

        [LabelText("默认开启")]
        // [HideLabel]
        // [TableColumnWidth(50, resizable: false)]
        [JsonMember("InitEnabled")]
        [BoxGroup("组配置")]
        public bool InitEnabled;


        // [TableTitleGroup("自动关闭")]
        // [TableColumnWidth(50, resizable: false)]
        [LabelText("运行后自动关闭")]
        [JsonMember("DisableOnFinish")]
        [BoxGroup("组配置")]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.NoCondition")]
        public bool DisableOnFinish;




        public static List<DirectInstructionGroup> CreateList(string s)
        {
            List<DirectInstructionGroup> ret = null;
            if (s.IsNullOrWhiteSpace())
            {
                return new List<DirectInstructionGroup>();
            }
            try
            {
                ret = JsonMapper.ToObject<List<DirectInstructionGroup>>(s); ;
            }
            catch (Exception e)
            {
                ret = new List<DirectInstructionGroup>();
            }

            return ret;
        }

        public static DirectInstructionGroup[] CreateArray(string s)
        {
            return JsonMapper.ToObject<DirectInstructionGroup[]>(s);
        }
#if UNITY_EDITOR

        public void UpdateUuidById()
        {
            if (DirectInstructionList == null) return;
            for (int i = 0; i < DirectInstructionList.Length; i++)
            {
                DirectInstructionList[i].UpdateUuidById();
            }
        }

        public IEnumerable ConditionUuidValueDropdown()
        {
            return ConditionOverview.GetUuidDropdown();
        }
#endif
        public string Save()
        {
            return JsonMapper.ToJson(this);
        }

        public static string Save(DirectInstructionGroup[] directInstructionGroups)
        {

            return JsonMapper.ToJson(directInstructionGroups);

        }
    }

}