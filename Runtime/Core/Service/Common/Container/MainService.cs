using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pangoo.Core.Services
{
    public class MainSerice : NestedBaseService
    {
        public MainSerice()
        {
            AddService(new ExcelTableService());
            AddService(new StaticSceneService());
            AddService(new GameSectionService());
            AddService(new GlobalDataService());
            AddService(new SaveLoadService());
            AddService(new RuntimeDataService());
            AddService(new DataContainerService());
            AddService(new GameInfoService());
            AddService(new DynamicObjectManagerService());
            AddService(new CharacterService());
            AddService(new GameMainConfigService());
        }

    }
}