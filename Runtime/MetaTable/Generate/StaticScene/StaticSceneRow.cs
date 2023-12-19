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
    public partial class StaticSceneRow : MetaTableRow
    {

        [JsonMember("NameCn")]
        [MetaTableRowColumn("NameCn","string", "中文名",3)]
        [LabelText("中文名")]
        public string NameCn ;

        [JsonMember("AssetPathId")]
        [MetaTableRowColumn("AssetPathId","int", "资源路径",4)]
        [LabelText("资源路径")]
        public int AssetPathId ;

        [JsonMember("EntityGroupId")]
        [MetaTableRowColumn("EntityGroupId","int", "实体组",5)]
        [LabelText("实体组")]
        public int EntityGroupId ;

        [JsonMember("LoadSceneIds")]
        [MetaTableRowColumn("LoadSceneIds","string", "加载场景",6)]
        [LabelText("加载场景")]
        public string LoadSceneIds ;

        [JsonMember("Id")]
        [MetaTableRowColumn("Id","int", "Id",7)]
        [LabelText("Id")]
        public int Id ;

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "资源路径Uuid",8)]
        [LabelText("资源路径Uuid")]
        public string AssetPathUuid ;

        [JsonMember("LoadSceneUuids")]
        [MetaTableRowColumn("LoadSceneUuids","string", "资源路径加载Uuid",9)]
        [LabelText("资源路径加载Uuid")]
        public string LoadSceneUuids ;

    }
}

