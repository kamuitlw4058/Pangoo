using Pangoo;

namespace Pangoo.Core.Services
{
    public class MainSubService : BaseService
    {

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




        protected override void DoAwake()
        {
            base.DoAwake();


        }


    }
}