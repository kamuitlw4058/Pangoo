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
        public RuntimeDataService RuntimeData = new RuntimeDataService();

        public CharacterService CharacterService = new CharacterService();

        public GameMainConfigService GameConfig = new GameMainConfigService();


        public UIService UI = new UIService();

        public SubtitleService Subtitle = new SubtitleService();

        public SoundService Sound = new SoundService();


        public StaticSceneService StaticScene = new StaticSceneService();


        public ExcelTableService ExcelTable = new ExcelTableService();


        public MetaTableService MetaTable = new MetaTableService();

        public DynamicObjectService DynamicObject = new DynamicObjectService();


        public MainService()
        {
            AddService(ExcelTable);
            AddService(MetaTable);
            AddService(StaticScene);
            AddService(new GameSectionService());
            AddService(RuntimeData);
            AddService(new GameInfoService());
            AddService(DynamicObject);
            AddService(CharacterService);
            AddService(GameConfig);
            AddService(Sound);
            AddService(UI);
            AddService(Subtitle);
        }


        public DynamicObjectValue GetOrCreateDynamicObjectValue(string key, DynamicObject dynamicObject)
        {
            var val = RuntimeData.Get<DynamicObjectValue>(key, null);
            if (val == null)
            {
                val = new DynamicObjectValue();
                val.dynamicObejct = dynamicObject;
                RuntimeData.Set<DynamicObjectValue>(key, val);
            }
            return val;
        }

        public InstructionGetRowByIdHandler GetInstructionRowByIdHandler()
        {
            return ExcelTable.GetInstructionById;
        }

        public InstructionGetRowByUuidHandler GetInstructionRowByUuidHandler()
        {
            return MetaTable.GetInstructionByUuid;
        }

        public T GetExcelTable<T>() where T : ExcelTableBase
        {
            return ExcelTable.GetExcelTable<T>();
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

    }
}