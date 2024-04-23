using System;
using System.Collections.Generic;
using System.Linq;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using UnityEngine;
using Pangoo.Core.Characters;

namespace Pangoo.Core.Services
{

    public class SideEffectMainService : NestedBaseService
    {
        public RuntimeDataService RuntimeData;


        public GameMainConfigService GameConfig;


        public UIService UI;

        public SubtitleService Subtitle;

        public SoundService Sound;





        public MetaTableService MetaTable;


        public SaveLoadService SaveLoad;

        public MainMenuService MainMenu;


        public CursorService Cursor;



        public GameInfoService GameInfo;



        public SideEffectMainService()
        {
        }

        public void Init()
        {
            MetaTable = new MetaTableService();
            Sound = new SoundService();
            Subtitle = new SubtitleService();
            UI = new UIService();
            GameConfig = new GameMainConfigService();
            RuntimeData = new RuntimeDataService();
            SaveLoad = new SaveLoadService();
            MainMenu = new MainMenuService();

            Cursor = new CursorService();
            GameInfo = new GameInfoService();

            AddService(MetaTable, sortService: false);
            AddService(RuntimeData, sortService: false);
            AddService(GameInfo, sortService: false);
            AddService(GameConfig, sortService: false);
            AddService(Sound, sortService: false);
            AddService(UI, sortService: false);
            AddService(Subtitle, sortService: false);
            AddService(SaveLoad, sortService: false);
            AddService(MainMenu, sortService: false);
            AddService(Cursor, sortService: false);
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