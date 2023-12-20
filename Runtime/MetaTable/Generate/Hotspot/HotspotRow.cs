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
    public partial class HotspotRow : MetaTableRow,IHotspotRow
    {

        [JsonMember("HotspotType")]
        [MetaTableRowColumn("HotspotType","string", "热点区域类型",3)]
        [LabelText("热点区域类型")]
        public string HotspotType ;

        string IHotspotRow.HotspotType {get => HotspotType; set => HotspotType = value;}

        [JsonMember("Params")]
        [MetaTableRowColumn("Params","string", "参数",4)]
        [LabelText("参数")]
        public string Params ;

        string IHotspotRow.Params {get => Params; set => Params = value;}

    }
}

