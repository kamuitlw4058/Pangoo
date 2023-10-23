using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;
using Pangoo.Core.VisualScripting;

namespace Pangoo.Core.Common
{
    [Serializable]
    public class DynamicObjectValue
    {
        public DynamicObject dynamicObejct { get; set; }
        public TransformValue? transformValue { get; set; }

        public Dictionary<string, TransformValue> ChilernTransforms = new Dictionary<string, TransformValue>();


        public void SetChilernTransforms(string key, TransformValue val)
        {
            if (ChilernTransforms.ContainsKey(key))
            {
                ChilernTransforms[key] = val;
            }
            else
            {
                ChilernTransforms.Add(key, val);
            }
        }

    }
}