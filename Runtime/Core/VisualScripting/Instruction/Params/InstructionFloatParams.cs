using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Newtonsoft.Json;
using UnityEngine.Serialization;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionFloatParams : InstructionParams
    {
        [JsonMember("Val1")]
        public float Val1;
        [JsonMember("Val2")]
        public float Val2;
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionFloatParams>(val);
            Val1 = par.Val1;
            Val2 = par.Val2;
        }
    }
}