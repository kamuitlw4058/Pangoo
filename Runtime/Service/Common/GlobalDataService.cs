using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Service
{
    public class GlobalDataService : KeyValueService
    {
        [ShowInInspector]
        private GlobalDataTable m_GlobalDataTable;

        protected override void DoStart()
        {
            m_GlobalDataTable = m_ExcelTableService.GetExcelTable<GlobalDataTable>();
            for (int i = 0; i < m_GlobalDataTable.Rows.Count; i++)
            {
                var valueStr = m_GlobalDataTable.Rows[i].Value;
                var typeStr = m_GlobalDataTable.Rows[i].Type;

                var value = StringConvert.ToValue(typeStr, valueStr);
                m_KeyValueDict.Add(m_GlobalDataTable.Rows[i].Key, value);
            }
        }
    }
}
