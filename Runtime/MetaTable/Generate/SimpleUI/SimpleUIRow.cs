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
    public partial class SimpleUIRow : MetaTableRow
    {

        [JsonMember("AssetPathId")]
        [MetaTableRowColumn("AssetPathId","int", "资源路径",3)]
        [LabelText("资源路径")]
        public int AssetPathId ;

        [JsonMember("SortingLayer")]
        [MetaTableRowColumn("SortingLayer","string", "排序层",4)]
        [LabelText("排序层")]
        public string SortingLayer ;

        [JsonMember("SortingOrder")]
        [MetaTableRowColumn("SortingOrder","int", "排序索引",5)]
        [LabelText("排序索引")]
        public int SortingOrder ;

        [JsonMember("UIParamsType")]
        [MetaTableRowColumn("UIParamsType","string", "UI参数类",6)]
        [LabelText("UI参数类")]
        public string UIParamsType ;

        [JsonMember("Params")]
        [MetaTableRowColumn("Params","string", "UI参数",7)]
        [LabelText("UI参数")]
        public string Params ;

        [JsonMember("Id")]
        [MetaTableRowColumn("Id","int", "Id",8)]
        [LabelText("Id")]
        public int Id ;

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "资源路径Uuid",9)]
        [LabelText("资源路径Uuid")]
        public string AssetPathUuid ;

    }
}

