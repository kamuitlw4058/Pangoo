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
    public partial class StaticSceneRow : MetaTableRow,IStaticSceneRow
    {

        [JsonMember("NameCn")]
        [MetaTableRowColumn("NameCn","string", "中文名",3)]
        [LabelText("中文名")]
        public string NameCn ;

        string IStaticSceneRow.NameCn {get => NameCn; set => NameCn = value;}

        [JsonMember("AssetPathId")]
        [MetaTableRowColumn("AssetPathId","int", "资源路径",4)]
        [LabelText("资源路径")]
        public int AssetPathId ;

        int IStaticSceneRow.AssetPathId {get => AssetPathId; set => AssetPathId = value;}

        [JsonMember("EntityGroupId")]
        [MetaTableRowColumn("EntityGroupId","int", "实体组",5)]
        [LabelText("实体组")]
        public int EntityGroupId ;

        int IStaticSceneRow.EntityGroupId {get => EntityGroupId; set => EntityGroupId = value;}

        [JsonMember("LoadSceneIds")]
        [MetaTableRowColumn("LoadSceneIds","string", "加载场景",6)]
        [LabelText("加载场景")]
        public string LoadSceneIds ;

        string IStaticSceneRow.LoadSceneIds {get => LoadSceneIds; set => LoadSceneIds = value;}

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "资源路径Uuid",8)]
        [LabelText("资源路径Uuid")]
        public string AssetPathUuid ;

        string IStaticSceneRow.AssetPathUuid {get => AssetPathUuid; set => AssetPathUuid = value;}

        [JsonMember("LoadSceneUuids")]
        [MetaTableRowColumn("LoadSceneUuids","string", "资源路径加载Uuid",9)]
        [LabelText("资源路径加载Uuid")]
        public string LoadSceneUuids ;

        string IStaticSceneRow.LoadSceneUuids {get => LoadSceneUuids; set => LoadSceneUuids = value;}

    }
}

