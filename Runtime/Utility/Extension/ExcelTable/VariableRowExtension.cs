using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using Pangoo.MetaTable;
using LitJson;

namespace Pangoo
{
    public delegate IVariablesRow VariableGetRowByUuidHandler(string uuid);

    public static class VariableRowExtension
    {
        public static IVariablesRow GetByUuid(string uuid, VariableGetRowByUuidHandler handler = null)
        {
            IVariablesRow row = null;

#if UNITY_EDITOR
            if (Application.isPlaying && handler != null)
            {
                // Debug.Log($"GetRowByInstructionTable");
                row = handler(uuid);
            }
            else
            {
                var oldRow = VariablesOverview.GetUnityRowByUuid(uuid);
                row = oldRow.Row;
            }

#else
            if (handler == null)
            {
                Debug.LogError($"InstructionRow Get Table is Null");
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
