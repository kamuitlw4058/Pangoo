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
        [Searchable]
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
        [Searchable]
        public Dictionary<string, IVariablesRow> m_VariablesDict = new Dictionary<string, IVariablesRow>();

        [ShowInInspector]
        Dictionary<string, DynamicObjectValue> m_DynamicObjectValueDict = new Dictionary<string, DynamicObjectValue>();
        protected override void DoStart()
        {
            base.DoStart();
            m_VariablesTable = MetaTableSrv.GetMetaTable<Pangoo.MetaTable.VariablesTable>();
            foreach (var row in m_VariablesTable.RowDict.Values)
            {
                if (row.VariableType.IsNullOrWhiteSpace()) continue;

                m_VariablesDict.Add(row.Uuid, row);

                if (row.VariableType.Equals(VariableTypeEnum.DynamicObject.ToString())) continue;

                switch (row.ValueType.ToEnum<VariableValueTypeEnum>())
                {
                    case VariableValueTypeEnum.String:
                        Set<string>(row.Uuid, row.DefaultValue);
                        break;
                    case VariableValueTypeEnum.Float:
                        Set<float>(row.Uuid, row.DefaultValue.ToFloatForce());
                        break;
                    case VariableValueTypeEnum.Bool:
                        Set<bool>(row.Uuid, row.DefaultValue.ToBoolForce());
                        break;
                    case VariableValueTypeEnum.Int:
                        Set<int>(row.Uuid, row.DefaultValue.ToIntForce());
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


        public DynamicObjectValue GetOrCreateDynamicObjectValue(string dynamicObjectUuid, DynamicObject dynamicObject)
        {
            DynamicObjectValue val = null;
            if (m_DynamicObjectValueDict.ContainsKey(dynamicObjectUuid))
            {
                val = m_DynamicObjectValueDict[dynamicObjectUuid];
                if (dynamicObject != null)
                {
                    val.dynamicObejct = dynamicObject;
                }
                return val;
            }
            val = new DynamicObjectValue();
            val.dynamicObejct = dynamicObject;
            val.RuntimeDataSrv = this;
            m_DynamicObjectValueDict.Add(dynamicObjectUuid, val);

            return val;
        }

        public IVariablesRow GetVariablesRow(string uuid)
        {
            return MetaTableSrv.GetVariablesByUuid(uuid);
        }

        public object GetVariable(string uuid)
        {

            if (m_KeyValueDict.ContainsKey(uuid))
            {
                return m_KeyValueDict[uuid];
            }

            return null;
        }



        public T GetVariable<T>(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace()) return default(T);

            if (m_VariablesDict.TryGetValue(uuid, out IVariablesRow row))
            {
                T defaultValue = row.DefaultValue.ToType<T>();
                return Get<T>(uuid, defaultValue);
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
                Set<T>(uuid, val);
            }
        }

        public T GetDynamicObjectVariable<T>(string dynamicObejctUuid, string variableUuid)
        {
            if (dynamicObejctUuid.IsNullOrWhiteSpace() || variableUuid.IsNullOrWhiteSpace()) return default(T);

            var dynamicObejctValue = GetOrCreateDynamicObjectValue(dynamicObejctUuid, null);
            return dynamicObejctValue.GetVariable<T>(variableUuid);
        }



        public void SetDynamicObjectVariable<T>(string dynamicObejctUuid, string variableUuid, T val)
        {
            if (dynamicObejctUuid.IsNullOrWhiteSpace() || variableUuid.IsNullOrWhiteSpace()) return;

            var dynamicObejctValue = GetOrCreateDynamicObjectValue(dynamicObejctUuid, null);
            dynamicObejctValue.SetVariable<T>(variableUuid, val);
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
                    saveKeyValues.Add(row.Uuid, val);
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
