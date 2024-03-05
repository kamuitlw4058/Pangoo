using System;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
using UnityEngine;
using LitJson;
using Sirenix.OdinInspector;


namespace Pangoo.Core.Services
{
    [Serializable]
    public class RuntimeDataService : KeyValueService
    {
        public override int Priority => -1;
        public Dictionary<string, object> KeyValues
        {
            get
            {
                return m_KeyValueDict;
            }
        }

        public Dictionary<string, DynamicObjectValue> DynamicObjectValueDict
        {
            get
            {
                return m_DynamicObjectValueDict;
            }
        }

        Pangoo.MetaTable.VariablesTable m_VariablesTable;

        [ShowInInspector]
        public Dictionary<string, IVariablesRow> m_VariablesDict = new Dictionary<string, IVariablesRow>();

        [ShowInInspector]
        Dictionary<string, DynamicObjectValue> m_DynamicObjectValueDict = new Dictionary<string, DynamicObjectValue>();
        protected override void DoStart()
        {
            base.DoStart();
            m_VariablesTable = MetaTableSrv.GetMetaTable<Pangoo.MetaTable.VariablesTable>();
            foreach (var row in m_VariablesTable.RowDict.Values)
            {
                if (row.VariableType.IsNullOrWhiteSpace() || row.VariableType.Equals(VariableTypeEnum.DynamicObject.ToString())) continue;

                m_VariablesDict.Add(row.Uuid, row);

                switch (row.ValueType.ToEnum<VariableValueTypeEnum>())
                {
                    case VariableValueTypeEnum.String:
                        Set<string>(row.Key, row.DefaultValue);
                        break;
                    case VariableValueTypeEnum.Float:
                        Set<float>(row.Key, row.DefaultValue.ToFloatForce());
                        break;
                    case VariableValueTypeEnum.Bool:
                        Set<bool>(row.Key, row.DefaultValue.ToBoolForce());
                        break;
                    case VariableValueTypeEnum.Int:
                        Set<int>(row.Key, row.DefaultValue.ToIntForce());
                        break;
                }
            }

        }
        public DynamicObjectValue GetDynamicObjectValue(string key)
        {
            if (m_DynamicObjectValueDict.ContainsKey(key))
            {
                return m_DynamicObjectValueDict[key];
            }

            return null;
        }


        public DynamicObjectValue GetOrCreateDynamicObjectValue(string key, DynamicObject dynamicObject)
        {
            DynamicObjectValue val = null;
            if (m_DynamicObjectValueDict.ContainsKey(key))
            {
                return m_DynamicObjectValueDict[key];
            }
            val = new DynamicObjectValue();
            val.dynamicObejct = dynamicObject;
            m_DynamicObjectValueDict.Add(key, val);

            return val;
        }

        public object GetVariable(string uuid)
        {
            if (m_VariablesDict.TryGetValue(uuid, out IVariablesRow row))
            {
                if (m_KeyValueDict.ContainsKey(row.Key))
                {
                    return m_KeyValueDict[row.Key];
                }
            }

            return null;

        }

        public T GetVariable<T>(string uuid)
        {
            if (m_VariablesDict.TryGetValue(uuid, out IVariablesRow row))
            {
                T defaultValue = row.DefaultValue.ToType<T>();
                return Get<T>(row.Key, defaultValue);
            }

            return default(T);
        }

        public void SetVariable<T>(string uuid, T val)
        {
            if (uuid.IsNullOrWhiteSpace())
            {
                LogError($"Set Variable uuid Is null!");
                return;
            }

            if (m_VariablesDict.TryGetValue(uuid, out IVariablesRow row))
            {
                Set<T>(row.Key, val);
            }
        }

        public VariableTypeEnum? GetVariableType(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace())
            {
                Debug.LogError($"GetVariableType uuid is Null");
                return null;
            }


            if (m_VariablesDict.TryGetValue(uuid, out IVariablesRow row))
            {
                return row.VariableType.ToEnum<VariableTypeEnum>();
            }

            return null;
        }

        [Serializable]
        public class RuntimeDataClass
        {
            [JsonMember("KeyValueDict")]
            public string KeyValueDict;


            [JsonMember("DynamicObjectValueDict")]
            public string DynamicObjectValueDict;
        }


        public override string SerializeToString()
        {
            var data = new RuntimeDataClass();
            data.DynamicObjectValueDict = JsonMapper.ToJson(m_DynamicObjectValueDict);
            var saveKeyValues = new Dictionary<string, object>();
            foreach (var kv in m_VariablesDict)
            {
                var val = GetVariable(kv.Key);
                var row = kv.Value;
                if (!row.NotSave)
                {
                    saveKeyValues.Add(row.Key, val);
                }
            }

            data.KeyValueDict = JsonMapper.ToJson(saveKeyValues);
            return JsonMapper.ToJson(data);
        }

        public override void Deserialize(string data)
        {
            var dataClass = JsonMapper.ToObject<RuntimeDataClass>(data);
            KeyValues.Clear();
            JsonMapper.ToObject<Dictionary<string, object>>(dataClass.KeyValueDict, KeyValues);
            m_DynamicObjectValueDict.Clear();
            JsonMapper.ToObject<Dictionary<string, DynamicObjectValue>>(dataClass.DynamicObjectValueDict, m_DynamicObjectValueDict);
        }

    }
}
