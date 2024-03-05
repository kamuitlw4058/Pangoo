using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionChangeHotspotSpriteParams : InstructionParams
    {
        [JsonMember("TargetName")]
        public string TargetName;
        [JsonMember("State")]
        public DynamicObjectHotsoptState State;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionChangeHotspotSpriteParams>(val);
            TargetName = par.TargetName;
            State = par.State;
        }
    }
}
