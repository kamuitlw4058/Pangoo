using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;

namespace Pangoo
{

    public static class ConditionRowExtension
    {
        public static ConditionTable.ConditionRow GetById(int id, ConditionTable table = null)
        {
            ConditionTable.ConditionRow row = null;

#if UNITY_EDITOR
            if (Application.isPlaying && table != null)
            {
                Debug.Log($"ConditionTable");
                row = table.GetRowById(id);
            }
            else
            {
                row = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<ConditionTableOverview, ConditionTable.ConditionRow>(id);
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
