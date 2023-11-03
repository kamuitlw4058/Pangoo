using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;

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

        [ShowInInspector]
        Dictionary<string, object> m_KeyValueDict = new Dictionary<string, object>();


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



        public virtual T Get<T>(string key, T defaultValue = default(T))
        {
            object value = null;
            if (m_KeyValueDict.ContainsKey(key))
            {
                value = m_KeyValueDict[key];
                // Debug.LogError($"获取的value值0：{value}");
                return (T)value;
            }

            return defaultValue;
        }

        public virtual void Set<T>(string key, T value)
        {
            m_KeyValueDict.Set(key, value);
        }


    }
}