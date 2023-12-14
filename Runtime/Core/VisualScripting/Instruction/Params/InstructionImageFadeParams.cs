using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionImageFadeParams : InstructionParams
    {
        [JsonMember("AlphaValue")]
        public float AlphaValue;
        
        [JsonMember("TweenTime")]
        public float TweenTime;
        [JsonMember("TweenID")]
        public string TweenID;
        
        [JsonMember("WaitFinsh")]
        public bool WaitFinsh;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionImageFadeParams>(val);
            AlphaValue = par.AlphaValue;
            TweenTime = par.TweenTime;
            TweenID = par.TweenID;
            WaitFinsh = par.WaitFinsh;
        }
        
    }
}