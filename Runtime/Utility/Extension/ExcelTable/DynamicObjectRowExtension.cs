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

        public static IDynamicObjectRow ToInterface(this DynamicObjectTable.DynamicObjectRow row)
        {
            var json = LitJson.JsonMapper.ToJson(row);
            return LitJson.JsonMapper.ToObject<Pangoo.MetaTable.DynamicObjectRow>(json);
        }


    }
}
