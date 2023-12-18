using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionShowHideCursorParams : InstructionParams
    {
        [JsonMember("Visible")]
        public bool Visible;
        [JsonMember("CursorLockMode")]
        public CursorLockMode CursorLockMode;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionShowHideCursorParams>(val);
            Visible = par.Visible;
            CursorLockMode = par.CursorLockMode;
        }
    }
}