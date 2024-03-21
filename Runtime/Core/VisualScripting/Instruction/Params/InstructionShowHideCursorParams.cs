using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionShowHideCursorParams : InstructionParams
    {
        [JsonMember("CursorType")]
        public CursorTypeEnum CursorType;
    }
}