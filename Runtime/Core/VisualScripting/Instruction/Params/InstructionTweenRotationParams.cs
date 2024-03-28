using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionTweenRotationParams : InstructionParams
    {
        [LabelText("路径列表")]
        [JsonMember("Path")]
        public string Path;
        [JsonMember("Rotation")]
        public Vector3 Rotation;
        [JsonMember("Duration")]
        public float Duration;
        
        [JsonMember("WaitToComplete")]
        public bool WaitToComplete;
    }
}

