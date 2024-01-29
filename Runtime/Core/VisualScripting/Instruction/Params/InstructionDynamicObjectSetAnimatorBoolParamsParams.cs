using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionDynamicObjectSetAnimatorBoolParamsParams : InstructionParams
    {
        [JsonMember("TargetPath")]
        public string TargetPath;
        [JsonMember("ParamsName")]
        public string ParamsName;
        [JsonMember("Val")]
        public bool Val;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionDynamicObjectSetAnimatorBoolParamsParams>(val);
            TargetPath = par.TargetPath;
        }
    }
}

