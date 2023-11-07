using System;
using System.Collections.Generic;
using System.Linq;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using UnityEngine;
using Pangoo.Core.Characters;

namespace Pangoo.Core.Services
{
    [Serializable]
    public class MainService : NestedBaseService
    {
        RuntimeDataService RuntimeData = new RuntimeDataService();

        public CharacterService CharacterService = new CharacterService();

        public GameMainConfigService GameConfig = new GameMainConfigService();


        public UIService UI = new UIService();

        public SubtitleService Subtitle = new SubtitleService();

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
            AddService(CharacterService);
            AddService(GameConfig);
            AddService(new SoundService());
            AddService(UI);
            AddService(Subtitle);
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