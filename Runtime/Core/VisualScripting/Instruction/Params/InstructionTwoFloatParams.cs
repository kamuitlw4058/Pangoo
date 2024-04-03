using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionTwoFloatParams : InstructionParams
    {
        [JsonMember("Val")]
        public float Val1;

        [JsonMember("Val2")]
        public float Val2;
    }
}
