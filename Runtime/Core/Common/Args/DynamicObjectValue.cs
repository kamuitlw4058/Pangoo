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

        public Dictionary<int, bool> TriggerEnabledDict = new Dictionary<int, bool>();

        public Dictionary<int, int> TriggerIndexDict = new();


        public void SetChilernTransforms(string key, TransformValue val)
        {
            ChilernTransforms.Set(key, val);
        }

        public void SetTriggerEnabled(int key, bool val)
        {
            TriggerEnabledDict.Set(key, val);
        }

        public void SetTriggerIndex(int key, int val)
        {
            TriggerIndexDict.Set(key, val);
        }



    }
}