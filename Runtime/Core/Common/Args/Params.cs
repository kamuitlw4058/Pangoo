using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;

namespace Pangoo.Core.Common
{
    public abstract class Params : IParams
    {
        public virtual void Load(string val) { }
        public virtual string Save()
        {
            return string.Empty;
        }
    }
}