using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pangoo.Service
{
    public class GlobalDataService : ServiceBase,IKeyValue
    {
        public Dictionary<string, object> GlobalDataDict;
        private GlobalDataTable m_GlobalDataTable;
        private ExcelTableService m_ExcelTableService;

        public override void DoAwake(IServiceContainer services)
        {
            GlobalDataDict = new Dictionary<string, object>();
            m_ExcelTableService = services.GetService<ExcelTableService>();
        }

        public override void DoStart()
        {
            m_GlobalDataTable = m_ExcelTableService.GetExcelTable<GlobalDataTable>();
            for (int i = 0; i < m_GlobalDataTable.Rows.Count; i++)
            {
                var valueStr = m_GlobalDataTable.Rows[i].Value;
                var typeStr = m_GlobalDataTable.Rows[i].Type;
                
                var value = StringConvert.ToValue(typeStr, valueStr);
                GlobalDataDict.Add(m_GlobalDataTable.Rows[i].Key,value);
                Debug.Log("查看字典"+GlobalDataDict[m_GlobalDataTable.Rows[i].Key].GetType());
            }
        }

        public T Get<T>(string key)
        {
            var value = GlobalDataDict[key];
            return (T)value;
        }
        public void Set<T>(string key, T value)
        {
            if (GlobalDataDict.ContainsKey(key))
            {
                GlobalDataDict[key] = value;
            }
            else
            {
                GlobalDataDict.Add(key,value);
            }
        }
    }
}
