using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionSubtitleStringParams : InstructionParams
    {
        [JsonMember("Context")]
        public string Context;

        [JsonMember("Duration")]
        public float Duration;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionSubtitleStringParams>(val);
            Context = par.Context;
            Duration = par.Duration;

        }
    }
}