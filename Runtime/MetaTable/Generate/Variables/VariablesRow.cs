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
    public partial class VariablesRow : MetaTableRow,IVariablesRow
    {

        [JsonMember("VariableType")]
        [MetaTableRowColumn("VariableType","string", "变量类型",3)]
        [LabelText("变量类型")]
        public string VariableType ;

        string IVariablesRow.VariableType {get => VariableType; set => VariableType = value;}

        [JsonMember("Key")]
        [MetaTableRowColumn("Key","string", "关键字",4)]
        [LabelText("关键字")]
        public string Key ;

        string IVariablesRow.Key {get => Key; set => Key = value;}

        [JsonMember("ValueType")]
        [MetaTableRowColumn("ValueType","string", "值类型",5)]
        [LabelText("值类型")]
        public string ValueType ;

        string IVariablesRow.ValueType {get => ValueType; set => ValueType = value;}

        [JsonMember("DefaultValue")]
        [MetaTableRowColumn("DefaultValue","string", "默认值",6)]
        [LabelText("默认值")]
        public string DefaultValue ;

        string IVariablesRow.DefaultValue {get => DefaultValue; set => DefaultValue = value;}

        [JsonMember("NotSave")]
        [MetaTableRowColumn("NotSave","bool", "是否不要保存",7)]
        [LabelText("是否不要保存")]
        public bool NotSave ;

        bool IVariablesRow.NotSave {get => NotSave; set => NotSave = value;}

    }
}

