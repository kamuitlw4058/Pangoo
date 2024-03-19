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
    public partial class SimpleUIRow : MetaTableRow,ISimpleUIRow
    {

        [JsonMember("AssetPathId")]
        [MetaTableRowColumn("AssetPathId","int", "资源路径",3)]
        [LabelText("资源路径")]
        public int AssetPathId ;

        int ISimpleUIRow.AssetPathId {get => AssetPathId; set => AssetPathId = value;}

        [JsonMember("SortingLayer")]
        [MetaTableRowColumn("SortingLayer","string", "排序层",4)]
        [LabelText("排序层")]
        public string SortingLayer ;

        string ISimpleUIRow.SortingLayer {get => SortingLayer; set => SortingLayer = value;}

        [JsonMember("SortingOrder")]
        [MetaTableRowColumn("SortingOrder","int", "排序索引",5)]
        [LabelText("排序索引")]
        public int SortingOrder ;

        int ISimpleUIRow.SortingOrder {get => SortingOrder; set => SortingOrder = value;}

        [JsonMember("UIParamsType")]
        [MetaTableRowColumn("UIParamsType","string", "UI参数类",6)]
        [LabelText("UI参数类")]
        public string UIParamsType ;

        string ISimpleUIRow.UIParamsType {get => UIParamsType; set => UIParamsType = value;}

        [JsonMember("Params")]
        [MetaTableRowColumn("Params","string", "UI参数",7)]
        [LabelText("UI参数")]
        public string Params ;

        string ISimpleUIRow.Params {get => Params; set => Params = value;}

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "资源路径Uuid",8)]
        [LabelText("资源路径Uuid")]
        public string AssetPathUuid ;

        string ISimpleUIRow.AssetPathUuid {get => AssetPathUuid; set => AssetPathUuid = value;}

    }
}

