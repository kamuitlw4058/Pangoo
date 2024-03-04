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
