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
    public partial class AssetGroupRow : MetaTableRow
    {

        [JsonMember("AssetGroup")]
        [MetaTableRowColumn("AssetGroup","string", "",3)]
        [LabelText("")]
        public string AssetGroup ;

        [JsonMember("Desc")]
        [MetaTableRowColumn("Desc","string", "资源描述",4)]
        [LabelText("资源描述")]
        public string Desc ;

    }
}

