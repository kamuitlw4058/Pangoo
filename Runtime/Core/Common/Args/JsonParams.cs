using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;

namespace Pangoo.Core.Common
{
    public class JsonParams : Params
    {
        public override void Load(string val) { }
        public override string Save()
        {
            return JsonMapper.ToJson(this);
        }
    }
}