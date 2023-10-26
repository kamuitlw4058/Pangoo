using System;
using System.Collections.Generic;
using System.Linq;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using UnityEngine;

namespace Pangoo.Core.Services
{
    public class MainService : NestedBaseService
    {
        RuntimeDataService RuntimeData = new RuntimeDataService();

        public MainService()
        {
            AddService(new ExcelTableService());
            AddService(new StaticSceneService());
            AddService(new GameSectionService());
            AddService(new GlobalDataService());
            AddService(new SaveLoadService());
            AddService(RuntimeData);
            AddService(new DataContainerService());
            AddService(new GameInfoService());
            AddService(new DynamicObjectService());
            AddService(new CharacterService());
            AddService(new GameMainConfigService());
        }


        public DynamicObjectValue GetOrCreateDynamicObjectValue(string key, DynamicObject dynamicObject)
        {
            var val = RuntimeData.Get<DynamicObjectValue>(key);
            if (val == null)
            {
                val = new DynamicObjectValue();
                val.dynamicObejct = dynamicObject;
                RuntimeData.Set<DynamicObjectValue>(key, val);
            }
            return val;
        }

    }
}