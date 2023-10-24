using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionPlayTimelineParams : InstructionParams
    {
        [JsonMember("WaitToComplete")]
        public bool WaitToComplete;
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionPlayTimelineParams>(val);
            WaitToComplete = par.WaitToComplete;
        }
    }
}
