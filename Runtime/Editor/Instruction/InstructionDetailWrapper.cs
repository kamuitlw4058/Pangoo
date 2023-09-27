#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;
using GameFramework;

namespace Pangoo
{
    public class InstructionDetailWrapper : ExcelTableRowDetailWrapper<InstructionTableOverview, InstructionTable.InstructionRow>
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

                return Row?.InstructionType;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.InstructionType = value;
                    Row.Params = "{}";
                    Save();
                    UpdateInstruction();
                }
            }
        }

        void UpdateInstruction()
        {

            var instructionType = Utility.Assembly.GetType(Row.InstructionType);
            if (instructionType == null)
            {
                return;
            }

            m_InstructionInstance = Activator.CreateInstance(instructionType) as Instruction;
            m_InstructionInstance.LoadParams(Row.Params);
        }

        [ShowInInspector]
        [ReadOnly]
        public string InstructionParams
        {
            get
            {
                return Row?.Params;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.Params = value;
                    Save();
                }
            }
        }
        Instruction m_InstructionInstance;

        [ShowInInspector]
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
            InstructionParams = InstructionInstance.ParamsString();
        }

        [Button("加载参数")]
        [TableColumnWidth(80, resizable: false)]
        public void LoadParams()
        {
            InstructionInstance.LoadParams(Row.Params);
        }

        [Button("立即运行")]
        public void Run()
        {
            InstructionInstance.RunImmediate(null);
        }

    }


}
#endif