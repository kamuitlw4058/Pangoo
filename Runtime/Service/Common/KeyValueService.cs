using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Pangoo.Service;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public class KeyValueService : ServiceBase,IKeyValue
    {
        [ShowInInspector]
        protected Dictionary<string, object> m_KeyValueDict;
        protected ExcelTableService m_ExcelTableService;

        public override void DoAwake(IServiceContainer services)
        {
            m_KeyValueDict  = new Dictionary<string, object>();
            m_ExcelTableService = services.GetService<ExcelTableService>();
        }
        [CanBeNull]
        public virtual T Get<T>(string key)
        {
            object value = null;
            if (m_KeyValueDict.ContainsKey(key))
            {
                value = m_KeyValueDict[key];
                Debug.LogError($"获取的value值0：{value}");
                return (T)value;
            }
            return (T)value;
        }

        public virtual void Set<T>(string key, T value)
        {
            if (m_KeyValueDict.ContainsKey(key))
            {
                m_KeyValueDict[key] = value;
            }
            else
            {
                m_KeyValueDict.Add(key,value);
            }
        }

    }
}
