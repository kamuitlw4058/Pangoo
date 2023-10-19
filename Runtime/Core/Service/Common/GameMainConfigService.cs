using Pangoo;

namespace Pangoo.Core.Services
{
    public class GameMainConfigService : BaseService
    {
        public override int Priority => -1;


        public T GetExcelTable<T>() where T : ExcelTableBase
        {
            return PangooEntry.ExcelTable.GetExcelTable<T>();
        }

        public GameMainConfig GetGameMainConfig()
        {
            return PangooEntry.GameConfig.GetGameMainConfig();
        }

    }
}