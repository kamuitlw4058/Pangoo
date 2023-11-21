using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pangoo.Core.Services;
using Pangoo.Core.VisualScripting;
using UnityEngine;

namespace Pangoo.Core.Services
{
    [Serializable]
    public class RuntimeDataService : KeyValueService
    {
        VariablesTable m_VariablesTable;

        Dictionary<int, VariablesTable.VariablesRow> m_VariablesDict = new Dictionary<int, VariablesTable.VariablesRow>();
        protected override void DoStart()
        {
            base.DoStart();
            m_VariablesTable = m_ExcelTableService.GetExcelTable<VariablesTable>();
            foreach (var row in m_VariablesTable.Rows)
            {
                m_VariablesDict.Add(row.Id, row);
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

        public T GetVariable<T>(int id)
        {
            if (m_VariablesDict.TryGetValue(id, out VariablesTable.VariablesRow row))
            {
                T defaultValue = row.DefaultValue.ToType<T>();
                return Get<T>(row.Key, defaultValue);
            }

            return default(T);
        }

        public void SetVariable<T>(int id, T val)
        {
            if (m_VariablesDict.TryGetValue(id, out VariablesTable.VariablesRow row))
            {
                Set<T>(row.Key, val);
            }
        }



    }
}
