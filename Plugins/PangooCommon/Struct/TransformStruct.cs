using System;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Common
{
    public struct PPose
    {
        [JsonMember("Position")]
        [LabelText("位置")]
        public Vector3 Position;

        [JsonMember("Rotation")]
        [LabelText("旋转")]
        public Vector3 Rotation;
    }

    public struct PColliderRange
    {
        [JsonMember("Center")]
        [LabelText("中心位置")]
        public Vector3 Center;

        [JsonMember("Size")]
        [LabelText("大小")]
        public Vector3 Size;

    }
}