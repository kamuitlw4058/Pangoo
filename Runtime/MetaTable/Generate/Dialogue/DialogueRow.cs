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

        [JsonMember("SoundUuid")]
        [MetaTableRowColumn("SoundUuid","string", "音频Uuid",1)]
        [LabelText("音频Uuid")]
        public string SoundUuid ;

        string IDialogueRow.SoundUuid {get => SoundUuid; set => SoundUuid = value;}

        [JsonMember("Subtitles")]
        [MetaTableRowColumn("Subtitles","string", "字幕列表",2)]
        [LabelText("字幕列表")]
        public string Subtitles ;

        string IDialogueRow.Subtitles {get => Subtitles; set => Subtitles = value;}

    }
}

