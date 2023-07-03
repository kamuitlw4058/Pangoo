using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

        public bool TryGet<T>(string key, out T outValue)
        {
            if (!runtimeDataService.TryGet<T>(key,out outValue))
            {
                if (!saveLoadService.TryGet<T>(key,out outValue))
                {
                    if (!globalDataService.TryGet<T>(key,out outValue))
                    {
                        Debug.LogError("默认配置表中没有这个键:"+key);
                        return false;
                    }
                }
                runtimeDataService.Set<T>(key,(T)outValue);
            }
            return true;
        }

        public void Set<T>(string key, T value)
        {
            runtimeDataService.Set<T>(key,value);
            //TODO:添加条件约束添加SaveLoad数据的节点
            //saveLoadService.Set<T>(key,value);
        }
    }
}
