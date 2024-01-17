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

        [JsonMember("Name")]
        public string Name;

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


        [LabelText("使用变量条件")]
        [JsonMember("UseVariableCondition")]
        [BoxGroup("条件配置")]
        public bool UseVariableCondition;

        [LabelText("条件Uuid")]
        [BoxGroup("条件配置")]

        [ValueDropdown("ConditionUuidValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(ShowFoldout = true)]
        [ShowInInspector]
        [ShowIf("@(this.ConditionType == ConditionTypeEnum.BoolCondition || this.ConditionType == ConditionTypeEnum.StateCondition) && !this.UseVariableCondition")]
        [JsonMember("ConditionUuids")]
        public string[] ConditionUuids;

        [ValueDropdown("VariableBoolUuidValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(ShowFoldout = true)]
        [ShowInInspector]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.BoolCondition  && this.UseVariableCondition")]
        [JsonMember("BoolVariableUuds")]
        public string[] BoolVariableUuds;


        [ValueDropdown("VariableIntUuidValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(ShowFoldout = true)]
        [ShowInInspector]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.StateCondition  && this.UseVariableCondition")]
        [JsonMember("IntVariableUuid")]
        public string IntVariableUuid;

        [JsonMember("UseStringTarget")]
        [BoxGroup("目标配置")]
        [LabelText("使用字符串目标")]
        public bool UseStringTarget;


        [JsonMember("Targets")]
        [BoxGroup("目标配置")]
        [LabelText("目标列表")]
        [ValueDropdown("PrefabPathDropdown")]
        [ShowIf("@!this.UseStringTarget")]
        public string[] Targets;


        [JsonMember("StringTargets")]
        [BoxGroup("目标配置")]
        [LabelText("目标字符串列表")]
        [ShowIf("@this.UseStringTarget")]
        public string[] StringTargets;

        [BoxGroup("目标配置")]
        [JsonMember("TargetProcessType")]
        [LabelText("目标列表处理方式")]

        public TriggerTargetListProcessTypeEnum TargetProcessType;


        [JsonMember("DirectInstructionList")]
        [HideReferenceObjectPicker]
        [LabelText("指令列表")]
        [TableList]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.BoolCondition || this.ConditionType == ConditionTypeEnum.NoCondition")]
        [ListDrawerSettings(ShowFoldout = true)]
        [BoxGroup("指令配置")]


        public DirectInstruction[] DirectInstructionList;


        [JsonMember("FailedDirectInstructionList")]
        [HideReferenceObjectPicker]
        [LabelText("失败指令列表")]
        [BoxGroup("指令配置")]

        [TableList]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.BoolCondition")]
        [ListDrawerSettings(ShowFoldout = true)]

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


        [LabelText("运行后自动关闭Trigger")]
        [JsonMember("DisableOnFinish")]
        [BoxGroup("组配置")]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.NoCondition")]
        public bool DisableOnFinish;


        [LabelText("运行后自动关闭交互")]
        [JsonMember("DisableInteractOnFinish")]
        [BoxGroup("组配置")]


        [ShowIf("@this.ConditionType == ConditionTypeEnum.NoCondition")]
        public bool DisableInteractOnFinish;


        [LabelText("运行后自动关闭Hotspot")]
        [JsonMember("DisableHotspotOnFinish")]
        [BoxGroup("组配置")]


        [ShowIf("@this.ConditionType == ConditionTypeEnum.NoCondition")]
        public bool DisableHotspotOnFinish;




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

        [JsonNoMember]
        // [HideInInspector]
        public GameObject Prefab;
#if UNITY_EDITOR

        public void SetPrefab(GameObject go)
        {
            Prefab = go;
        }
        IEnumerable PrefabPathDropdown()
        {
            return GameSupportEditorUtility.RefPrefabStringDropdown(Prefab);
        }

        public IEnumerable ConditionUuidValueDropdown()
        {
            return ConditionOverview.GetConditionUuidDropdown(ConditionType);
        }

        public IEnumerable VariableBoolUuidValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString());
        }

        public IEnumerable VariableIntUuidValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Int.ToString());
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