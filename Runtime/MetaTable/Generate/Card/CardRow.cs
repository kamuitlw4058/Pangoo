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
    public partial class CardRow : MetaTableRow,ICardRow
    {

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "资源Uuid",1)]
        [LabelText("资源Uuid")]
        public string AssetPathUuid ;

        string ICardRow.AssetPathUuid {get => AssetPathUuid; set => AssetPathUuid = value;}

    }
}

