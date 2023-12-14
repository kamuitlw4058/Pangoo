using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionShowHideCursorParams : InstructionParams
    {
        [JsonMember("IsShow")]
        public bool IsShow;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionShowHideCursorParams>(val);
            IsShow = par.IsShow;
        }
    }
}