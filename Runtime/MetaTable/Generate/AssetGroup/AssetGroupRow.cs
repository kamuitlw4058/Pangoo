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
    public partial class AssetGroupRow : MetaTableRow,IAssetGroupRow
    {

        [JsonMember("AssetGroup")]
        [MetaTableRowColumn("AssetGroup","string", "资源组",3)]
        [LabelText("资源组")]
        public string AssetGroup ;

        string IAssetGroupRow.AssetGroup {get => AssetGroup; set => AssetGroup = value;}

        [JsonMember("Desc")]
        [MetaTableRowColumn("Desc","string", "资源组描述",4)]
        [LabelText("资源组描述")]
        public string Desc ;

        string IAssetGroupRow.Desc {get => Desc; set => Desc = value;}

    }
}

