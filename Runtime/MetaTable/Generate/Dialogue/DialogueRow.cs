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
    public partial class DialogueRow : MetaTableRow,IDialogueRow
    {

        [JsonMember("DialogueType")]
        [MetaTableRowColumn("DialogueType","string", "对话类型",1)]
        [LabelText("对话类型")]
        public string DialogueType ;

        string IDialogueRow.DialogueType {get => DialogueType; set => DialogueType = value;}

        [JsonMember("ActorsLinesUuid")]
        [MetaTableRowColumn("ActorsLinesUuid","string", "台词Uuid",2)]
        [LabelText("台词Uuid")]
        public string ActorsLinesUuid ;

        string IDialogueRow.ActorsLinesUuid {get => ActorsLinesUuid; set => ActorsLinesUuid = value;}

        [JsonMember("NextDialogueUuid")]
        [MetaTableRowColumn("NextDialogueUuid","string", "下一个对话的Uuid",3)]
        [LabelText("下一个对话的Uuid")]
        public string NextDialogueUuid ;

        string IDialogueRow.NextDialogueUuid {get => NextDialogueUuid; set => NextDialogueUuid = value;}

        [JsonMember("Options")]
        [MetaTableRowColumn("Options","string", "选择列表",4)]
        [LabelText("选择列表")]
        public string Options ;

        string IDialogueRow.Options {get => Options; set => Options = value;}

    }
}

