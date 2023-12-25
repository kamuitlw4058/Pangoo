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

        [JsonMember("DynamicSceneIds")]
        [MetaTableRowColumn("DynamicSceneIds","string", "动态加载场景",3)]
        [LabelText("动态加载场景")]
        public string DynamicSceneIds ;

        string IGameSectionRow.DynamicSceneIds {get => DynamicSceneIds; set => DynamicSceneIds = value;}

        [JsonMember("KeepSceneIds")]
        [MetaTableRowColumn("KeepSceneIds","string", "持续加载场景",4)]
        [LabelText("持续加载场景")]
        public string KeepSceneIds ;

        string IGameSectionRow.KeepSceneIds {get => KeepSceneIds; set => KeepSceneIds = value;}

        [JsonMember("SectionJumpByScene")]
        [MetaTableRowColumn("SectionJumpByScene","string", "章节跳转的场景",5)]
        [LabelText("章节跳转的场景")]
        public string SectionJumpByScene ;

        string IGameSectionRow.SectionJumpByScene {get => SectionJumpByScene; set => SectionJumpByScene = value;}

        [JsonMember("InitSceneIds")]
        [MetaTableRowColumn("InitSceneIds","string", "进入章节默认加载的场景",6)]
        [LabelText("进入章节默认加载的场景")]
        public string InitSceneIds ;

        string IGameSectionRow.InitSceneIds {get => InitSceneIds; set => InitSceneIds = value;}

        [JsonMember("DynamicObjectIds")]
        [MetaTableRowColumn("DynamicObjectIds","string", "动态物体",7)]
        [LabelText("动态物体")]
        public string DynamicObjectIds ;

        string IGameSectionRow.DynamicObjectIds {get => DynamicObjectIds; set => DynamicObjectIds = value;}

        [JsonMember("InitedInstructionIds")]
        [MetaTableRowColumn("InitedInstructionIds","string", "初始化后执行的指令",8)]
        [LabelText("初始化后执行的指令")]
        public string InitedInstructionIds ;

        string IGameSectionRow.InitedInstructionIds {get => InitedInstructionIds; set => InitedInstructionIds = value;}

        [JsonMember("EditorInitedInstructionIds")]
        [MetaTableRowColumn("EditorInitedInstructionIds","string", "编辑器初始化后执行的指令",9)]
        [LabelText("编辑器初始化后执行的指令")]
        public string EditorInitedInstructionIds ;

        string IGameSectionRow.EditorInitedInstructionIds {get => EditorInitedInstructionIds; set => EditorInitedInstructionIds = value;}

        [JsonMember("DynamicSceneUuids")]
        [MetaTableRowColumn("DynamicSceneUuids","string", "动态加载场景Uuids",10)]
        [LabelText("动态加载场景Uuids")]
        public string DynamicSceneUuids ;

        string IGameSectionRow.DynamicSceneUuids {get => DynamicSceneUuids; set => DynamicSceneUuids = value;}

        [JsonMember("KeepSceneUuids")]
        [MetaTableRowColumn("KeepSceneUuids","string", "持续加载场景Uuids",11)]
        [LabelText("持续加载场景Uuids")]
        public string KeepSceneUuids ;

        string IGameSectionRow.KeepSceneUuids {get => KeepSceneUuids; set => KeepSceneUuids = value;}

        [JsonMember("InitSceneUuids")]
        [MetaTableRowColumn("InitSceneUuids","string", "进入章节默认加载的场景Uuids",12)]
        [LabelText("进入章节默认加载的场景Uuids")]
        public string InitSceneUuids ;

        string IGameSectionRow.InitSceneUuids {get => InitSceneUuids; set => InitSceneUuids = value;}

        [JsonMember("DynamicObjectUuids")]
        [MetaTableRowColumn("DynamicObjectUuids","string", "动态物体Uuids",13)]
        [LabelText("动态物体Uuids")]
        public string DynamicObjectUuids ;

        string IGameSectionRow.DynamicObjectUuids {get => DynamicObjectUuids; set => DynamicObjectUuids = value;}

        [JsonMember("InitedInstructionUuids")]
        [MetaTableRowColumn("InitedInstructionUuids","string", "初始化后执行的指令Uuids",14)]
        [LabelText("初始化后执行的指令Uuids")]
        public string InitedInstructionUuids ;

        string IGameSectionRow.InitedInstructionUuids {get => InitedInstructionUuids; set => InitedInstructionUuids = value;}

        [JsonMember("EditorInitedInstructionUuids")]
        [MetaTableRowColumn("EditorInitedInstructionUuids","string", "编辑器初始化后执行的指令Uuids",15)]
        [LabelText("编辑器初始化后执行的指令Uuids")]
        public string EditorInitedInstructionUuids ;

        string IGameSectionRow.EditorInitedInstructionUuids {get => EditorInitedInstructionUuids; set => EditorInitedInstructionUuids = value;}

    }
}

