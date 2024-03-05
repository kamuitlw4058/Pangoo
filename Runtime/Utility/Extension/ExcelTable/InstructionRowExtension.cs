using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using LitJson;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;

namespace Pangoo
{
    public delegate IInstructionRow InstructionGetRowByIdHandler(int id);
    public delegate IInstructionRow InstructionGetRowByUuidHandler(string uuid);

    public static class InstructionRowExtension
    {



        public static IInstructionRow GetByUuid(string uuid, InstructionGetRowByUuidHandler handler = null)
        {
            IInstructionRow instructionRow = null;

#if UNITY_EDITOR
            if (Application.isPlaying && handler != null)
            {
                // Debug.Log($"GetRowByInstructionTable,handler:{handler},uuid:{uuid}");
                instructionRow = handler(uuid);
            }
            else
            {
                var oldRow = InstructionOverview.GetUnityRowByUuid(uuid);
                instructionRow = oldRow.Row;
            }

#else
            if (handler == null)
            {
                Debug.LogError($"InstructionRow Get Table is Null");
            }else{
                instructionRow = handler(uuid);
            }
#endif
            return instructionRow;
        }


    }
}
