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
    public partial class SoundRow : MetaTableRow,ISoundRow
    {

        [JsonMember("PackageDir")]
        [MetaTableRowColumn("PackageDir","string", "包目录",3)]
        [LabelText("包目录")]
        public string PackageDir ;

        string ISoundRow.PackageDir {get => PackageDir; set => PackageDir = value;}

        [JsonMember("SoundType")]
        [MetaTableRowColumn("SoundType","string", "音频类型",4)]
        [LabelText("音频类型")]
        public string SoundType ;

        string ISoundRow.SoundType {get => SoundType; set => SoundType = value;}

        [JsonMember("AssetPath")]
        [MetaTableRowColumn("AssetPath","string", "资源路径",5)]
        [LabelText("资源路径")]
        public string AssetPath ;

        string ISoundRow.AssetPath {get => AssetPath; set => AssetPath = value;}

    }
}

