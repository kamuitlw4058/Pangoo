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


        public DynamicObjectValue GetDynamicObjectValue(string key)
        {
            return RuntimeData.Get<DynamicObjectValue>(key);
        }

        public void SetTargetTransformValue(DynamicObjectValue dynamicObjectValue, string target, TransformValue transformValue)
        {
            if (target == null)
            {
                return;
            }

            if (target == string.Empty || target == "Self")
            {
                dynamicObjectValue.transformValue = transformValue;
            }
            else
            {
                Debug.Log($"Set Child:{transformValue}");
                dynamicObjectValue.SetChilernTransforms(target, transformValue);
            }
        }


        public void SetDynamicObjectValue(DynamicObject dynamicObejct, string target, TransformValue transformValue)
        {
            var val = RuntimeData.Get<DynamicObjectValue>(dynamicObejct.RuntimeKey);
            if (val == null)
            {
                val = new DynamicObjectValue();
                val.dynamicObejct = dynamicObejct;
                val.transformValue = null;

                SetTargetTransformValue(val, target, transformValue);
                RuntimeData.Set<DynamicObjectValue>(dynamicObejct.RuntimeKey, val);
            }
            else
            {
                SetTargetTransformValue(val, target, transformValue);
            }
        }

    }
}