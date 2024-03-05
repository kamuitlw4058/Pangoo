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
                 row = handler(uuid);
            }
#endif
            return row;
        }



    }
}
