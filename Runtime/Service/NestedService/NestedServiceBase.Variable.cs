using System;
using GameFramework;
using Sirenix.OdinInspector;
using System.Collections.Generic;


namespace Pangoo.Service
{
    public abstract partial class NestedServiceBase
    {
        Dictionary<string, object> m_VariableDict = new Dictionary<string, object>();

        public T GetVariable<T>(string key, T default_val = default(T))
        {
            object val = null;
            if (m_VariableDict.TryGetValue(key, out val))
            {
                return (T)val;
            }
            return default_val;
        }

        public void SetVariable<T>(string key, object val, bool overwrite = true)
        {

            if (!m_VariableDict.TryAdd(key, (T)val) && overwrite)
            {
                m_VariableDict[key] = (T)val;
            }
        }
    }
}