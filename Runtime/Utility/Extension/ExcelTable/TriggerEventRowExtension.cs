using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using Pangoo.Common;
using Pangoo.MetaTable;

namespace Pangoo
{
    public delegate ITriggerEventRow TriggerEventRowByUuidHandler(string uuid);

    public static class TriggerEventRowExtension
    {

        public static TriggerEventTable.TriggerEventRow GetById(int id, TriggerEventTable table = null)
        {
            TriggerEventTable.TriggerEventRow row = null;
#if UNITY_EDITOR
            if (Application.isPlaying && table != null)
            {
                Debug.Log($"GetRowByTriggerEventTable");
                row = table?.GetRowById(id);
            }
            else
            {
                row = GameSupportEditorUtility.GetTriggerEventRowById(id);
            }
#else
            if(table == null){
                Debug.LogError($"GetTriggerEventRow Table Is null");
            }else{
                 row = table?.GetRowById(id);
            }
#endif
            return row;
        }


        public static ITriggerEventRow GetByUuid(string uuid, TriggerEventRowByUuidHandler handler = null)
        {
            ITriggerEventRow row = null;
#if UNITY_EDITOR
            if (Application.isPlaying && handler != null)
            {
                // Debug.Log($"GetRowByTriggerEventTable");
                row = handler(uuid);
            }
            else
            {
                var triggerRow = TriggerEventOverview.GetUnityRowByUuid(uuid);
                row = triggerRow.Row;
            }
#else
            if(handler == null){
                Debug.LogError($"GetTriggerEventRow Table Is null");
            }else{
                 row = handler(id);
            }
#endif
            return row;
        }

        public static List<int> GetInstructionList(this TriggerEventTable.TriggerEventRow row)
        {
            if (row == null || row.InstructionList.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.InstructionList.ToSplitList<int>();
        }

        public static List<int> GetFailInstructionList(this TriggerEventTable.TriggerEventRow row)
        {
            if (row == null || row.FailInstructionList.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.FailInstructionList.ToSplitList<int>();
        }

        public static List<int> GetConditionList(this TriggerEventTable.TriggerEventRow row)
        {
            if (row == null || row.ConditionList.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.ConditionList.ToSplitList<int>();
        }

        public static void AddInstructionId(this TriggerEventTable.TriggerEventRow row, int id)
        {
            if (row == null)
            {
                Debug.LogError(GameFramework.Utility.Text.Format("row is null.{0}", row));
                return;
            }

            var list = row.GetInstructionList();
            list.Add(id);
            row.InstructionList = list.ToListString();
        }

        public static void AddInstructionIds(this TriggerEventTable.TriggerEventRow row, List<int> ids)
        {
            if (row == null)
            {
                Debug.LogError(Utility.Text.Format("row is null.{0}", row));
                return;
            }

            var list = row.GetInstructionList();
            list.AddRange(ids);
            row.InstructionList = list.ToListString();
        }

        public static void RemoveInstructionId(this TriggerEventTable.TriggerEventRow row, int id)
        {
            if (row == null)
            {
                Debug.LogError(Utility.Text.Format("row is null.{0}", row));
                return;
            }

            var list = row.GetInstructionList();
            list.Remove(id);
            row.InstructionList = list.ToListString();
        }

        public static string ToRowString(this TriggerEventTable.TriggerEventRow row)
        {
            return $"<{row.Id}-{row.Name}>{row.GetInstructionList().Count}";
        }



    }
}
