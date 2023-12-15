using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionTweenKillParams : InstructionParams
    {
        [JsonMember("TweenID")]
        public string TweenID;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionTweenKillParams>(val);
            TweenID = par.TweenID;
        }
    }
}
