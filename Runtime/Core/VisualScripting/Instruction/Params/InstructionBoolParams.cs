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
    public class InstructionBoolParams : InstructionParams
    {
        [JsonMember("Val")]
        public bool Val;
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionBoolParams>(val);
            Val = par.Val;
        }
    }
}