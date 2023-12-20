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
    public partial class ConditionRow : MetaTableRow,IConditionRow
    {

        [JsonMember("ConditionType")]
        [MetaTableRowColumn("ConditionType","string", "条件类型",3)]
        [LabelText("条件类型")]
        public string ConditionType ;

        string IConditionRow.ConditionType {get => ConditionType; set => ConditionType = value;}

        [JsonMember("Params")]
        [MetaTableRowColumn("Params","string", "条件参数",4)]
        [LabelText("条件参数")]
        public string Params ;

        string IConditionRow.Params {get => Params; set => Params = value;}

    }
}

