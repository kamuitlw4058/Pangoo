using Pangoo;

namespace Pangoo.Core.Services
{
    public class MainSubService : BaseService
    {

        CharacterService m_CharacterService;

        public CharacterService CharacterSrv
        {
            get
            {
                if (m_CharacterService == null)
                {
                    m_CharacterService = Parent.GetService<CharacterService>();
                }
                return m_CharacterService;
            }
        }

        StaticSceneService m_StaticSceneService;

        public StaticSceneService StaticSceneSrv
        {
            get
            {
                if (m_StaticSceneService == null)
                {
                    m_StaticSceneService = Parent.GetService<StaticSceneService>();
                }
                return m_StaticSceneService;
            }
        }

        DynamicObjectService m_DynamicObjectService;


        public DynamicObjectService DynamicObjectSrv
        {
            get
            {
                if (m_DynamicObjectService == null)
                {
                    m_DynamicObjectService = Parent.GetService<DynamicObjectService>();
                }
                return m_DynamicObjectService;
            }
        }

        ExcelTableService m_ExcelTableService;
        public ExcelTableService ExcelTableSrv
        {
            get
            {
                if (m_ExcelTableService == null)
                {
                    m_ExcelTableService = Parent.GetService<ExcelTableService>();
                }
                return m_ExcelTableService;
            }
        }


        GameMainConfigService m_GameMainConfigService;

        public GameMainConfigService GameMainConfigSrv
        {
            get
            {
                if (m_GameMainConfigService == null)
                {
                    m_GameMainConfigService = Parent.GetService<GameMainConfigService>();
                }
                return m_GameMainConfigService;
            }
        }


        MetaTableService m_MetaTableService;
        public MetaTableService MetaTableSrv
        {
            get
            {
                if (m_MetaTableService == null)
                {
                    m_MetaTableService = Parent.GetService<MetaTableService>();
                }
                return m_MetaTableService;
            }
        }


        GameInfoService m_GameInfoService;


        public GameInfoService GameInfoSrv
        {
            get
            {
                if (m_GameInfoService == null)
                {
                    m_GameInfoService = Parent.GetService<GameInfoService>();
                }
                return m_GameInfoService;
            }
        }


        RuntimeDataService m_RuntimeDataService;


        public RuntimeDataService RuntimeDataSrv
        {
            get
            {
                if (m_RuntimeDataService == null)
                {
                    m_RuntimeDataService = Parent.GetService<RuntimeDataService>();
                }
                return m_RuntimeDataService;
            }
        }



        protected override void DoAwake()
        {
            base.DoAwake();


        }


    }
}