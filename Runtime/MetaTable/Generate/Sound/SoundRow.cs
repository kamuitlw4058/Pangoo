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
    public partial class SoundRow : MetaTableRow
    {

        [JsonMember("PackageDir")]
        [MetaTableRowColumn("PackageDir","string", "包目录",3)]
        [LabelText("包目录")]
        public string PackageDir ;

        [JsonMember("SoundType")]
        [MetaTableRowColumn("SoundType","string", "音频类型",4)]
        [LabelText("音频类型")]
        public string SoundType ;

        [JsonMember("AssetPath")]
        [MetaTableRowColumn("AssetPath","string", "资源路径",5)]
        [LabelText("资源路径")]
        public string AssetPath ;

        [JsonMember("Id")]
        [MetaTableRowColumn("Id","int", "Id",6)]
        [LabelText("Id")]
        public int Id ;

    }
}

