using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class VariableParams : IParams
    {
        public virtual void Load(string val)
        {

        }
        public virtual string Save()
        {
            return string.Empty;
        }
    }
}