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
    public partial class DynamicObjectRow : MetaTableRow
    {

        [JsonMember("AssetPathId")]
        [MetaTableRowColumn("AssetPathId","int", "资源路径",3)]
        [LabelText("资源路径")]
        public int AssetPathId ;

        [JsonMember("NameCn")]
        [MetaTableRowColumn("NameCn","string", "中文名",4)]
        [LabelText("中文名")]
        public string NameCn ;

        [JsonMember("Space")]
        [MetaTableRowColumn("Space","string", "位置空间",5)]
        [LabelText("位置空间")]
        public string Space ;

        [JsonMember("Position")]
        [MetaTableRowColumn("Position","Vector3", "位置",6)]
        [LabelText("位置")]
        public Vector3 Position ;

        [JsonMember("Rotation")]
        [MetaTableRowColumn("Rotation","Vector3", "旋转",7)]
        [LabelText("旋转")]
        public Vector3 Rotation ;

        [JsonMember("Scale")]
        [MetaTableRowColumn("Scale","Vector3", "缩放",8)]
        [LabelText("缩放")]
        public Vector3 Scale ;

        [JsonMember("TriggerEventIds")]
        [MetaTableRowColumn("TriggerEventIds","string", "触发事件Ids",9)]
        [LabelText("触发事件Ids")]
        public string TriggerEventIds ;

        [JsonMember("UseHotspot")]
        [MetaTableRowColumn("UseHotspot","bool", "使用热点区域",10)]
        [LabelText("使用热点区域")]
        public bool UseHotspot ;

        [JsonMember("DefaultHideHotspot")]
        [MetaTableRowColumn("DefaultHideHotspot","bool", "默认关闭热点",11)]
        [LabelText("默认关闭热点")]
        public bool DefaultHideHotspot ;

        [JsonMember("HotspotRadius")]
        [MetaTableRowColumn("HotspotRadius","float", "热点区域的范围",12)]
        [LabelText("热点区域的范围")]
        public float HotspotRadius ;

        [JsonMember("HotspotIds")]
        [MetaTableRowColumn("HotspotIds","string", "热点区域Ids",13)]
        [LabelText("热点区域Ids")]
        public string HotspotIds ;

        [JsonMember("HotspotOffset")]
        [MetaTableRowColumn("HotspotOffset","Vector3", "热点区域偏移",14)]
        [LabelText("热点区域偏移")]
        public Vector3 HotspotOffset ;

        [JsonMember("DirectInstructions")]
        [MetaTableRowColumn("DirectInstructions","string", "直接指令",15)]
        [LabelText("直接指令")]
        public string DirectInstructions ;

        [JsonMember("SubDynamicObject")]
        [MetaTableRowColumn("SubDynamicObject","string", "子物体",16)]
        [LabelText("子物体")]
        public string SubDynamicObject ;

        [JsonMember("InteractRadius")]
        [MetaTableRowColumn("InteractRadius","float", "可交互半径",17)]
        [LabelText("可交互半径")]
        public float InteractRadius ;

        [JsonMember("InteractOffset")]
        [MetaTableRowColumn("InteractOffset","Vector3", "交互点偏移",18)]
        [LabelText("交互点偏移")]
        public Vector3 InteractOffset ;

        [JsonMember("InteractRadian")]
        [MetaTableRowColumn("InteractRadian","float", "可交互角度",19)]
        [LabelText("可交互角度")]
        public float InteractRadian ;

        [JsonMember("InteractTarget")]
        [MetaTableRowColumn("InteractTarget","string", "交互对象",20)]
        [LabelText("交互对象")]
        public string InteractTarget ;

        [JsonMember("DefaultHideModel")]
        [MetaTableRowColumn("DefaultHideModel","bool", "默认隐藏模型",21)]
        [LabelText("默认隐藏模型")]
        public bool DefaultHideModel ;

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "AssetPathUuid",22)]
        [LabelText("AssetPathUuid")]
        public string AssetPathUuid ;

        [JsonMember("DefaultDisableInteract")]
        [MetaTableRowColumn("DefaultDisableInteract","bool", "默认关闭交互",23)]
        [LabelText("默认关闭交互")]
        public bool DefaultDisableInteract ;

        [JsonMember("TriggerEventUuids")]
        [MetaTableRowColumn("TriggerEventUuids","string", "TriggerEventUuids",24)]
        [LabelText("TriggerEventUuids")]
        public string TriggerEventUuids ;

        [JsonMember("HotspotUuids")]
        [MetaTableRowColumn("HotspotUuids","string", "HotspotUuids",25)]
        [LabelText("HotspotUuids")]
        public string HotspotUuids ;

        [JsonMember("Id")]
        [MetaTableRowColumn("Id","int", "Id",26)]
        [LabelText("Id")]
        public int Id ;

    }
}

