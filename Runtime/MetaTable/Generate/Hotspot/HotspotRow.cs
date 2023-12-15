// 本文件使用工具自动生成，请勿进行手动修改！

using System;
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
    public partial class HotspotRow : MetaTableRow
    {

        [JsonMember("HotspotType")]
        [MetaTableRowColumn("HotspotType","string", "热点区域类型",3)]
        [LabelText("热点区域类型")]
        public string HotspotType ;

        [JsonMember("Params")]
        [MetaTableRowColumn("Params","string", "参数",4)]
        [LabelText("参数")]
        public string Params ;

        [JsonMember("Id")]
        [MetaTableRowColumn("Id","int", "Id",5)]
        [LabelText("Id")]
        public int Id ;

    }
}

