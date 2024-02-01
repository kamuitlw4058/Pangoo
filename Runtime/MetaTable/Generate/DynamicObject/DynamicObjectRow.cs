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
    public partial class DynamicObjectRow : MetaTableRow,IDynamicObjectRow
    {

        [JsonMember("NameCn")]
        [MetaTableRowColumn("NameCn","string", "中文名",3)]
        [LabelText("中文名")]
        public string NameCn ;

        string IDynamicObjectRow.NameCn {get => NameCn; set => NameCn = value;}

        [JsonMember("Space")]
        [MetaTableRowColumn("Space","string", "位置空间",4)]
        [LabelText("位置空间")]
        public string Space ;

        string IDynamicObjectRow.Space {get => Space; set => Space = value;}

        [JsonMember("Position")]
        [MetaTableRowColumn("Position","Vector3", "位置",5)]
        [LabelText("位置")]
        public Vector3 Position ;

        Vector3 IDynamicObjectRow.Position {get => Position; set => Position = value;}

        [JsonMember("Rotation")]
        [MetaTableRowColumn("Rotation","Vector3", "旋转",6)]
        [LabelText("旋转")]
        public Vector3 Rotation ;

        Vector3 IDynamicObjectRow.Rotation {get => Rotation; set => Rotation = value;}

        [JsonMember("Scale")]
        [MetaTableRowColumn("Scale","Vector3", "缩放",7)]
        [LabelText("缩放")]
        public Vector3 Scale ;

        Vector3 IDynamicObjectRow.Scale {get => Scale; set => Scale = value;}

        [JsonMember("UseHotspot")]
        [MetaTableRowColumn("UseHotspot","bool", "使用热点区域",8)]
        [LabelText("使用热点区域")]
        public bool UseHotspot ;

        bool IDynamicObjectRow.UseHotspot {get => UseHotspot; set => UseHotspot = value;}

        [JsonMember("DefaultHideHotspot")]
        [MetaTableRowColumn("DefaultHideHotspot","bool", "默认关闭热点",9)]
        [LabelText("默认关闭热点")]
        public bool DefaultHideHotspot ;

        bool IDynamicObjectRow.DefaultHideHotspot {get => DefaultHideHotspot; set => DefaultHideHotspot = value;}

        [JsonMember("HotspotRadius")]
        [MetaTableRowColumn("HotspotRadius","float", "热点区域的范围",10)]
        [LabelText("热点区域的范围")]
        public float HotspotRadius ;

        float IDynamicObjectRow.HotspotRadius {get => HotspotRadius; set => HotspotRadius = value;}

        [JsonMember("HotspotOffset")]
        [MetaTableRowColumn("HotspotOffset","Vector3", "热点区域偏移",11)]
        [LabelText("热点区域偏移")]
        public Vector3 HotspotOffset ;

        Vector3 IDynamicObjectRow.HotspotOffset {get => HotspotOffset; set => HotspotOffset = value;}

        [JsonMember("DirectInstructions")]
        [MetaTableRowColumn("DirectInstructions","string", "直接指令",12)]
        [LabelText("直接指令")]
        public string DirectInstructions ;

        string IDynamicObjectRow.DirectInstructions {get => DirectInstructions; set => DirectInstructions = value;}

        [JsonMember("SubDynamicObject")]
        [MetaTableRowColumn("SubDynamicObject","string", "子物体",13)]
        [LabelText("子物体")]
        public string SubDynamicObject ;

        string IDynamicObjectRow.SubDynamicObject {get => SubDynamicObject; set => SubDynamicObject = value;}

        [JsonMember("InteractRadius")]
        [MetaTableRowColumn("InteractRadius","float", "可交互半径",14)]
        [LabelText("可交互半径")]
        public float InteractRadius ;

        float IDynamicObjectRow.InteractRadius {get => InteractRadius; set => InteractRadius = value;}

        [JsonMember("InteractOffset")]
        [MetaTableRowColumn("InteractOffset","Vector3", "交互点偏移",15)]
        [LabelText("交互点偏移")]
        public Vector3 InteractOffset ;

        Vector3 IDynamicObjectRow.InteractOffset {get => InteractOffset; set => InteractOffset = value;}

        [JsonMember("InteractRadian")]
        [MetaTableRowColumn("InteractRadian","float", "可交互角度",16)]
        [LabelText("可交互角度")]
        public float InteractRadian ;

        float IDynamicObjectRow.InteractRadian {get => InteractRadian; set => InteractRadian = value;}

        [JsonMember("InteractTarget")]
        [MetaTableRowColumn("InteractTarget","string", "交互对象",17)]
        [LabelText("交互对象")]
        public string InteractTarget ;

        string IDynamicObjectRow.InteractTarget {get => InteractTarget; set => InteractTarget = value;}

        [JsonMember("DefaultHideModel")]
        [MetaTableRowColumn("DefaultHideModel","bool", "默认隐藏模型",18)]
        [LabelText("默认隐藏模型")]
        public bool DefaultHideModel ;

        bool IDynamicObjectRow.DefaultHideModel {get => DefaultHideModel; set => DefaultHideModel = value;}

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "AssetPathUuid",19)]
        [LabelText("AssetPathUuid")]
        public string AssetPathUuid ;

        string IDynamicObjectRow.AssetPathUuid {get => AssetPathUuid; set => AssetPathUuid = value;}

        [JsonMember("DefaultDisableInteract")]
        [MetaTableRowColumn("DefaultDisableInteract","bool", "默认关闭交互",20)]
        [LabelText("默认关闭交互")]
        public bool DefaultDisableInteract ;

        bool IDynamicObjectRow.DefaultDisableInteract {get => DefaultDisableInteract; set => DefaultDisableInteract = value;}

        [JsonMember("TriggerEventUuids")]
        [MetaTableRowColumn("TriggerEventUuids","string", "TriggerEventUuids",21)]
        [LabelText("TriggerEventUuids")]
        public string TriggerEventUuids ;

        string IDynamicObjectRow.TriggerEventUuids {get => TriggerEventUuids; set => TriggerEventUuids = value;}

        [JsonMember("HotspotUuids")]
        [MetaTableRowColumn("HotspotUuids","string", "HotspotUuids",22)]
        [LabelText("HotspotUuids")]
        public string HotspotUuids ;

        string IDynamicObjectRow.HotspotUuids {get => HotspotUuids; set => HotspotUuids = value;}

        [JsonMember("PreviewName")]
        [MetaTableRowColumn("PreviewName","string", "预览名字",23)]
        [LabelText("预览名字")]
        public string PreviewName ;

        string IDynamicObjectRow.PreviewName {get => PreviewName; set => PreviewName = value;}

        [JsonMember("PreviewDesc")]
        [MetaTableRowColumn("PreviewDesc","string", "预览描述",24)]
        [LabelText("预览描述")]
        public string PreviewDesc ;

        string IDynamicObjectRow.PreviewDesc {get => PreviewDesc; set => PreviewDesc = value;}

        [JsonMember("StateSubDynamicObject")]
        [MetaTableRowColumn("StateSubDynamicObject","string", "状态子物体",25)]
        [LabelText("状态子物体")]
        public string StateSubDynamicObject ;

        string IDynamicObjectRow.StateSubDynamicObject {get => StateSubDynamicObject; set => StateSubDynamicObject = value;}

        [JsonMember("StateVariableUuid")]
        [MetaTableRowColumn("StateVariableUuid","string", "状态变量Uuid",26)]
        [LabelText("状态变量Uuid")]
        public string StateVariableUuid ;

        string IDynamicObjectRow.StateVariableUuid {get => StateVariableUuid; set => StateVariableUuid = value;}

        [JsonMember("PreviewRotation")]
        [MetaTableRowColumn("PreviewRotation","Vector3", "预览旋转",27)]
        [LabelText("预览旋转")]
        public Vector3 PreviewRotation ;

        Vector3 IDynamicObjectRow.PreviewRotation {get => PreviewRotation; set => PreviewRotation = value;}

        [JsonMember("PreviewScale")]
        [MetaTableRowColumn("PreviewScale","Vector3", "预览缩放",28)]
        [LabelText("预览缩放")]
        public Vector3 PreviewScale ;

        Vector3 IDynamicObjectRow.PreviewScale {get => PreviewScale; set => PreviewScale = value;}

        [JsonMember("SubObjectTriggerList")]
        [MetaTableRowColumn("SubObjectTriggerList","string", "子物体触发器列表",29)]
        [LabelText("子物体触发器列表")]
        public string SubObjectTriggerList ;

        string IDynamicObjectRow.SubObjectTriggerList {get => SubObjectTriggerList; set => SubObjectTriggerList = value;}

    }
}

