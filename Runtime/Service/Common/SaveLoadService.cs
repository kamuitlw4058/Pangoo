using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Pangoo.Service
{
    public class SaveLoadService : ServiceBase,IKeyValue
    {
        public Dictionary<string, object> SaveLoadDict;
        private SaveLoadTable m_SaveLoadTable;
        private ExcelTableService m_ExcelTableService;

        public override void DoAwake(IServiceContainer services)
        {
            SaveLoadDict = new Dictionary<string, object>();
            m_ExcelTableService = services.GetService<ExcelTableService>();
        }

        public override void DoStart()
        {
            m_SaveLoadTable = m_ExcelTableService.GetExcelTable<SaveLoadTable>();
            for (int i = 0; i < m_SaveLoadTable.Rows.Count; i++)
            {
                var key = m_SaveLoadTable.Rows[i].IntKey;
                var intValue = m_SaveLoadTable.Rows[i].IntValue;
                if (key != "" && intValue != null)
                {
                    SaveLoadDict.Add(key, intValue);
                }

                key = m_SaveLoadTable.Rows[i].FloatKey;
                var floatValue = m_SaveLoadTable.Rows[i].FloatValue;
                if (key != "" && floatValue != null)
                {
                    SaveLoadDict.Add(key, floatValue);
                }
            }
        }
        
        public T Get<T>(string key)
        {
            var value = SaveLoadDict[key];
            return (T)value;
        }
        public void Set<T>(string key, T value)
        {
            if (SaveLoadDict.ContainsKey(key))
            {
                SaveLoadDict[key] = value;
            }
            else
            {
                SaveLoadDict.Add(key,value);
            }
        }
    }
}
