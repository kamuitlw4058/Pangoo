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
    public partial class DynamicObjectPreviewRow : MetaTableRow,IDynamicObjectPreviewRow
    {

        [JsonMember("Title")]
        [MetaTableRowColumn("Title","string", "标题",1)]
        [LabelText("标题")]
        public string Title ;

        string IDynamicObjectPreviewRow.Title {get => Title; set => Title = value;}

        [JsonMember("OnShowInstructions")]
        [MetaTableRowColumn("OnShowInstructions","string", "显示时指令",2)]
        [LabelText("显示时指令")]
        public string OnShowInstructions ;

        string IDynamicObjectPreviewRow.OnShowInstructions {get => OnShowInstructions; set => OnShowInstructions = value;}

        [JsonMember("OnGrabInstructions")]
        [MetaTableRowColumn("OnGrabInstructions","string", "抓取时指令",3)]
        [LabelText("抓取时指令")]
        public string OnGrabInstructions ;

        string IDynamicObjectPreviewRow.OnGrabInstructions {get => OnGrabInstructions; set => OnGrabInstructions = value;}

        [JsonMember("OnPreviewInstructions")]
        [MetaTableRowColumn("OnPreviewInstructions","string", "预览时指令",4)]
        [LabelText("预览时指令")]
        public string OnPreviewInstructions ;

        string IDynamicObjectPreviewRow.OnPreviewInstructions {get => OnPreviewInstructions; set => OnPreviewInstructions = value;}

        [JsonMember("OnCloseInstructions")]
        [MetaTableRowColumn("OnCloseInstructions","string", "关闭时指令",5)]
        [LabelText("关闭时指令")]
        public string OnCloseInstructions ;

        string IDynamicObjectPreviewRow.OnCloseInstructions {get => OnCloseInstructions; set => OnCloseInstructions = value;}

        [JsonMember("GrabSoundUuid")]
        [MetaTableRowColumn("GrabSoundUuid","string", "抓取音频",6)]
        [LabelText("抓取音频")]
        public string GrabSoundUuid ;

        string IDynamicObjectPreviewRow.GrabSoundUuid {get => GrabSoundUuid; set => GrabSoundUuid = value;}

        [JsonMember("ExitKeyCodes")]
        [MetaTableRowColumn("ExitKeyCodes","string", "退出按键列表",7)]
        [LabelText("退出按键列表")]
        public string ExitKeyCodes ;

        string IDynamicObjectPreviewRow.ExitKeyCodes {get => ExitKeyCodes; set => ExitKeyCodes = value;}

        [JsonMember("PreviewInteractKeyCodes")]
        [MetaTableRowColumn("PreviewInteractKeyCodes","string", "预览内交互按键列表",8)]
        [LabelText("预览内交互按键列表")]
        public string PreviewInteractKeyCodes ;

        string IDynamicObjectPreviewRow.PreviewInteractKeyCodes {get => PreviewInteractKeyCodes; set => PreviewInteractKeyCodes = value;}

        [JsonMember("Params")]
        [MetaTableRowColumn("Params","string", "參數",9)]
        [LabelText("參數")]
        public string Params ;

        string IDynamicObjectPreviewRow.Params {get => Params; set => Params = value;}

    }
}

