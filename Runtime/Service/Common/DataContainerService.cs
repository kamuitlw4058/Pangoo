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
            globalDataService = services.GetService<GlobalDataService>();
            saveLoadService = services.GetService<SaveLoadService>();
            runtimeDataService = services.GetService<RuntimeDataService>();
            base.DoAwake(services);
        }
        [CanBeNull]
        public T Get<T>(string key)
        {
            T value = runtimeDataService.Get<T>(key);
            if (value==null)
            {
                value = saveLoadService.Get<T>(key);
                if (value==null)
                {
                    value = globalDataService.Get<T>(key);
                    if (value==null)
                    {
                        Debug.LogError("默认配置表中没有这个键:"+key);
                        return value;
                    }
                }
        
                runtimeDataService.Set<T>(key,value);
            }
            return value;
        }

        public void Set<T>(string key, T value)
        {
            
        }
    }
}
