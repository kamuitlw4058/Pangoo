using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo.Core.Services
{
    public class DataContainerService : BaseService, IKeyValue
    {
        public GlobalDataService globalDataService = new GlobalDataService();
        public SaveLoadService saveLoadService = new SaveLoadService();
        public RuntimeDataService runtimeDataService = new RuntimeDataService();

        protected override void DoAwake()
        {
            base.DoAwake();
            globalDataService = Parent.GetService<GlobalDataService>();
            saveLoadService = Parent.GetService<SaveLoadService>();
            runtimeDataService = Parent.GetService<RuntimeDataService>();
        }

        public bool TryGet<T>(string key, out T outValue)
        {
            if (!runtimeDataService.TryGet<T>(key, out outValue))
            {
                if (!saveLoadService.TryGet<T>(key, out outValue))
                {
                    if (!globalDataService.TryGet<T>(key, out outValue))
                    {
                        Debug.LogError("默认配置表中没有这个键:" + key);
                        return false;
                    }
                }
                runtimeDataService.Set<T>(key, (T)outValue);
            }
            return true;
        }

        public void Set<T>(string key, T value)
        {
            runtimeDataService.Set<T>(key, value);
            //TODO:添加条件约束添加SaveLoad数据的节点
            //saveLoadService.Set<T>(key,value);
        }
    }
}
