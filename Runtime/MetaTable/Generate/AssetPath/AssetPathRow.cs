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
    public partial class AssetPathRow : MetaTableRow,IAssetPathRow
    {

        [JsonMember("AssetPackageDir")]
        [MetaTableRowColumn("AssetPackageDir","string", "资源包路径",3)]
        [LabelText("资源包路径")]
        public string AssetPackageDir ;

        string IAssetPathRow.AssetPackageDir {get => AssetPackageDir; set => AssetPackageDir = value;}

        [JsonMember("AssetType")]
        [MetaTableRowColumn("AssetType","string", "资源类型",4)]
        [LabelText("资源类型")]
        public string AssetType ;

        string IAssetPathRow.AssetType {get => AssetType; set => AssetType = value;}

        [JsonMember("AssetPath")]
        [MetaTableRowColumn("AssetPath","string", "资源路径",5)]
        [LabelText("资源路径")]
        public string AssetPath ;

        string IAssetPathRow.AssetPath {get => AssetPath; set => AssetPath = value;}

        [JsonMember("AssetGroup")]
        [MetaTableRowColumn("AssetGroup","string", "资源组",6)]
        [LabelText("资源组")]
        public string AssetGroup ;

        string IAssetPathRow.AssetGroup {get => AssetGroup; set => AssetGroup = value;}

        [JsonMember("Id")]
        [MetaTableRowColumn("Id","int", "Id",7)]
        [LabelText("Id")]
        public int Id ;

        int IAssetPathRow.Id {get => Id; set => Id = value;}

    }
}

