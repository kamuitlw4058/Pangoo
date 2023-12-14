using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionCanvasGroupFadeParams : InstructionParams
    {
        [JsonMember("TargetName")]
        public string TargetName;
        
        [JsonMember("AlphaValue")]
        public float AlphaValue;
        
        [JsonMember("TweenTime")]
        public float TweenTime;

        [JsonMember("WaitFinsh")]
        public bool WaitFinsh;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionCanvasGroupFadeParams>(val);
            TargetName = par.TargetName;
            AlphaValue = par.AlphaValue;
            TweenTime = par.TweenTime;
            WaitFinsh = par.WaitFinsh;
        }
    }
}
