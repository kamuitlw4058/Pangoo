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
        public RuntimeDataService RuntimeData;

        public CharacterService CharacterService;

        public GameMainConfigService GameConfig;


        public UIService UI;

        public SubtitleService Subtitle;

        public SoundService Sound;


        public StaticSceneService StaticScene;




        public MetaTableService MetaTable;

        public DynamicObjectService DynamicObject;

        public SaveLoadService SaveLoad;


        public MainService()
        {

        }

        public void Init()
        {
            MetaTable = new MetaTableService();
            DynamicObject = new DynamicObjectService();
            StaticScene = new StaticSceneService();
            Sound = new SoundService();
            Subtitle = new SubtitleService();
            UI = new UIService();
            GameConfig = new GameMainConfigService();
            CharacterService = new CharacterService();
            RuntimeData = new RuntimeDataService();
            SaveLoad = new SaveLoadService();


            AddService(MetaTable, sortService: false);
            AddService(StaticScene, sortService: false);
            AddService(new GameSectionService(), sortService: false);
            AddService(RuntimeData, sortService: false);
            AddService(new GameInfoService(), sortService: false);
            AddService(DynamicObject, sortService: false);
            AddService(CharacterService, sortService: false);
            AddService(GameConfig, sortService: false);
            AddService(Sound, sortService: false);
            AddService(UI, sortService: false);
            AddService(Subtitle, sortService: false);
            AddService(SaveLoad, sortService: false);
            SortService();
        }


        public DynamicObjectValue GetOrCreateDynamicObjectValue(string key, DynamicObject dynamicObject)
        {
            return RuntimeData.GetOrCreateDynamicObjectValue(key, dynamicObject);
        }


        public InstructionGetRowByUuidHandler GetInstructionRowByUuidHandler()
        {
            return MetaTable.GetInstructionByUuid;
        }




        public float DefaultInteractRadius
        {
            get
            {
                var radius = GameConfig.GetGameMainConfig()?.DefaultInteractRadius;
                if (radius == null || (radius != null && radius <= 0)) return float.MaxValue;
                return radius.Value;
            }
        }

        public float DefaultHotspotRadius
        {
            get
            {
                var radius = GameConfig.GetGameMainConfig()?.DefaultHotspotRadius;
                if (radius == null || (radius != null && radius <= 0)) return float.MaxValue;
                return radius.Value;
            }
        }

        public string DefaultPreviewInteraceVariableUuid
        {
            get
            {
                return GameConfig.GetGameMainConfig()?.DefaultPreviewIntVariable;
            }
        }


        public string DefaultPreviewExitVariableUuid
        {
            get
            {
                return GameConfig.GetGameMainConfig()?.DefaultPreviewExitVariable;
            }
        }

        public string DynamicObjectStateVariableUuid
        {
            get
            {
                return GameConfig.GetGameMainConfig()?.DynamicObjectStateVariableUuid;
            }
        }

    }
}