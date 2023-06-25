using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pangoo.Service;
using UnityEngine;

namespace Pangoo.Service
{
    public class RuntimeDataService : ServiceBase,IKeyValue
    {
        public Dictionary<string, object> RuntimeDataDict;
        private RuntimeDataTable m_RuntimeDataTable;
        private ExcelTableService m_ExcelTableService;

        public override void DoAwake(IServiceContainer services)
        {
            RuntimeDataDict = new Dictionary<string, object>();
            m_ExcelTableService = services.GetService<ExcelTableService>();
        }
        [CanBeNull]
        public T Get<T>(string key)
        {
            var value = RuntimeDataDict[key];
            return (T)value;
        }

        public void Set<T>(string key, T value)
        {
            if (RuntimeDataDict.ContainsKey(key))
            {
                RuntimeDataDict[key] = value;
            }
            else
            {
                RuntimeDataDict.Add(key,value);
            }
        }
        
    }
}
