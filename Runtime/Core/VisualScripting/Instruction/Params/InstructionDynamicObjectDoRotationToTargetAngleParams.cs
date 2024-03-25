using System.Collections;
using System.Collections.Generic;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionDynamicObjectDoRotationToTargetAngleParams : InstructionParams
    {
        [JsonMember("Path")]
        public string Path;
        [JsonMember("InitRotation")]
        public Vector3 InitRotation;
        [JsonMember("TargetRotation")]
        public Vector3 TargetRotation;
        [JsonMember("RotationSpeed")]
        public float RotationSpeed = 5f;
    }
}

