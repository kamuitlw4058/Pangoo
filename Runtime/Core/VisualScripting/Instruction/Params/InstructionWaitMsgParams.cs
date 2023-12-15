using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionWaitMsgParams : InstructionParams
    {
        [JsonMember("ConditionString")]
        public string ConditionString;
        

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionWaitMsgParams>(val);
            ConditionString = par.ConditionString;
        }
    }
}
