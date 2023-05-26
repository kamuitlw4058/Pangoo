using Pangoo;

namespace Pangoo.Service
{
    public class ExcelTableService : ServiceBase
    {
        public T GetExcelTable<T>() where T:ExcelTableBase
        {
            return  PangooEntry.ExcelTable.GetExcelTable<T>();
        }

    }
}