using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pangoo.Core.Services;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
using UnityEngine;

namespace Pangoo.Core.Services
{
    [Serializable]
    public class RuntimeDataService : KeyValueService
    {
        public Dictionary<string, object> KeyValues
        {
            get
            {
                return m_KeyValueDict;
            }
        }

        Pangoo.MetaTable.VariablesTable m_VariablesTable;

        Dictionary<string, IVariablesRow> m_VariablesDict = new Dictionary<string, IVariablesRow>();
        protected override void DoStart()
        {
            base.DoStart();
            m_VariablesTable = MetaTableSrv.GetMetaTable<Pangoo.MetaTable.VariablesTable>();
            foreach (var row in m_VariablesTable.RowDict.Values)
            {
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
                }
            }

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

    }
}
