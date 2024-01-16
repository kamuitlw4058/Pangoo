using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.Characters
{
    [Serializable]
    public class DriverInfo
    {
        [VerticalGroup("DriverInfo")]
        [VerticalGroup("DriverInfo/CharacterDriverTypeEnum")]
        [JsonMember("CharacterDriverTypeEnum")]
        public CharacterDriverTypeEnum CharacterDriverTypeEnum;
        [LabelText("斜坡限制")]
        [LabelWidth(120)]
        [VerticalGroup("DriverInfo/SlopeLimit")]
        [JsonMember("SlopeLimit")]
        public float SlopeLimit=45f;
        [VerticalGroup("DriverInfo/StepOffset")]
        [LabelText("步高限制")]
        [LabelWidth(120)]
        [JsonMember("StepOffset")]
        public float StepOffset=0.3f;
        
        [VerticalGroup("DriverInfo/SkinWidth")]
        [LabelText("皮肤宽度")]
        [LabelWidth(120)]
        [JsonMember("SkinWidth")]
        public float SkinWidth=0.08f;
        [VerticalGroup("DriverInfo/MinMoveDistance")]
        [LabelText("最小移动距离")]
        [LabelWidth(120)]
        [JsonMember("MinMoveDistance")]
        public float MinMoveDistance=0.001f;
        [VerticalGroup("DriverInfo/Center")]
        [LabelText("中心")]
        [LabelWidth(120)]
        [JsonMember("Center")]
        public Vector3 Center;
        [VerticalGroup("DriverInfo/Radius")]
        [LabelText("半径")]
        [LabelWidth(120)]
        [JsonMember("Radius")]
        public float Radius=0.5f;
        [VerticalGroup("DriverInfo/Height")]
        [LabelText("高度")]
        [LabelWidth(120)]
        [JsonMember("Height")]
        public float Height=2f;
    }
}
