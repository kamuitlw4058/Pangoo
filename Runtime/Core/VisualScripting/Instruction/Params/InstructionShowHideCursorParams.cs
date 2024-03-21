using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionShowHideCursorParams : InstructionParams
    {
        [JsonMember("CursorTypeEnum")]
        public CursorTypeEnum CursorTypeEnum;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionShowHideCursorParams>(val);
            CursorTypeEnum = par.CursorTypeEnum;
        }
    }
}