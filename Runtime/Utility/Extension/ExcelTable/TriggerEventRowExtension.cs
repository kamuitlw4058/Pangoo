using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;

namespace Pangoo
{

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


        public static List<int> GetInstructionList(this TriggerEventTable.TriggerEventRow row)
        {
            if (row == null || row.InstructionList.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.InstructionList.ToListInt();
        }

        public static List<int> GetFailInstructionList(this TriggerEventTable.TriggerEventRow row)
        {
            if (row == null || row.FailInstructionList.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.FailInstructionList.ToListInt();
        }

        public static List<int> GetConditionList(this TriggerEventTable.TriggerEventRow row)
        {
            if (row == null || row.ConditionList.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.ConditionList.ToListInt();
        }

        public static void AddInstructionId(this TriggerEventTable.TriggerEventRow row, int id)
        {
            if (row == null)
            {
                Debug.LogError(Utility.Text.Format("row is null.{0}", row));
                return;
            }

            var list = row.GetInstructionList();
            list.Add(id);
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
