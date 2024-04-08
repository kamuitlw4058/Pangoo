// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class CasesRow : MetaTableRow,ICasesRow
    {

        [JsonMember("DynamicObjectUuid")]
        [MetaTableRowColumn("DynamicObjectUuid","string", "动态物体Uuid",1)]
        [LabelText("动态物体Uuid")]
        public string DynamicObjectUuid ;

        string ICasesRow.DynamicObjectUuid {get => DynamicObjectUuid; set => DynamicObjectUuid = value;}

        [JsonMember("CaseTitle")]
        [MetaTableRowColumn("CaseTitle","string", "案件名",2)]
        [LabelText("案件名")]
        public string CaseTitle ;

        string ICasesRow.CaseTitle {get => CaseTitle; set => CaseTitle = value;}

        [JsonMember("CaseVariables")]
        [MetaTableRowColumn("CaseVariables","string", "案件变量列表",3)]
        [LabelText("案件变量列表")]
        public string CaseVariables ;

        string ICasesRow.CaseVariables {get => CaseVariables; set => CaseVariables = value;}

        [JsonMember("CaseStates")]
        [MetaTableRowColumn("CaseStates","string", "案件状态列表",4)]
        [LabelText("案件状态列表")]
        public string CaseStates ;

        string ICasesRow.CaseStates {get => CaseStates; set => CaseStates = value;}

        [JsonMember("CaseClues")]
        [MetaTableRowColumn("CaseClues","string", "案件线索",5)]
        [LabelText("案件线索")]
        public string CaseClues ;

        string ICasesRow.CaseClues {get => CaseClues; set => CaseClues = value;}

        [JsonMember("CluesIntegrate")]
        [MetaTableRowColumn("CluesIntegrate","string", "线索整合列表",6)]
        [LabelText("线索整合列表")]
        public string CluesIntegrate ;

        string ICasesRow.CluesIntegrate {get => CluesIntegrate; set => CluesIntegrate = value;}

        [JsonMember("StateModelOnOff")]
        [MetaTableRowColumn("StateModelOnOff","string", "状态模型开关",7)]
        [LabelText("状态模型开关")]
        public string StateModelOnOff ;

        string ICasesRow.StateModelOnOff {get => StateModelOnOff; set => StateModelOnOff = value;}

        [JsonMember("CaseShowType")]
        [MetaTableRowColumn("CaseShowType","string", "案件显示类型",8)]
        [LabelText("案件显示类型")]
        public string CaseShowType ;

        string ICasesRow.CaseShowType {get => CaseShowType; set => CaseShowType = value;}

        [JsonMember("CaseVariableState")]
        [MetaTableRowColumn("CaseVariableState","string", "案件变量状态",9)]
        [LabelText("案件变量状态")]
        public string CaseVariableState ;

        string ICasesRow.CaseVariableState {get => CaseVariableState; set => CaseVariableState = value;}

    }
}

