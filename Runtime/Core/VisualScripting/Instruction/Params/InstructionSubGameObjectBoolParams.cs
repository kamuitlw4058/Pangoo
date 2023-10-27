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
    public class InstructionSubGameObjectBoolParams : InstructionParams
    {
        [JsonMember("Path")]
        public string Path;

        [JsonMember("Val")]
        public bool Val;
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionSubGameObjectBoolParams>(val);
            Val = par.Val;
            Path = par.Path;
        }
    }
}