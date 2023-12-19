using Pangoo;
using Pangoo.MetaTable;

namespace Pangoo.Core.Services
{
    public class ExcelTableService : BaseService
    {
        public override int Priority => -1;

        GameSectionTable m_GameSectionTable;


        public T GetExcelTable<T>() where T : ExcelTableBase
        {
            return PangooEntry.ExcelTable.GetExcelTable<T>();
        }


        public InstructionTable GetInstructionTable()
        {
            return GetExcelTable<InstructionTable>();
        }


        // public IGameSectionRow GetGameSectionById(int id)
        // {
        //     if (m_GameSectionTable == null)
        //     {
        //         m_GameSectionTable = GetExcelTable<GameSectionTable>();
        //     }

        //     var row = m_GameSectionTable.GetGameSectionRow(id);


        //     // ExcelTableSrv.GetExcelTable<>
        // }

    }
}