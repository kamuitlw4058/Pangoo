using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Newtonsoft.Json;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionFloatParams : InstructionParams
    {
        [JsonMember("Val")]
        public float Val;
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionFloatParams>(val);
            Val = par.Val;
        }
    }
}