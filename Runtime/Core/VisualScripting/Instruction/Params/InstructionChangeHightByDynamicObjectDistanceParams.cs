using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.VisualScripting;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionChangeHightByDynamicObjectDistanceParams : InstructionParams
    {
        [JsonMember("StartDistance")]
        public float StartDistance;
        [JsonMember("EndDistance")]
        public float EndDistance;

        [JsonMember("StartHeight")]
        public float StartHeight;
        [JsonMember("EndHeight")]
        public float EndHeight;
        public float TwoPointDistance => EndDistance - StartDistance;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionChangeHightByDynamicObjectDistanceParams>(val);
            EndDistance = par.EndDistance;
            StartDistance = par.StartDistance;
            StartHeight = par.StartHeight;
            EndHeight = par.EndHeight;
        }
    }
}
