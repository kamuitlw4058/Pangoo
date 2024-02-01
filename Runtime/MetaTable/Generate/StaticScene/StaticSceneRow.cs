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

        [JsonMember("EntityGroupId")]
        [MetaTableRowColumn("EntityGroupId","int", "实体组",4)]
        [LabelText("实体组")]
        public int EntityGroupId ;

        int IStaticSceneRow.EntityGroupId {get => EntityGroupId; set => EntityGroupId = value;}

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "资源路径Uuid",5)]
        [LabelText("资源路径Uuid")]
        public string AssetPathUuid ;

        string IStaticSceneRow.AssetPathUuid {get => AssetPathUuid; set => AssetPathUuid = value;}

        [JsonMember("LoadSceneUuids")]
        [MetaTableRowColumn("LoadSceneUuids","string", "资源路径加载Uuid",6)]
        [LabelText("资源路径加载Uuid")]
        public string LoadSceneUuids ;

        string IStaticSceneRow.LoadSceneUuids {get => LoadSceneUuids; set => LoadSceneUuids = value;}

        [JsonMember("UseSceneFootstep")]
        [MetaTableRowColumn("UseSceneFootstep","bool", "使用场景脚步声",7)]
        [LabelText("使用场景脚步声")]
        public bool UseSceneFootstep ;

        bool IStaticSceneRow.UseSceneFootstep {get => UseSceneFootstep; set => UseSceneFootstep = value;}

        [JsonMember("Footsetp")]
        [MetaTableRowColumn("Footsetp","string", "脚步声配置",8)]
        [LabelText("脚步声配置")]
        public string Footsetp ;

        string IStaticSceneRow.Footsetp {get => Footsetp; set => Footsetp = value;}

    }
}

