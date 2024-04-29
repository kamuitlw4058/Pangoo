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
    public partial class ClueRow : MetaTableRow,IClueRow
    {

        [JsonMember("DynamicObjectUuid")]
        [MetaTableRowColumn("DynamicObjectUuid","string", "动态物体Uuid",1)]
        [LabelText("动态物体Uuid")]
        public string DynamicObjectUuid ;

        string IClueRow.DynamicObjectUuid {get => DynamicObjectUuid; set => DynamicObjectUuid = value;}

        [JsonMember("ClueTitle")]
        [MetaTableRowColumn("ClueTitle","string", "标题",2)]
        [LabelText("标题")]
        public string ClueTitle ;

        string IClueRow.ClueTitle {get => ClueTitle; set => ClueTitle = value;}

        [JsonMember("Desc")]
        [MetaTableRowColumn("Desc","string", "描述",3)]
        [LabelText("描述")]
        public string Desc ;

        string IClueRow.Desc {get => Desc; set => Desc = value;}

        [JsonMember("ClueKey")]
        [MetaTableRowColumn("ClueKey","string", "线索索引Key",4)]
        [LabelText("线索索引Key")]
        public string ClueKey ;

        string IClueRow.ClueKey {get => ClueKey; set => ClueKey = value;}

        [JsonMember("ClueBackTitle")]
        [MetaTableRowColumn("ClueBackTitle","string", "背面标题",5)]
        [LabelText("背面标题")]
        public string ClueBackTitle ;

        string IClueRow.ClueBackTitle {get => ClueBackTitle; set => ClueBackTitle = value;}

        [JsonMember("ClueBackDesc")]
        [MetaTableRowColumn("ClueBackDesc","string", "背面描述",6)]
        [LabelText("背面描述")]
        public string ClueBackDesc ;

        string IClueRow.ClueBackDesc {get => ClueBackDesc; set => ClueBackDesc = value;}

    }
}

