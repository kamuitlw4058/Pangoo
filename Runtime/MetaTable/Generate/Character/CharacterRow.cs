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

        [JsonMember("RunSpeed")]
        [MetaTableRowColumn("RunSpeed","float", "跑步速度",6)]
        [LabelText("跑步速度")]
        public float RunSpeed ;

        float ICharacterRow.RunSpeed {get => RunSpeed; set => RunSpeed = value;}

        [JsonMember("CameraHeight")]
        [MetaTableRowColumn("CameraHeight","float", "相机高度",7)]
        [LabelText("相机高度")]
        public float CameraHeight ;

        float ICharacterRow.CameraHeight {get => CameraHeight; set => CameraHeight = value;}

        [JsonMember("CameraOffset")]
        [MetaTableRowColumn("CameraOffset","Vector3", "相机偏移",8)]
        [LabelText("相机偏移")]
        public Vector3 CameraOffset ;

        Vector3 ICharacterRow.CameraOffset {get => CameraOffset; set => CameraOffset = value;}

        [JsonMember("XMaxPitch")]
        [MetaTableRowColumn("XMaxPitch","float", "俯仰范围",9)]
        [LabelText("俯仰范围")]
        public float XMaxPitch ;

        float ICharacterRow.XMaxPitch {get => XMaxPitch; set => XMaxPitch = value;}

        [JsonMember("YMaxPitch")]
        [MetaTableRowColumn("YMaxPitch","float", "左右范围",10)]
        [LabelText("左右范围")]
        public float YMaxPitch ;

        float ICharacterRow.YMaxPitch {get => YMaxPitch; set => YMaxPitch = value;}

        [JsonMember("CameraOnly")]
        [MetaTableRowColumn("CameraOnly","bool", "只使用相机",11)]
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

        [JsonMember("CharacterSlopeLimit")]
        [MetaTableRowColumn("CharacterSlopeLimit","float", "角色斜坡限制",14)]
        [LabelText("角色斜坡限制")]
        public float CharacterSlopeLimit ;

        float ICharacterRow.CharacterSlopeLimit {get => CharacterSlopeLimit; set => CharacterSlopeLimit = value;}

        [JsonMember("CharacterStepOffset")]
        [MetaTableRowColumn("CharacterStepOffset","float", "角色步高限制",15)]
        [LabelText("角色步高限制")]
        public float CharacterStepOffset ;

        float ICharacterRow.CharacterStepOffset {get => CharacterStepOffset; set => CharacterStepOffset = value;}

        [JsonMember("CharacterSkinWidth")]
        [MetaTableRowColumn("CharacterSkinWidth","float", "角色皮肤宽度",16)]
        [LabelText("角色皮肤宽度")]
        public float CharacterSkinWidth ;

        float ICharacterRow.CharacterSkinWidth {get => CharacterSkinWidth; set => CharacterSkinWidth = value;}

        [JsonMember("CharacterMinMoveDistance")]
        [MetaTableRowColumn("CharacterMinMoveDistance","float", "角色最小移动距离",17)]
        [LabelText("角色最小移动距离")]
        public float CharacterMinMoveDistance ;

        float ICharacterRow.CharacterMinMoveDistance {get => CharacterMinMoveDistance; set => CharacterMinMoveDistance = value;}

        [JsonMember("ColliderCenter")]
        [MetaTableRowColumn("ColliderCenter","Vector3", "碰撞中心",18)]
        [LabelText("碰撞中心")]
        public Vector3 ColliderCenter ;

        Vector3 ICharacterRow.ColliderCenter {get => ColliderCenter; set => ColliderCenter = value;}

        [JsonMember("ColliderRadius")]
        [MetaTableRowColumn("ColliderRadius","float", "碰撞半径",19)]
        [LabelText("碰撞半径")]
        public float ColliderRadius ;

        float ICharacterRow.ColliderRadius {get => ColliderRadius; set => ColliderRadius = value;}

        [JsonMember("ColliderHeight")]
        [MetaTableRowColumn("ColliderHeight","float", "碰撞高度",20)]
        [LabelText("碰撞高度")]
        public float ColliderHeight ;

        float ICharacterRow.ColliderHeight {get => ColliderHeight; set => ColliderHeight = value;}

        [JsonMember("CharacterConfigUuid")]
        [MetaTableRowColumn("CharacterConfigUuid","string", "默认角色配置",21)]
        [LabelText("默认角色配置")]
        public string CharacterConfigUuid ;

        string ICharacterRow.CharacterConfigUuid {get => CharacterConfigUuid; set => CharacterConfigUuid = value;}

    }
}

