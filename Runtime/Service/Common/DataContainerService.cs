using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Pangoo.Service;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public class DataContainerService : ServiceBase,IKeyValue
    {
        public GlobalDataService globalDataService;
        public SaveLoadService saveLoadService;
        public RuntimeDataService runtimeDataService;

        public override void DoAwake(IServiceContainer services)
        {
            base.DoAwake(services);
            globalDataService = services.GetService<GlobalDataService>();
            saveLoadService = services.GetService<SaveLoadService>();
            runtimeDataService = services.GetService<RuntimeDataService>();

        }
        public T? Get<T>(string key)
        {
            object value = runtimeDataService.Get<T>(key);
            
            if (value==null)
            {
                value = saveLoadService.Get<T>(key);
                if (value==null)
                {
                    value = globalDataService.Get<T>(key);
                    Debug.LogError($"获取的value值0：{value}");
                    if (value==null)
                    {
                        Debug.LogError("默认配置表中没有这个键:"+key);
                        return default;
                    }
                }
                runtimeDataService.Set<T>(key,(T)value);
            }
            Debug.LogError($"获取的value值1：{value}");
            return (T)value;
        }

        public void Set<T>(string key, T value)
        {
            
        }
    }
}
