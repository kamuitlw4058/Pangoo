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
    public partial class AssetPathRow : MetaTableRow
    {

        [JsonMember("AssetPackageDir")]
        [MetaTableRowColumn("AssetPackageDir","string", "资源包Id",3)]
        [LabelText("资源包Id")]
        public string AssetPackageDir ;

        [JsonMember("AssetType")]
        [MetaTableRowColumn("AssetType","string", "资源类型",4)]
        [LabelText("资源类型")]
        public string AssetType ;

        [JsonMember("AssetPath")]
        [MetaTableRowColumn("AssetPath","string", "资源路径",5)]
        [LabelText("资源路径")]
        public string AssetPath ;

        [JsonMember("AssetGroup")]
        [MetaTableRowColumn("AssetGroup","string", "",6)]
        [LabelText("")]
        public string AssetGroup ;

        [JsonMember("Desc")]
        [MetaTableRowColumn("Desc","string", "资源描述",7)]
        [LabelText("资源描述")]
        public string Desc ;

    }
}

