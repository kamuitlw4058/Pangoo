using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionParams
    {
        public virtual void LoadFromJson(string val) { }
        public virtual string ToJson()
        {
            return JsonMapper.ToJson(this);
        }

    }
}