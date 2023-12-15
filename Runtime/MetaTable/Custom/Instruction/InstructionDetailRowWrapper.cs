#if UNITY_EDITOR

using System;
using System.Collections;
using Sirenix.OdinInspector;
using MetaTable;
using Pangoo.Core.VisualScripting;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class InstructionDetailRowWrapper : MetaTableDetailRowWrapper<InstructionOverview, UnityInstructionRow>
    {
        [ShowInInspector]
        [ValueDropdown("GetInstructionType")]
        public string InstructionType
        {
            get
            {
                if (m_InstructionInstance == null)
                {
                    UpdateInstruction();
                }

                return UnityRow.Row?.InstructionType;
            }
            set
            {

                UnityRow.Row.InstructionType = value;
                UnityRow.Row.Params = "{}";
                Save();
                UpdateInstruction();
            }
        }

        void UpdateInstruction()
        {
            if (UnityRow.Row.InstructionType.IsNullOrWhiteSpace())
            {
                return;
            }

            m_InstructionInstance = ClassUtility.CreateInstance(UnityRow.Row.InstructionType) as Instruction;
            if (m_InstructionInstance == null)
            {
                return;
            }

            m_InstructionInstance.Load(UnityRow.Row.Params);
        }

        [ShowInInspector]
        [ReadOnly]
        public string InstructionParams
        {
            get
            {
                return UnityRow.Row?.Params;
            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.Params = value;
                    Save();
                }
            }
        }
        Instruction m_InstructionInstance;

        [ShowInInspector]
        [HideLabel]
        [HideReferenceObjectPicker]
        public Instruction InstructionInstance
        {
            get
            {
                return m_InstructionInstance;
            }
            set
            {
                m_InstructionInstance = value;
            }
        }

        public IEnumerable GetInstructionType()
        {
            return GameSupportEditorUtility.GetInstructionType();
        }

        [Button("保存参数")]
        [TableColumnWidth(80, resizable: false)]
        public void SaveParams()
        {
            InstructionParams = InstructionInstance.Save();
        }

        [Button("加载参数")]
        [TableColumnWidth(80, resizable: false)]
        public void LoadParams()
        {
            InstructionInstance.Load(UnityRow.Row.Params);
        }

        [Button("立即运行")]
        public void Run()
        {
            InstructionInstance.RunImmediate(null);
        }
    }
}
#endif

