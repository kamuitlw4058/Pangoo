using MetaTable;
using Pangoo.MetaTable;

namespace Pangoo.Core.Services
{
    public class MetaTableService : MainSubService
    {
        public override int Priority => -2;


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


        MetaTable.DynamicObjectTable m_DynamicObjectTable;

        public MetaTable.DynamicObjectTable DynamicObjectTab
        {
            get
            {
                if (m_DynamicObjectTable == null)
                {
                    m_DynamicObjectTable = GetMetaTable<MetaTable.DynamicObjectTable>();
                }
                return m_DynamicObjectTable;
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

        MetaTable.VariablesTable m_VariablesTable;

        public MetaTable.VariablesTable VariablesTab
        {
            get
            {
                if (m_VariablesTable == null)
                {
                    m_VariablesTable = GetMetaTable<MetaTable.VariablesTable>();
                }
                return m_VariablesTable;
            }
        }


        MetaTable.SoundTable m_SoundTable;

        public MetaTable.SoundTable SoundTab
        {
            get
            {
                if (m_SoundTable == null)
                {
                    m_SoundTable = GetMetaTable<MetaTable.SoundTable>();
                }
                return m_SoundTable;
            }
        }


        MetaTable.DynamicObjectPreviewTable m_DynamicObjectPreviewTable;

        public MetaTable.DynamicObjectPreviewTable DynamicObjectPreviewTab
        {
            get
            {
                if (m_DynamicObjectPreviewTable == null)
                {
                    m_DynamicObjectPreviewTable = GetMetaTable<MetaTable.DynamicObjectPreviewTable>();
                }
                return m_DynamicObjectPreviewTable;
            }
        }


        MetaTable.HotspotTable m_HotspotTable;

        public MetaTable.HotspotTable HotspotTab
        {
            get
            {
                if (m_HotspotTable == null)
                {
                    m_HotspotTable = GetMetaTable<MetaTable.HotspotTable>();
                }
                return m_HotspotTable;
            }
        }


        MetaTable.DialogueTable m_DialogueTable;

        public MetaTable.DialogueTable DialogueTab
        {
            get
            {
                if (m_DialogueTable == null)
                {
                    m_DialogueTable = GetMetaTable<MetaTable.DialogueTable>();
                }
                return m_DialogueTable;
            }
        }

        MetaTable.ActorsLinesTable m_ActorsLinesTable;

        public MetaTable.ActorsLinesTable ActorsLinesTab
        {
            get
            {
                if (m_ActorsLinesTable == null)
                {
                    m_ActorsLinesTable = GetMetaTable<MetaTable.ActorsLinesTable>();
                }
                return m_ActorsLinesTable;
            }
        }

        MetaTable.CasesTable m_CasesTable;

        public MetaTable.CasesTable CasesTab
        {
            get
            {
                if (m_CasesTable == null)
                {
                    m_CasesTable = GetMetaTable<MetaTable.CasesTable>();
                }
                return m_CasesTable;
            }
        }

        MetaTable.ClueTable m_ClueTable;

        public MetaTable.ClueTable ClueTab
        {
            get
            {
                if (m_ClueTable == null)
                {
                    m_ClueTable = GetMetaTable<MetaTable.ClueTable>();
                }
                return m_ClueTable;
            }
        }

        MetaTable.CharacterConfigTable m_CharacterConfigTable;
        public MetaTable.CharacterConfigTable CharacterConfigTab
        {
            get
            {
                if (m_CharacterConfigTable == null)
                {
                    m_CharacterConfigTable = GetMetaTable<MetaTable.CharacterConfigTable>();
                }
                return m_CharacterConfigTable;
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

        public IVariablesRow GetVariablesByUuid(string uuid)
        {
            return VariablesTab.GetRowByUuid(uuid);
        }

        public ISoundRow GetSoundByUuid(string uuid)
        {
            return SoundTab.GetRowByUuid(uuid);
        }

        public IDynamicObjectPreviewRow GetDynamicObjectPreviewByUuid(string uuid)
        {
            return DynamicObjectPreviewTab.GetRowByUuid(uuid);
        }

        public IActorsLinesRow GetActorsLinesByUuid(string uuid)
        {
            return ActorsLinesTab.GetRowByUuid(uuid);
        }

        public IDialogueRow GetDialogueByUuid(string uuid)
        {
            return DialogueTab.GetRowByUuid(uuid);
        }

        public IHotspotRow GetHotspotByUuid(string uuid)
        {
            return HotspotTab.GetRowByUuid(uuid);
        }

        public ICasesRow GetCaseByUuid(string uuid)
        {
            return CasesTab.GetRowByUuid(uuid);
        }

        public IClueRow GetClueRowByUuid(string uuid)
        {
            return ClueTab.GetRowByUuid(uuid);
        }

        public IDynamicObjectRow GetDynamicObjectRow(string uuid)
        {
            return DynamicObjectTab.GetRowByUuid(uuid);
        }

        public ICharacterConfigRow GetCharacterConfigRow(string uuid)
        {
            return CharacterConfigTab.GetRowByUuid(uuid);
        }

    }
}