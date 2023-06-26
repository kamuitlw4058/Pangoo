using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Pangoo.Service
{
    public class SaveLoadService : KeyValueService
    {
        private SaveLoadTable m_SaveLoadTable;


        public override void DoStart()
        {
            m_SaveLoadTable = m_ExcelTableService.GetExcelTable<SaveLoadTable>();
            for (int i = 0; i < m_SaveLoadTable.Rows.Count; i++)
            {
                var key = m_SaveLoadTable.Rows[i].IntKey;
                var intValue = m_SaveLoadTable.Rows[i].IntValue;
                if (key != "" && intValue != null)
                {
                    m_KeyValueDict.Add(key, intValue);
                }

                key = m_SaveLoadTable.Rows[i].FloatKey;
                var floatValue = m_SaveLoadTable.Rows[i].FloatValue;
                if (key != "")
                {
                    m_KeyValueDict.Add(key, floatValue);
                }
            }
        }
    }
}
