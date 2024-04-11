using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using Pangoo.Core.Services;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{
    public abstract class TargetValue
    {
        public virtual VariableTypeEnum TargetVariableType { get; }

        [JsonNoMember]
        public RuntimeDataService RuntimeDataSrv { get; set; }


        [ShowInInspector]
        [JsonMember("KeyValueDict")]
        public Dictionary<string, object> m_KeyValueDict = new Dictionary<string, object>();

        public Dictionary<string, object> KeyValueDict
        {
            get
            {
                return m_KeyValueDict;
            }
        }

        public virtual T Get<T>(string key, T defaultValue = default(T))
        {
            if (key.IsNullOrWhiteSpace())
            {
                return defaultValue;
            }

            object value = null;
            if (m_KeyValueDict.ContainsKey(key))
            {
                value = m_KeyValueDict[key];
                return (T)value;
            }
            else
            {
                m_KeyValueDict.Add(key, defaultValue);
            }

            return defaultValue;
        }

        public virtual void Set<T>(string key, T value)
        {
            if (key.IsNullOrWhiteSpace())
            {
                return;
            }

            m_KeyValueDict.Set(key, value);
        }


        public T GetVariable<T>(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace()) return default(T);

            IVariablesRow row = RuntimeDataSrv.GetVariablesRow(uuid);
            if (row != null)
            {
                var VariableType = row.VariableType.ToEnum<VariableTypeEnum>();
                if (VariableType != TargetVariableType)
                {
                    Debug.LogError($"Get Target Variable Type Wrong:{VariableType}, Target Type:{TargetVariableType},uuid:{uuid}");
                    return default(T);
                }

                T defaultValue = row.DefaultValue.ToType<T>();
                return Get<T>(row.Uuid, defaultValue);
            }

            return default(T);
        }

        public void SetVariable<T>(string uuid, T val)
        {
            if (uuid.IsNullOrWhiteSpace())
            {
                Debug.LogError($"Set Variable uuid Is null!");
                return;
            }

            IVariablesRow row = RuntimeDataSrv.GetVariablesRow(uuid);
            if (row != null)
            {
                var VariableType = row.VariableType.ToEnum<VariableTypeEnum>();
                if (VariableType != TargetVariableType)
                {
                    Debug.LogError($"Set Target Variable Type Wrong:{VariableType}, Target Type:{TargetVariableType},uuid:{uuid}, val:{val}");
                    return;
                }

                Set<T>(row.Uuid, val);
            }
        }


    }
}