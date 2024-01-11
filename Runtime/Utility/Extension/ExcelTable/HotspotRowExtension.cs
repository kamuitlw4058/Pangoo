using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using Pangoo.Common;
using Pangoo.MetaTable;
using LitJson;

namespace Pangoo
{
    public delegate IHotspotRow HotspotGetRowByIdHandler(int id);
    public delegate IHotspotRow HotspotGetRowByUuidHandler(string uuid);


    public static class HotspotRowExtension
    {
        public static IHotspotRow GetById(int id, HotspotGetRowByIdHandler handler = null)
        {
            IHotspotRow row = null;

#if UNITY_EDITOR
            if (Application.isPlaying && handler != null)
            {
                row = handler(id);
            }
            else
            {
                var oldRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<HotspotTableOverview, HotspotTable.HotspotRow>(id);
                var rowJson = JsonMapper.ToJson(oldRow);
                var newRow = JsonMapper.ToObject<HotspotRow>(rowJson);
                row = newRow;
            }
#else
            if (handler == null)
            {
                Debug.LogError($"Handler is Null");
            }
            else
            {
                row = handler(id);
            }
#endif
            return row;
        }

        public static IHotspotRow GetByUuid(string uuid, HotspotGetRowByUuidHandler handler = null)
        {
            IHotspotRow row = null;

#if UNITY_EDITOR
            if (Application.isPlaying && handler != null)
            {
                // Debug.Log($"HotspotTable");
                row = handler(uuid);
            }
            else
            {
                var unityRow = HotspotOverview.GetUnityRowByUuid(uuid);
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
