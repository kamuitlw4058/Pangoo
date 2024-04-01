using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionTweenRotationParams : InstructionParams
    {
        [LabelText("路径列表")]
        [JsonMember("Path")]
        public string Path;
        [JsonMember("InitRotation")]
        public Vector3 InitRotation;
        [JsonMember("TargetRotation")]
        public Vector3 TargetRotation;
        [JsonMember("Duration")]
        public float Duration;
        
        [JsonMember("WaitToComplete")]
        public bool WaitToComplete;
    }
}

