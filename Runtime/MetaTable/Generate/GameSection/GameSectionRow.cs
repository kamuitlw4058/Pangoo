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
    public partial class GameSectionRow : MetaTableRow,IGameSectionRow
    {

        [JsonMember("SectionJumpByScene")]
        [MetaTableRowColumn("SectionJumpByScene","string", "章节跳转的场景",3)]
        [LabelText("章节跳转的场景")]
        public string SectionJumpByScene ;

        string IGameSectionRow.SectionJumpByScene {get => SectionJumpByScene; set => SectionJumpByScene = value;}

        [JsonMember("DynamicObjectUuids")]
        [MetaTableRowColumn("DynamicObjectUuids","string", "动态物体Uuids",4)]
        [LabelText("动态物体Uuids")]
        public string DynamicObjectUuids ;

        string IGameSectionRow.DynamicObjectUuids {get => DynamicObjectUuids; set => DynamicObjectUuids = value;}

        [JsonMember("InitedInstructionUuids")]
        [MetaTableRowColumn("InitedInstructionUuids","string", "初始化后执行的指令Uuids",5)]
        [LabelText("初始化后执行的指令Uuids")]
        public string InitedInstructionUuids ;

        string IGameSectionRow.InitedInstructionUuids {get => InitedInstructionUuids; set => InitedInstructionUuids = value;}

        [JsonMember("EditorInitedInstructionUuids")]
        [MetaTableRowColumn("EditorInitedInstructionUuids","string", "编辑器初始化后执行的指令Uuids",6)]
        [LabelText("编辑器初始化后执行的指令Uuids")]
        public string EditorInitedInstructionUuids ;

        string IGameSectionRow.EditorInitedInstructionUuids {get => EditorInitedInstructionUuids; set => EditorInitedInstructionUuids = value;}

        [JsonMember("SceneUuids")]
        [MetaTableRowColumn("SceneUuids","string", "场景uuids",7)]
        [LabelText("场景uuids")]
        public string SceneUuids ;

        string IGameSectionRow.SceneUuids {get => SceneUuids; set => SceneUuids = value;}

    }
}

