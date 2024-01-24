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

        [JsonMember("SceneFootstepVolume")]
        [MetaTableRowColumn("SceneFootstepVolume","float", "场景脚步声音量",8)]
        [LabelText("场景脚步声音量")]
        public float SceneFootstepVolume ;

        float IStaticSceneRow.SceneFootstepVolume {get => SceneFootstepVolume; set => SceneFootstepVolume = value;}

        [JsonMember("SceneFootstepUuids")]
        [MetaTableRowColumn("SceneFootstepUuids","string", "场景脚步声列表",9)]
        [LabelText("场景脚步声列表")]
        public string SceneFootstepUuids ;

        string IStaticSceneRow.SceneFootstepUuids {get => SceneFootstepUuids; set => SceneFootstepUuids = value;}

        [JsonMember("SceneFootstepIntervalMin")]
        [MetaTableRowColumn("SceneFootstepIntervalMin","float", "场景脚步声间隔最小值",10)]
        [LabelText("场景脚步声间隔最小值")]
        public float SceneFootstepIntervalMin ;

        float IStaticSceneRow.SceneFootstepIntervalMin {get => SceneFootstepIntervalMin; set => SceneFootstepIntervalMin = value;}

        [JsonMember("SceneFootstepIntervalMax")]
        [MetaTableRowColumn("SceneFootstepIntervalMax","float", "场景脚步声间隔最大值",11)]
        [LabelText("场景脚步声间隔最大值")]
        public float SceneFootstepIntervalMax ;

        float IStaticSceneRow.SceneFootstepIntervalMax {get => SceneFootstepIntervalMax; set => SceneFootstepIntervalMax = value;}

        [JsonMember("SceneFootstepMinInterval")]
        [MetaTableRowColumn("SceneFootstepMinInterval","float", "场景脚步声最小间隔",12)]
        [LabelText("场景脚步声最小间隔")]
        public float SceneFootstepMinInterval ;

        float IStaticSceneRow.SceneFootstepMinInterval {get => SceneFootstepMinInterval; set => SceneFootstepMinInterval = value;}

    }
}

