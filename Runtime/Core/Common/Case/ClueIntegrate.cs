using System;
using System.Collections.Generic;
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
        [LabelText("更新案件状态")]
        CaseState,
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

        [JsonMember("CaseState")]
        [LabelText("案件状态")]
        [ShowIf("@ResultType == ClueIntegrateResultType.CaseState")]
        public int CaseState;


        [JsonMember("ClueUuid")]
        [LabelText("线索Uuid")]
        [ShowIf("@ResultType != ClueIntegrateResultType.CaseState")]
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
}