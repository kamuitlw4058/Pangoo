using System;
using System.Collections.Generic;
using System.Linq;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using UnityEngine;
using Pangoo.Core.Characters;

namespace Pangoo.Core.Services
{

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

        public MainMenuService MainMenu;

        public DialogueService Dialogue;

        public CursorService Cursor;

        public CaseService Case;

        public GameSectionService GameSection;

        public GameInfoService GameInfo;

        public NewDynamicObjectService NewDynamicObject;




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
            MainMenu = new MainMenuService();

            Dialogue = new DialogueService();
            Cursor = new CursorService();
            Case = new CaseService();
            GameSection = new GameSectionService();
            GameInfo = new GameInfoService();
            NewDynamicObject = new NewDynamicObjectService();

            AddService(MetaTable, sortService: false);
            AddService(StaticScene, sortService: false);
            AddService(GameSection, sortService: false);
            AddService(RuntimeData, sortService: false);
            AddService(GameInfo, sortService: false);
            AddService(DynamicObject, sortService: false);
            AddService(CharacterService, sortService: false);
            AddService(GameConfig, sortService: false);
            AddService(Sound, sortService: false);
            AddService(UI, sortService: false);
            AddService(Subtitle, sortService: false);
            AddService(SaveLoad, sortService: false);
            AddService(MainMenu, sortService: false);
            AddService(Dialogue, sortService: false);
            AddService(Cursor, sortService: false);
            AddService(Case, sortService: false);
            AddService(NewDynamicObject, sortService: false);
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