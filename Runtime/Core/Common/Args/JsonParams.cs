using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;

namespace Pangoo.Core.Common
{
    public class JsonParams
    {
        public virtual void LoadFromJson(string val) { }
        public virtual string ToJson()
        {
            return JsonMapper.ToJson(this);
        }
    }
}