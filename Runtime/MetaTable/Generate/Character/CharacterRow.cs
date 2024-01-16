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
    public partial class CharacterRow : MetaTableRow,ICharacterRow
    {

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "资源Uuid",3)]
        [LabelText("资源Uuid")]
        public string AssetPathUuid ;

        string ICharacterRow.AssetPathUuid {get => AssetPathUuid; set => AssetPathUuid = value;}

        [JsonMember("IsPlayer")]
        [MetaTableRowColumn("IsPlayer","bool", "是否是玩家",4)]
        [LabelText("是否是玩家")]
        public bool IsPlayer ;

        bool ICharacterRow.IsPlayer {get => IsPlayer; set => IsPlayer = value;}

        [JsonMember("MoveSpeed")]
        [MetaTableRowColumn("MoveSpeed","float", "移动速度",5)]
        [LabelText("移动速度")]
        public float MoveSpeed ;

        float ICharacterRow.MoveSpeed {get => MoveSpeed; set => MoveSpeed = value;}

        [JsonMember("Height")]
        [MetaTableRowColumn("Height","float", "高度",6)]
        [LabelText("高度")]
        public float Height ;

        float ICharacterRow.Height {get => Height; set => Height = value;}

        [JsonMember("CameraOffset")]
        [MetaTableRowColumn("CameraOffset","Vector3", "相机偏移",7)]
        [LabelText("相机偏移")]
        public Vector3 CameraOffset ;

        Vector3 ICharacterRow.CameraOffset {get => CameraOffset; set => CameraOffset = value;}

        [JsonMember("XMaxPitch")]
        [MetaTableRowColumn("XMaxPitch","float", "俯仰范围",8)]
        [LabelText("俯仰范围")]
        public float XMaxPitch ;

        float ICharacterRow.XMaxPitch {get => XMaxPitch; set => XMaxPitch = value;}

        [JsonMember("YMaxPitch")]
        [MetaTableRowColumn("YMaxPitch","float", "左右范围",9)]
        [LabelText("左右范围")]
        public float YMaxPitch ;

        float ICharacterRow.YMaxPitch {get => YMaxPitch; set => YMaxPitch = value;}

        [JsonMember("CameraOnly")]
        [MetaTableRowColumn("CameraOnly","bool", "只使用相机",10)]
        [LabelText("只使用相机")]
        public bool CameraOnly ;

        bool ICharacterRow.CameraOnly {get => CameraOnly; set => CameraOnly = value;}

        [JsonMember("AssetPathId")]
        [MetaTableRowColumn("AssetPathId","int", "AssetPathId",12)]
        [LabelText("AssetPathId")]
        public int AssetPathId ;

        int ICharacterRow.AssetPathId {get => AssetPathId; set => AssetPathId = value;}

        [JsonMember("SubDynamicObject")]
        [MetaTableRowColumn("SubDynamicObject","string", "子对象",13)]
        [LabelText("子对象")]
        public string SubDynamicObject ;

        string ICharacterRow.SubDynamicObject {get => SubDynamicObject; set => SubDynamicObject = value;}

        [JsonMember("SlopeLimit")]
        [MetaTableRowColumn("SlopeLimit","float", "斜坡限制",14)]
        [LabelText("斜坡限制")]
        public float SlopeLimit ;

        float ICharacterRow.SlopeLimit {get => SlopeLimit; set => SlopeLimit = value;}

        [JsonMember("StepOffset")]
        [MetaTableRowColumn("StepOffset","float", "步高限制",15)]
        [LabelText("步高限制")]
        public float StepOffset ;

        float ICharacterRow.StepOffset {get => StepOffset; set => StepOffset = value;}

        [JsonMember("SkinWidth")]
        [MetaTableRowColumn("SkinWidth","float", "皮肤宽度",16)]
        [LabelText("皮肤宽度")]
        public float SkinWidth ;

        float ICharacterRow.SkinWidth {get => SkinWidth; set => SkinWidth = value;}

        [JsonMember("MinMoveDistance")]
        [MetaTableRowColumn("MinMoveDistance","float", "最小移动距离",17)]
        [LabelText("最小移动距离")]
        public float MinMoveDistance ;

        float ICharacterRow.MinMoveDistance {get => MinMoveDistance; set => MinMoveDistance = value;}

        [JsonMember("Center")]
        [MetaTableRowColumn("Center","Vector3", "中心",18)]
        [LabelText("中心")]
        public Vector3 Center ;

        Vector3 ICharacterRow.Center {get => Center; set => Center = value;}

        [JsonMember("Radius")]
        [MetaTableRowColumn("Radius","float", "半径",19)]
        [LabelText("半径")]
        public float Radius ;

        float ICharacterRow.Radius {get => Radius; set => Radius = value;}

    }
}

