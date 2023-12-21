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


        MetaTable.GameSectionTable m_GameSectionTable;

        public MetaTable.GameSectionTable GameSectionTab
        {
            get
            {
                if (m_GameSectionTable == null)
                {
                    m_GameSectionTable = GetMetaTable<MetaTable.GameSectionTable>();
                }
                return m_GameSectionTable;
            }
        }

        MetaTable.InstructionTable m_InstructionTable;

        public MetaTable.InstructionTable InstructionTab
        {
            get
            {
                if (m_InstructionTable == null)
                {
                    m_InstructionTable = GetMetaTable<MetaTable.InstructionTable>();
                }
                return m_InstructionTable;
            }
        }

        MetaTable.ConditionTable m_ConditionTable;

        public MetaTable.ConditionTable ConditionTab
        {
            get
            {
                if (m_ConditionTable == null)
                {
                    m_ConditionTable = GetMetaTable<MetaTable.ConditionTable>();
                }
                return m_ConditionTable;
            }
        }



        MetaTable.TriggerEventTable m_TriggerEventTable;

        public MetaTable.TriggerEventTable TriggerEventTab
        {
            get
            {
                if (m_TriggerEventTable == null)
                {
                    m_TriggerEventTable = GetMetaTable<MetaTable.TriggerEventTable>();
                }
                return m_TriggerEventTable;
            }
        }


        public IGameSectionRow GetGameSectionByUuid(string uuid)
        {
            return GameSectionTab?.GetRowByUuid(uuid);
        }



        public IInstructionRow GetInstructionByUuid(string uuid)
        {

            return InstructionTab.GetRowByUuid(uuid);
        }


        public IConditionRow GetConditionByUuid(string uuid)
        {

            return ConditionTab.GetRowByUuid(uuid);
        }

        public ITriggerEventRow GetTriggerEventByUuid(string uuid)
        {

            return TriggerEventTab.GetRowByUuid(uuid);
        }

    }
}