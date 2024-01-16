using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.VisualScripting;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionTweenLightIntensityParams : InstructionParams
    {
        [JsonMember("TargetPath")]
        public string TargetPath;
        [JsonMember("Value")]
        public float Val;
        
        [JsonMember("TweenTime")]
        public float TweenTime;

        [JsonMember("WaitFinsh")]
        public bool WaitFinsh;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionTweenLightIntensityParams>(val);
            TargetPath = par.TargetPath;
            Val = par.Val;
            TweenTime = par.TweenTime;
            WaitFinsh = par.WaitFinsh;
        }
    }
}
