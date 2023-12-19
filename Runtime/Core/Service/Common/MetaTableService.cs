using MetaTable;
using Pangoo.MetaTable;

namespace Pangoo.Core.Services
{
    public class MetaTableService : BaseService
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

    }
}