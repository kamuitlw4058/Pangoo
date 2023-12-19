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
    public partial class DynamicObjectRow : MetaTableRow,IDynamicObjectRow
    {

        [JsonMember("AssetPathId")]
        [MetaTableRowColumn("AssetPathId","int", "资源路径",3)]
        [LabelText("资源路径")]
        public int AssetPathId ;

        int IDynamicObjectRow.AssetPathId {get => AssetPathId; set => AssetPathId = value;}

        [JsonMember("NameCn")]
        [MetaTableRowColumn("NameCn","string", "中文名",4)]
        [LabelText("中文名")]
        public string NameCn ;

        string IDynamicObjectRow.NameCn {get => NameCn; set => NameCn = value;}

        [JsonMember("Space")]
        [MetaTableRowColumn("Space","string", "位置空间",5)]
        [LabelText("位置空间")]
        public string Space ;

        string IDynamicObjectRow.Space {get => Space; set => Space = value;}

        [JsonMember("Position")]
        [MetaTableRowColumn("Position","Vector3", "位置",6)]
        [LabelText("位置")]
        public Vector3 Position ;

        Vector3 IDynamicObjectRow.Position {get => Position; set => Position = value;}

        [JsonMember("Rotation")]
        [MetaTableRowColumn("Rotation","Vector3", "旋转",7)]
        [LabelText("旋转")]
        public Vector3 Rotation ;

        Vector3 IDynamicObjectRow.Rotation {get => Rotation; set => Rotation = value;}

        [JsonMember("Scale")]
        [MetaTableRowColumn("Scale","Vector3", "缩放",8)]
        [LabelText("缩放")]
        public Vector3 Scale ;

        Vector3 IDynamicObjectRow.Scale {get => Scale; set => Scale = value;}

        [JsonMember("TriggerEventIds")]
        [MetaTableRowColumn("TriggerEventIds","string", "触发事件Ids",9)]
        [LabelText("触发事件Ids")]
        public string TriggerEventIds ;

        string IDynamicObjectRow.TriggerEventIds {get => TriggerEventIds; set => TriggerEventIds = value;}

        [JsonMember("UseHotspot")]
        [MetaTableRowColumn("UseHotspot","bool", "使用热点区域",10)]
        [LabelText("使用热点区域")]
        public bool UseHotspot ;

        bool IDynamicObjectRow.UseHotspot {get => UseHotspot; set => UseHotspot = value;}

        [JsonMember("DefaultHideHotspot")]
        [MetaTableRowColumn("DefaultHideHotspot","bool", "默认关闭热点",11)]
        [LabelText("默认关闭热点")]
        public bool DefaultHideHotspot ;

        bool IDynamicObjectRow.DefaultHideHotspot {get => DefaultHideHotspot; set => DefaultHideHotspot = value;}

        [JsonMember("HotspotRadius")]
        [MetaTableRowColumn("HotspotRadius","float", "热点区域的范围",12)]
        [LabelText("热点区域的范围")]
        public float HotspotRadius ;

        float IDynamicObjectRow.HotspotRadius {get => HotspotRadius; set => HotspotRadius = value;}

        [JsonMember("HotspotIds")]
        [MetaTableRowColumn("HotspotIds","string", "热点区域Ids",13)]
        [LabelText("热点区域Ids")]
        public string HotspotIds ;

        string IDynamicObjectRow.HotspotIds {get => HotspotIds; set => HotspotIds = value;}

        [JsonMember("HotspotOffset")]
        [MetaTableRowColumn("HotspotOffset","Vector3", "热点区域偏移",14)]
        [LabelText("热点区域偏移")]
        public Vector3 HotspotOffset ;

        Vector3 IDynamicObjectRow.HotspotOffset {get => HotspotOffset; set => HotspotOffset = value;}

        [JsonMember("DirectInstructions")]
        [MetaTableRowColumn("DirectInstructions","string", "直接指令",15)]
        [LabelText("直接指令")]
        public string DirectInstructions ;

        string IDynamicObjectRow.DirectInstructions {get => DirectInstructions; set => DirectInstructions = value;}

        [JsonMember("SubDynamicObject")]
        [MetaTableRowColumn("SubDynamicObject","string", "子物体",16)]
        [LabelText("子物体")]
        public string SubDynamicObject ;

        string IDynamicObjectRow.SubDynamicObject {get => SubDynamicObject; set => SubDynamicObject = value;}

        [JsonMember("InteractRadius")]
        [MetaTableRowColumn("InteractRadius","float", "可交互半径",17)]
        [LabelText("可交互半径")]
        public float InteractRadius ;

        float IDynamicObjectRow.InteractRadius {get => InteractRadius; set => InteractRadius = value;}

        [JsonMember("InteractOffset")]
        [MetaTableRowColumn("InteractOffset","Vector3", "交互点偏移",18)]
        [LabelText("交互点偏移")]
        public Vector3 InteractOffset ;

        Vector3 IDynamicObjectRow.InteractOffset {get => InteractOffset; set => InteractOffset = value;}

        [JsonMember("InteractRadian")]
        [MetaTableRowColumn("InteractRadian","float", "可交互角度",19)]
        [LabelText("可交互角度")]
        public float InteractRadian ;

        float IDynamicObjectRow.InteractRadian {get => InteractRadian; set => InteractRadian = value;}

        [JsonMember("InteractTarget")]
        [MetaTableRowColumn("InteractTarget","string", "交互对象",20)]
        [LabelText("交互对象")]
        public string InteractTarget ;

        string IDynamicObjectRow.InteractTarget {get => InteractTarget; set => InteractTarget = value;}

        [JsonMember("DefaultHideModel")]
        [MetaTableRowColumn("DefaultHideModel","bool", "默认隐藏模型",21)]
        [LabelText("默认隐藏模型")]
        public bool DefaultHideModel ;

        bool IDynamicObjectRow.DefaultHideModel {get => DefaultHideModel; set => DefaultHideModel = value;}

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "AssetPathUuid",22)]
        [LabelText("AssetPathUuid")]
        public string AssetPathUuid ;

        string IDynamicObjectRow.AssetPathUuid {get => AssetPathUuid; set => AssetPathUuid = value;}

        [JsonMember("DefaultDisableInteract")]
        [MetaTableRowColumn("DefaultDisableInteract","bool", "默认关闭交互",23)]
        [LabelText("默认关闭交互")]
        public bool DefaultDisableInteract ;

        bool IDynamicObjectRow.DefaultDisableInteract {get => DefaultDisableInteract; set => DefaultDisableInteract = value;}

        [JsonMember("TriggerEventUuids")]
        [MetaTableRowColumn("TriggerEventUuids","string", "TriggerEventUuids",24)]
        [LabelText("TriggerEventUuids")]
        public string TriggerEventUuids ;

        string IDynamicObjectRow.TriggerEventUuids {get => TriggerEventUuids; set => TriggerEventUuids = value;}

        [JsonMember("HotspotUuids")]
        [MetaTableRowColumn("HotspotUuids","string", "HotspotUuids",25)]
        [LabelText("HotspotUuids")]
        public string HotspotUuids ;

        string IDynamicObjectRow.HotspotUuids {get => HotspotUuids; set => HotspotUuids = value;}

        [JsonMember("Id")]
        [MetaTableRowColumn("Id","int", "Id",26)]
        [LabelText("Id")]
        public int Id ;

        int IDynamicObjectRow.Id {get => Id; set => Id = value;}

    }
}

