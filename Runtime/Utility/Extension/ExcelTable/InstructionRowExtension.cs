using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;

namespace Pangoo
{

    public static class InstructionRowExtension
    {
        public static InstructionTable.InstructionRow GetById(int id, InstructionTable table = null)
        {
            InstructionTable.InstructionRow instructionRow = null;

#if UNITY_EDITOR
            if (Application.isPlaying && table != null)
            {
                Debug.Log($"GetRowByInstructionTable");
                instructionRow = table.GetRowById(id);
            }
            else
            {
                instructionRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<InstructionTableOverview, InstructionTable.InstructionRow>(id);
            }

#else
            if (table == null)
            {
                Debug.LogError($"InstructionRow Get Table is Null");
            }else{
                instructionRow = table.GetRowById(id);
            }
#endif
            return instructionRow;
        }





    }
}
