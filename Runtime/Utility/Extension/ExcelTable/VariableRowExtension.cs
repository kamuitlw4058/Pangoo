using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;

namespace Pangoo
{

    public static class VariableRowExtension
    {
        public static VariablesTable.VariablesRow GetById(int id, VariablesTable table = null)
        {
            VariablesTable.VariablesRow row = null;

#if UNITY_EDITOR
            if (Application.isPlaying && table != null)
            {
                // Debug.Log($"GetRowByInstructionTable");
                row = table.GetRowById(id);
            }
            else
            {
                row = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<VariablesTableOverview, VariablesTable.VariablesRow>(id);
            }

#else
            if (table == null)
            {
                Debug.LogError($"InstructionRow Get Table is Null");
            }
            else
            {
                row = table.GetRowById(id);
            }
#endif
            return row;
        }





    }
}
