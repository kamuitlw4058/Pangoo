using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;
using Pangoo.Common;

namespace Pangoo.Core.Common
{
    public class JsonParams : JsonSerializer, IParams
    {
        public virtual void Load(string val)
        {
            Deserialize(val);
        }
        public virtual string Save()
        {
            return SerializeToString();
        }
    }
}