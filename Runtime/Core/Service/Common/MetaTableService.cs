using Pangoo;

namespace Pangoo.Core.Services
{
    public class MetaTableService : BaseService
    {
        public override int Priority => -1;


        // public T GetExcelTable<T>() where T : ExcelTableBase
        // {
        //     return PangooEntry.ExcelTable.GetExcelTable<T>();
        // }


        // public InstructionTable GetInstructionTable()
        // {
        //     return GetExcelTable<InstructionTable>();
        // }

    }
}