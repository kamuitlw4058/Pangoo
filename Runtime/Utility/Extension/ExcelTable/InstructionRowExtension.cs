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
    public static class InstructionRowExtension
    {

        public static IInstructionRow GetById(int id, InstructionGetRowByIdHandler handler = null)
        {
            IInstructionRow instructionRow = null;

#if UNITY_EDITOR
            if (Application.isPlaying && handler != null)
            {
                Debug.Log($"GetRowByInstructionTable");
                instructionRow = handler(id);
            }
            else
            {
                var oldRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<InstructionTableOverview, InstructionTable.InstructionRow>(id);
                var rowJson = JsonMapper.ToJson(oldRow);
                var newRow = JsonMapper.ToObject<InstructionRow>(rowJson);
                instructionRow = newRow;
            }

#else
            if (handler == null)
            {
                Debug.LogError($"InstructionRow Get Table is Null");
            }else{
                instructionRow = handler(id);
            }
#endif
            return instructionRow;
        }


    }
}
