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
    public partial class CluesRow : MetaTableRow,ICluesRow
    {

        [JsonMember("DynamicObjectUuid")]
        [MetaTableRowColumn("DynamicObjectUuid","string", "动态物体Uuid",1)]
        [LabelText("动态物体Uuid")]
        public string DynamicObjectUuid ;

        string ICluesRow.DynamicObjectUuid {get => DynamicObjectUuid; set => DynamicObjectUuid = value;}

        [JsonMember("ClueTitle")]
        [MetaTableRowColumn("ClueTitle","string", "标题",2)]
        [LabelText("标题")]
        public string ClueTitle ;

        string ICluesRow.ClueTitle {get => ClueTitle; set => ClueTitle = value;}

        [JsonMember("Desc")]
        [MetaTableRowColumn("Desc","string", "描述",3)]
        [LabelText("描述")]
        public string Desc ;

        string ICluesRow.Desc {get => Desc; set => Desc = value;}

    }
}

