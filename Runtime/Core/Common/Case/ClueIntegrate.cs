using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
using LitJson;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Common
{
    public enum ClueIntegrateResultType
    {
        [LabelText("更新案件Bool变量")]
        CaseVariableBool,
        [LabelText("新线索")]
        NewClue,
        [LabelText("移除线索")]
        RemoveClue,
    }

    [Serializable]
    public struct ClueIntegrateResult
    {
        [JsonMember("ResultType")]
        [LabelText("结果类型")]
        public ClueIntegrateResultType ResultType;

        [JsonMember("VariableUuid")]
        [LabelText("案件变量")]
        [ShowIf("@ResultType == ClueIntegrateResultType.CaseVariableBool")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.Global.ToString(), false)")]
        public string VariableUuid;

        [JsonMember("VariableValue")]
        [LabelText("案件变量设置值")]
        [ShowIf("@ResultType == ClueIntegrateResultType.CaseVariableBool")]
        public bool VariableValue;


        [JsonMember("ClueUuid")]
        [LabelText("线索Uuid")]
        [ShowIf("@ResultType != ClueIntegrateResultType.CaseVariableBool")]
        [ValueDropdown("@ClueOverview.GetUuidDropdown()")]
        public string ClueUuid;

    }

    [Serializable]
    public class ClueIntegrate
    {
        [JsonMember("Targets")]
        [LabelText("目标线索")]
        [ValueDropdown("@ClueOverview.GetUuidDropdown()")]
        public string[] Targets = new string[0];


        [JsonMember("Results")]
        [LabelText("合成结果")]
        public ClueIntegrateResult[] Results = new ClueIntegrateResult[0];

    }

    [Serializable]
    public class CaseStateCheckItem
    {
        [ValueDropdown("GetOptionVariables")]
        [JsonMember("VariableUuids")]
        [LabelText("案件变量")]
        [ListDrawerSettings(DefaultExpandedState = true)]
        public string[] VariableUuids = new string[0];

        [JsonNoMember]
        [HideInInspector]
        public string[] OptionVariables = new string[0];

        [JsonMember("State")]
        [LabelText("输出状态")]
        public int State;

        [JsonMember("Note")]
        [LabelText("备注")]
        public string Note;

        IEnumerable GetOptionVariables()
        {
            List<Tuple<string, string>> includeUuids = new List<Tuple<string, string>>();
            foreach (var variableUuid in OptionVariables)
            {
                includeUuids.Add(new Tuple<string, string>(variableUuid, string.Empty));
            }
            return VariablesOverview.GetUuidDropdown(includeUuids: includeUuids);
        }

    }


    public enum CaseShowType
    {
        [LabelText("状态显示")]
        State,
        [LabelText("变量显示")]
        Variable
    }

    [Serializable]
    public class CaseVariableCheckItem
    {
        [ValueDropdown("GetOptionVariables")]
        [JsonMember("VariableUuids")]
        [LabelText("案件变量")]
        public string VariableUuid = string.Empty;

        [JsonNoMember]
        [HideInInspector]
        public string[] OptionVariables = new string[0];

        [JsonMember("ControlChildList")]
        [ValueDropdown("GetPrefabPath")]
        [LabelText("控制子物体列表")]
        public string[] ControlChildList = new string[0];


#if UNITY_EDITOR
        [JsonNoMember]
        [HideInInspector]
        public GameObject Prefab;

#endif

        [JsonMember("Note")]
        [LabelText("备注")]
        public string Note;

        IEnumerable GetOptionVariables()
        {
            List<Tuple<string, string>> includeUuids = new List<Tuple<string, string>>();
            foreach (var variableUuid in OptionVariables)
            {
                includeUuids.Add(new Tuple<string, string>(variableUuid, string.Empty));
            }
            return VariablesOverview.GetUuidDropdown(includeUuids: includeUuids);
        }

        IEnumerable GetPrefabPath()
        {
            return GameSupportEditorUtility.RefPrefabStringDropdown(Prefab, hasSelf: false);
        }

    }
}