using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pangoo.Service;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public class KeyValueService : ServiceBase,IKeyValue
    {
        [ShowInInspector]
        protected Dictionary<string, object> m_KeyValueDict;
        protected ExcelTableService m_ExcelTableService;

        public override void DoAwake(IServiceContainer services)
        {
            m_KeyValueDict  = new Dictionary<string, object>();
            m_ExcelTableService = services.GetService<ExcelTableService>();
        }
        public virtual T Get<T>(string key)
        {
            object value = null;
            if (m_KeyValueDict.ContainsKey(key))
            {
                value = m_KeyValueDict[key];
                Debug.LogError($"获取的value值0：{value}");
                return (T)value;
            }

            return (T)value;
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

        public virtual float?  GetFloat(string key)
        {
            return Get<float?>(key);
        }

        public virtual void Set<T>(string key, T value)
        {
            if (m_KeyValueDict.ContainsKey(key))
            {
                m_KeyValueDict[key] = value;
            }
            else
            {
                m_KeyValueDict.Add(key,value);
            }
        }

    }
}
