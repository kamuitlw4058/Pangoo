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
    public partial class ActorsLinesRow : MetaTableRow,IActorsLinesRow
    {

        [JsonMember("Duration")]
        [MetaTableRowColumn("Duration","float", "总持续时间",1)]
        [LabelText("总持续时间")]
        public float Duration ;

        float IActorsLinesRow.Duration {get => Duration; set => Duration = value;}

        [JsonMember("TimelinePath")]
        [MetaTableRowColumn("TimelinePath","string", "参考Timeline路径",2)]
        [LabelText("参考Timeline路径")]
        public string TimelinePath ;

        string IActorsLinesRow.TimelinePath {get => TimelinePath; set => TimelinePath = value;}

        [JsonMember("DialogueSubtitles")]
        [MetaTableRowColumn("DialogueSubtitles","string", "字幕列表",3)]
        [LabelText("字幕列表")]
        public string DialogueSubtitles ;

        string IActorsLinesRow.DialogueSubtitles {get => DialogueSubtitles; set => DialogueSubtitles = value;}

    }
}

