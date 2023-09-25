#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEditor;

namespace Pangoo.Editor
{

    [Serializable]
    public partial class TriggerEventWrapper : BaseNamedWrapper
    {

        public override string Title
        {
            get
            {
                if (m_DetailRow == null)
                {
                    return string.Empty;
                }
                return $"{m_DetailRow.TriggerType}".Humanize();
            }
        }

        [ShowInInspector]
        [FoldoutGroup("$Title")]
        public TriggerTypeEnum TriggerType
        {
            get
            {
                if (m_DetailRow == null)
                {
                    return TriggerTypeEnum.Unknown;
                }

                if (m_DetailRow.TriggerType == null)
                {
                    m_DetailRow.TriggerType = string.Empty;
                }
                switch (m_DetailRow.TriggerType)
                {
                    case "OnInteract":
                        return TriggerTypeEnum.OnInteract;
                    default:
                        return TriggerTypeEnum.Unknown;
                }
            }
            set
            {
                switch (value)
                {
                    case TriggerTypeEnum.Unknown:
                        m_DetailRow.TriggerType = string.Empty;
                        break;
                    case TriggerTypeEnum.OnInteract:
                        m_DetailRow.TriggerType = TriggerTypeEnum.OnInteract.ToString();
                        break;
                }
            }
        }

        TriggerEventTable.TriggerEventRow m_DetailRow;
        public TriggerEventWrapper(TriggerEventTable.TriggerEventRow row) : base(row)
        {
            m_DetailRow = row;
            UpdateInstructions();
        }

        [PropertyOrder(10)]
        [FoldoutGroup("$Title")]
        [LabelText("指令")]
        // [ShowInInspector]
        [SerializeField]
        // [HideLabel]
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true, DraggableItems = false, Expanded = true)]
        [TableList]
        // [GUIColor("#FF0000")]
        protected List<TriggerInstrucationWrapper> m_Instructions = new List<TriggerInstrucationWrapper>();



        public void UpdateInstructions()
        {
            m_Instructions.Clear();
            foreach (var instruction_id in m_DetailRow.GetInstructionList())
            {
                var row = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<InstructionTableOverview, InstructionTable.InstructionRow>(instruction_id);
                var wrapper = new TriggerInstrucationWrapper(row);
                wrapper.RemoveRow = RemoveInstruction;
                wrapper.RemoveRowRef = RemoveInstruction;
                m_Instructions.Add(wrapper);
            }
        }

        public void RemoveInstruction(int id)
        {
            m_DetailRow.RemoveInstructionId(id);
            var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<InstructionTableOverview>(Id);
            EditorUtility.SetDirty(overview);
            AssetDatabase.SaveAssets();
            UpdateInstructions();
        }

        // [Button("完全删除")]
        // [FoldoutGroup("$Title")]
        // protected override void Remove(){
        //     base.Remove();
        //     var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<TriggerEventTableOverview>(Id);
        //     overview.Data.Rows.Remove(m_DetailRow);
        //     EditorUtility.SetDirty(overview);
        //     AssetDatabase.SaveAssets();
        // }
    }
}
#endif