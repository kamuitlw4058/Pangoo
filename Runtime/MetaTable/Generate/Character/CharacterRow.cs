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
    public partial class CharacterRow : MetaTableRow
    {

        [JsonMember("AssetPathUuid")]
        [MetaTableRowColumn("AssetPathUuid","string", "资源Uuid",3)]
        [LabelText("资源Uuid")]
        public string AssetPathUuid ;

        [JsonMember("IsPlayer")]
        [MetaTableRowColumn("IsPlayer","bool", "是否是玩家",4)]
        [LabelText("是否是玩家")]
        public bool IsPlayer ;

        [JsonMember("MoveSpeed")]
        [MetaTableRowColumn("MoveSpeed","float", "移动速度",5)]
        [LabelText("移动速度")]
        public float MoveSpeed ;

        [JsonMember("Height")]
        [MetaTableRowColumn("Height","float", "高度",6)]
        [LabelText("高度")]
        public float Height ;

        [JsonMember("CameraOffset")]
        [MetaTableRowColumn("CameraOffset","Vector3", "相机偏移",7)]
        [LabelText("相机偏移")]
        public Vector3 CameraOffset ;

        [JsonMember("XMaxPitch")]
        [MetaTableRowColumn("XMaxPitch","float", "俯仰范围",8)]
        [LabelText("俯仰范围")]
        public float XMaxPitch ;

        [JsonMember("YMaxPitch")]
        [MetaTableRowColumn("YMaxPitch","float", "左右范围",9)]
        [LabelText("左右范围")]
        public float YMaxPitch ;

        [JsonMember("CameraOnly")]
        [MetaTableRowColumn("CameraOnly","bool", "只使用相机",10)]
        [LabelText("只使用相机")]
        public bool CameraOnly ;

        [JsonMember("Id")]
        [MetaTableRowColumn("Id","int", "Id",11)]
        [LabelText("Id")]
        public int Id ;

    }
}

