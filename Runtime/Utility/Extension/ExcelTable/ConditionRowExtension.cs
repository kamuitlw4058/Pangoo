using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using Pangoo.MetaTable;

namespace Pangoo
{
    public delegate IConditionRow ConditionGetRowByUuidHandler(string uuid);

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
                Debug.LogError($" Get ConditionTable is Null");
            }
            else
            {
                row = table.GetRowById(id);
            }
#endif
            return row;
        }



        public static IConditionRow GetByUuid(string uuid, ConditionGetRowByUuidHandler handler = null)
        {
            IConditionRow row = null;

#if UNITY_EDITOR
            if (Application.isPlaying && handler != null)
            {
                // Debug.Log($"ConditionTable");
                row = handler(uuid);
            }
            else
            {
                var unityRow = ConditionOverview.GetUnityRowByUuid(uuid);
                row = unityRow.Row;
            }

#else
            if (handler == null)
            {
                Debug.LogError($" Get ConditionTable is Null");
            }
            else
            {
                row = handler(uuid);
            }
#endif
            return row;
        }


    }
}
