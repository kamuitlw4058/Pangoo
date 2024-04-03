using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using Pangoo.Core.Services;
using Pangoo.MetaTable;

namespace Pangoo.Core.Common
{
    [Serializable]
    public class DynamicObjectValue
    {
        [JsonNoMember]
        public RuntimeDataService RuntimeDataSrv { get; set; }

        [JsonNoMember]
        public DynamicObject dynamicObejct { get; set; }

        [JsonNoMember]
        public TransformValue? transformValue { get; set; }

        [JsonNoMember]
        public Dictionary<string, TransformValue> ChilernTransforms = new Dictionary<string, TransformValue>();

        [JsonNoMember]
        public Dictionary<string, bool> TriggerEnabledDict = new Dictionary<string, bool>();

        [JsonNoMember]
        public Dictionary<string, int> TriggerIndexDict = new();

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


        public void SetChilernTransforms(string key, TransformValue val)
        {
            if (key.IsNullOrWhiteSpace()) return;

            ChilernTransforms.Set(key, val);
        }

        public void SetTriggerEnabled(string key, bool val)
        {
            if (key.IsNullOrWhiteSpace()) return;

            TriggerEnabledDict.Set(key, val);
        }

        public void SetTriggerIndex(string key, int val)
        {
            TriggerIndexDict.Set(key, val);
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
                // Debug.LogError($"获取的value值0：{value}");
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
            m_KeyValueDict.Set(key, value);
        }


        public T GetVariable<T>(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace()) return default(T);

            IVariablesRow row = RuntimeDataSrv.GetVariablesRow(uuid);
            if (row != null)
            {
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
                Set<T>(row.Uuid, val);
            }
        }


    }
}