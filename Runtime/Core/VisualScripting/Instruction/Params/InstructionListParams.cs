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
    public class InstructionListParams : InstructionParams
    {
        [JsonMember("Instructions")]
        public DirectInstructionList Instructions;
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionListParams>(val);
            Instructions = par.Instructions;
        }
    }
}