using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Pangoo.Editor
{
    public class InstructionDetailWrapper : ExcelTableRowDetailWrapper<InstructionTableOverview, InstructionTable.InstructionRow>
    {

        [ShowInInspector]
        [ValueDropdown("GetInstructionType")]
        // [OnValueChanged("OnInstructionTypeChange")]
        public string InstructionType
        {
            get
            {
                return Row?.InstructionType;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.InstructionType = value;
                    Save();
                }
            }
        }

        [ShowInInspector]
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

        // public void OnInstructionTypeChange()
        // {
        //     if (m_DetailRow.InstructionType == null)
        //     {
        //         InstructionInstance = null;
        //         return;
        //     }

        //     var type = Utility.Assembly.GetType(m_DetailRow.InstructionType);
        //     if (type == null)
        //     {
        //         InstructionInstance = null;
        //         return;
        //     }


        //     if (InstructionInstance == null || type != InstructionInstance.GetType())
        //     {
        //         InstructionInstance = Activator.CreateInstance(type) as Instruction;
        //         InstructionInstance.LoadParams(m_DetailRow.Params);
        //         return;
        //     }

        // }

        public IEnumerable GetInstructionType()
        {
            return GameSupportEditorUtility.GetInstructionType();
        }

    }
}