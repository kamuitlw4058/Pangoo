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
        public static List<int> GetInstructionList(this TriggerEventTable.TriggerEventRow row)
        {
            if (row == null || row.InstructionList.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.InstructionList.ToListInt();
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
