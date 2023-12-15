using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using Pangoo.Common;

namespace Pangoo
{

    public static class DynamicObjectRowExtension
    {
        public static List<int> GetTriggerEventIdList(this DynamicObjectTable.DynamicObjectRow row)
        {
            if (row == null || row.TriggerEventIds.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.TriggerEventIds.ToSplitList<int>();
        }

        public static List<int> GetHotspotIdList(this DynamicObjectTable.DynamicObjectRow row)
        {
            if (row == null || row.HotspotIds.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.HotspotIds.ToSplitList<int>();
        }

        public static void AddTriggerEventId(this DynamicObjectTable.DynamicObjectRow row, int id)
        {
            if (row == null)
            {
                Debug.LogError(Utility.Text.Format("row is null.{0}", row));
                return;
            }

            var list = row.GetTriggerEventIdList();
            list.Add(id);
            row.TriggerEventIds = list.ToListString();
        }

        public static void RemoveTriggerEventId(this DynamicObjectTable.DynamicObjectRow row, int id)
        {
            if (row == null)
            {
                Debug.LogError(Utility.Text.Format("row is null.{0}", row));
                return;
            }

            var list = row.GetTriggerEventIdList();
            list.Remove(id);
            row.TriggerEventIds = list.ToListString();

        }



    }
}
