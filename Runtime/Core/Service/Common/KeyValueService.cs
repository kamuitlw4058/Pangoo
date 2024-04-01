using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    [Serializable]
    public class KeyValueService : MainSubService, IKeyValue
    {
        [ShowInInspector]
        [Searchable]
        protected Dictionary<string, object> m_KeyValueDict = new Dictionary<string, object>();

        protected override void DoAwake()
        {
            m_KeyValueDict = new Dictionary<string, object>();
        }
        public virtual T Get<T>(string key, T defaultValue)
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


        public virtual bool TryGet<T>(string key, out T outValue)
        {
            outValue = default;
            if (m_KeyValueDict.ContainsKey(key))
            {
                outValue = (T)m_KeyValueDict[key];
                return true;
            }
            return false;
        }

        // public virtual float? GetFloat(string key)
        // {
        //     return Get<float?>(key);
        // }

        public virtual void Set<T>(string key, T value)
        {
            m_KeyValueDict.Set(key, value);
        }

    }
}
