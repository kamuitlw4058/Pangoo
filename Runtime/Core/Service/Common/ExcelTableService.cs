using Pangoo;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Services
{
    public class ExcelTableService : BaseService
    {
        public override int Priority => -1;

        GameSectionTable m_GameSectionTable;

        public GameSectionTable GameSectionTab
        {
            get
            {
                if (m_GameSectionTable == null)
                {
                    m_GameSectionTable = GetExcelTable<GameSectionTable>();
                }
                return m_GameSectionTable;
            }
        }

        InstructionTable m_InstructionTable;

        public InstructionTable InstructionTab
        {
            get
            {
                if (m_InstructionTable == null)
                {
                    m_InstructionTable = GetExcelTable<InstructionTable>();
                }
                return m_InstructionTable;
            }
        }


        public T GetExcelTable<T>() where T : ExcelTableBase
        {
            return PangooEntry.ExcelTable.GetExcelTable<T>();
        }


        public InstructionTable GetInstructionTable()
        {
            return GetExcelTable<InstructionTable>();
        }


        public IGameSectionRow GetGameSectionById(int id)
        {

            var row = GameSectionTab?.GetGameSectionRow(id);
            if (row != null)
            {
                var json = LitJson.JsonMapper.ToJson(row);
                var metaRow = LitJson.JsonMapper.ToObject<Pangoo.MetaTable.GameSectionRow>(json);
                return metaRow;
            }

            return null;
        }

        public IInstructionRow GetInstructionById(int id)
        {

            var row = InstructionTab?.GetRowById(id);
            if (row != null)
            {
                var json = LitJson.JsonMapper.ToJson(row);
                var metaRow = LitJson.JsonMapper.ToObject<Pangoo.MetaTable.InstructionRow>(json);
                return metaRow;
            }

            return null;
        }


    }
}