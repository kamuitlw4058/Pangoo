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
    public partial class GameSectionRow : MetaTableRow
    {

        [JsonMember("DynamicSceneIds")]
        [MetaTableRowColumn("DynamicSceneIds","string", "动态加载场景",3)]
        [LabelText("动态加载场景")]
        public string DynamicSceneIds ;

        [JsonMember("KeepSceneIds")]
        [MetaTableRowColumn("KeepSceneIds","string", "持续加载场景",4)]
        [LabelText("持续加载场景")]
        public string KeepSceneIds ;

        [JsonMember("SectionJumpByScene")]
        [MetaTableRowColumn("SectionJumpByScene","string", "章节跳转的场景",5)]
        [LabelText("章节跳转的场景")]
        public string SectionJumpByScene ;

        [JsonMember("InitSceneIds")]
        [MetaTableRowColumn("InitSceneIds","string", "进入章节默认加载的场景",6)]
        [LabelText("进入章节默认加载的场景")]
        public string InitSceneIds ;

        [JsonMember("DynamicObjectIds")]
        [MetaTableRowColumn("DynamicObjectIds","string", "动态物体",7)]
        [LabelText("动态物体")]
        public string DynamicObjectIds ;

        [JsonMember("InitedInstructionIds")]
        [MetaTableRowColumn("InitedInstructionIds","string", "初始化后执行的指令",8)]
        [LabelText("初始化后执行的指令")]
        public string InitedInstructionIds ;

        [JsonMember("EditorInitedInstructionIds")]
        [MetaTableRowColumn("EditorInitedInstructionIds","string", "编辑器初始化后执行的指令",9)]
        [LabelText("编辑器初始化后执行的指令")]
        public string EditorInitedInstructionIds ;

        [JsonMember("DynamicSceneUuids")]
        [MetaTableRowColumn("DynamicSceneUuids","string", "动态加载场景Uuids",10)]
        [LabelText("动态加载场景Uuids")]
        public string DynamicSceneUuids ;

        [JsonMember("KeepSceneUuids")]
        [MetaTableRowColumn("KeepSceneUuids","string", "持续加载场景Uuids",11)]
        [LabelText("持续加载场景Uuids")]
        public string KeepSceneUuids ;

        [JsonMember("InitSceneUuids")]
        [MetaTableRowColumn("InitSceneUuids","string", "进入章节默认加载的场景Uuids",12)]
        [LabelText("进入章节默认加载的场景Uuids")]
        public string InitSceneUuids ;

        [JsonMember("DynamicObjectUuids")]
        [MetaTableRowColumn("DynamicObjectUuids","string", "动态物体Uuids",13)]
        [LabelText("动态物体Uuids")]
        public string DynamicObjectUuids ;

        [JsonMember("InitedInstructionUuids")]
        [MetaTableRowColumn("InitedInstructionUuids","string", "初始化后执行的指令Uuids",14)]
        [LabelText("初始化后执行的指令Uuids")]
        public string InitedInstructionUuids ;

        [JsonMember("EditorInitedInstructionUuids")]
        [MetaTableRowColumn("EditorInitedInstructionUuids","string", "编辑器初始化后执行的指令Uuids",15)]
        [LabelText("编辑器初始化后执行的指令Uuids")]
        public string EditorInitedInstructionUuids ;

        [JsonMember("Id")]
        [MetaTableRowColumn("Id","int", "Id",16)]
        [LabelText("Id")]
        public int Id ;

    }
}

