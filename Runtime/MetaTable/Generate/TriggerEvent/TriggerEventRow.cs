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
    public partial class TriggerEventRow : MetaTableRow,ITriggerEventRow
    {

        [JsonMember("Enabled")]
        [MetaTableRowColumn("Enabled","bool", "是否打开",3)]
        [LabelText("是否打开")]
        public bool Enabled ;

        bool ITriggerEventRow.Enabled {get => Enabled; set => Enabled = value;}

        [JsonMember("TriggerType")]
        [MetaTableRowColumn("TriggerType","string", "触发类型",4)]
        [LabelText("触发类型")]
        public string TriggerType ;

        string ITriggerEventRow.TriggerType {get => TriggerType; set => TriggerType = value;}

        [JsonMember("Targets")]
        [MetaTableRowColumn("Targets","string", "触发对象列表",5)]
        [LabelText("触发对象列表")]
        public string Targets ;

        string ITriggerEventRow.Targets {get => Targets; set => Targets = value;}

        [JsonMember("TargetListType")]
        [MetaTableRowColumn("TargetListType","int", "目标列表类型",6)]
        [LabelText("目标列表类型")]
        public int TargetListType ;

        int ITriggerEventRow.TargetListType {get => TargetListType; set => TargetListType = value;}

        [JsonMember("InstructionList")]
        [MetaTableRowColumn("InstructionList","string", "指令列表",7)]
        [LabelText("指令列表")]
        public string InstructionList ;

        string ITriggerEventRow.InstructionList {get => InstructionList; set => InstructionList = value;}

        [JsonMember("Params")]
        [MetaTableRowColumn("Params","string", "触发器参数",8)]
        [LabelText("触发器参数")]
        public string Params ;

        string ITriggerEventRow.Params {get => Params; set => Params = value;}

        [JsonMember("ConditionType")]
        [MetaTableRowColumn("ConditionType","string", "条件类型",9)]
        [LabelText("条件类型")]
        public string ConditionType ;

        string ITriggerEventRow.ConditionType {get => ConditionType; set => ConditionType = value;}

        [JsonMember("ConditionList")]
        [MetaTableRowColumn("ConditionList","string", "条件列表",10)]
        [LabelText("条件列表")]
        public string ConditionList ;

        string ITriggerEventRow.ConditionList {get => ConditionList; set => ConditionList = value;}

        [JsonMember("FailInstructionList")]
        [MetaTableRowColumn("FailInstructionList","string", "失败指令列表",11)]
        [LabelText("失败指令列表")]
        public string FailInstructionList ;

        string ITriggerEventRow.FailInstructionList {get => FailInstructionList; set => FailInstructionList = value;}

        [JsonMember("ConditionUuidList")]
        [MetaTableRowColumn("ConditionUuidList","string", "ConditionUuidList",13)]
        [LabelText("ConditionUuidList")]
        public string ConditionUuidList ;

        string ITriggerEventRow.ConditionUuidList {get => ConditionUuidList; set => ConditionUuidList = value;}

        [JsonMember("UseVariableCondition")]
        [MetaTableRowColumn("UseVariableCondition","bool", "是否使用变量条件",14)]
        [LabelText("是否使用变量条件")]
        public bool UseVariableCondition ;

        bool ITriggerEventRow.UseVariableCondition {get => UseVariableCondition; set => UseVariableCondition = value;}

        [JsonMember("BoolVariableUuds")]
        [MetaTableRowColumn("BoolVariableUuds","string", "布尔变量列表",15)]
        [LabelText("布尔变量列表")]
        public string BoolVariableUuds ;

        string ITriggerEventRow.BoolVariableUuds {get => BoolVariableUuds; set => BoolVariableUuds = value;}

        [JsonMember("IntVariableUuid")]
        [MetaTableRowColumn("IntVariableUuid","string", "状态变量",16)]
        [LabelText("状态变量")]
        public string IntVariableUuid ;

        string ITriggerEventRow.IntVariableUuid {get => IntVariableUuid; set => IntVariableUuid = value;}

    }
}

