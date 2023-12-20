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
    public partial class InstructionRow : MetaTableRow,IInstructionRow
    {

        [JsonMember("InstructionType")]
        [MetaTableRowColumn("InstructionType","string", "指令类型",3)]
        [LabelText("指令类型")]
        public string InstructionType ;

        string IInstructionRow.InstructionType {get => InstructionType; set => InstructionType = value;}

        [JsonMember("Params")]
        [MetaTableRowColumn("Params","string", "指令参数",4)]
        [LabelText("指令参数")]
        public string Params ;

        string IInstructionRow.Params {get => Params; set => Params = value;}

    }
}

