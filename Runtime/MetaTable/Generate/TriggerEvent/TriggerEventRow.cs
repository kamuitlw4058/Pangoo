// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections;
using System.IO;
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
    public partial class TriggerEventRow : MetaTableRow
    {

        [JsonMember("Enabled")]
        [MetaTableRowColumn("Enabled","bool", "是否打开",3)]
        [LabelText("是否打开")]
        public bool Enabled ;

        [JsonMember("TriggerType")]
        [MetaTableRowColumn("TriggerType","string", "触发类型",4)]
        [LabelText("触发类型")]
        public string TriggerType ;

        [JsonMember("Targets")]
        [MetaTableRowColumn("Targets","string", "触发对象列表",5)]
        [LabelText("触发对象列表")]
        public string Targets ;

        [JsonMember("TargetListType")]
        [MetaTableRowColumn("TargetListType","int", "目标列表类型",6)]
        [LabelText("目标列表类型")]
        public int TargetListType ;

        [JsonMember("InstructionList")]
        [MetaTableRowColumn("InstructionList","string", "指令列表",7)]
        [LabelText("指令列表")]
        public string InstructionList ;

        [JsonMember("Params")]
        [MetaTableRowColumn("Params","string", "触发器参数",8)]
        [LabelText("触发器参数")]
        public string Params ;

        [JsonMember("ConditionType")]
        [MetaTableRowColumn("ConditionType","string", "条件类型",9)]
        [LabelText("条件类型")]
        public string ConditionType ;

        [JsonMember("ConditionList")]
        [MetaTableRowColumn("ConditionList","string", "条件列表",10)]
        [LabelText("条件列表")]
        public string ConditionList ;

        [JsonMember("FailInstructionList")]
        [MetaTableRowColumn("FailInstructionList","string", "失败指令列表",11)]
        [LabelText("失败指令列表")]
        public string FailInstructionList ;

        [JsonMember("Id")]
        [MetaTableRowColumn("Id","int", "Id",12)]
        [LabelText("Id")]
        public int Id ;

    }
}

