using MetaTable;
using OfficeOpenXml.Table;
using Pangoo.MetaTable;

namespace Pangoo.Core.Services
{
    public class MetaTableService : MainSubService
    {
        public override int Priority => -1;


        public T GetMetaTable<T>() where T : MetaTableBase
        {
            return PangooEntry.MetaTable.GetMetaTable<T>();
        }


        public Pangoo.MetaTable.InstructionTable GetInstructionTable()
        {
            return GetMetaTable<Pangoo.MetaTable.InstructionTable>();
        }

        // public IGameSectionRow GetGameSectionById(int id)
        // {
        //     // ExcelTableSrv.GetExcelTable<>
        // }

    }
}