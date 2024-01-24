using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.VisualScripting;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionChangeHightByDynamicObjectDistanceParams : InstructionParams
    {
        [JsonMember("MaxDistance")]
        public float MaxDistance;
        [JsonMember("MinDistance")]
        public float MinDistance;
        [JsonMember("MinHeight")]
        public float MinHeight;
        [JsonMember("MaxHeight")]
        public float MaxHeight;
        [JsonMember("Direction")]
        public float Direction;
        public float TwoPointDistance => MaxDistance - MinDistance;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionChangeHightByDynamicObjectDistanceParams>(val);
            MaxDistance = par.MaxDistance;
            MinDistance = par.MinDistance;
            MinHeight = par.MinHeight;
            MaxHeight = par.MaxHeight;
            Direction = par.Direction;
        }
    }
}
