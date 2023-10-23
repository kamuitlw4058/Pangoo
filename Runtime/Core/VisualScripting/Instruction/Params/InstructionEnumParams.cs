using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionEnumParams<T> : InstructionParams where T : Enum
    {
        public T EnumVal;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject(val, GetType()) as InstructionEnumParams<T>;
            EnumVal = par.EnumVal;
        }
    }
}