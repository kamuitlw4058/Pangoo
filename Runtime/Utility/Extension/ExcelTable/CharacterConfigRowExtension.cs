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
    public delegate ICharacterConfigRow CharacterConfigGetRowByUuidHandler(string uuid);

    public static class CharacterConfigRowExtension
    {
        public static ICharacterConfigRow GetByUuid(string uuid, CharacterConfigGetRowByUuidHandler handler = null)
        {
            ICharacterConfigRow row = null;

#if UNITY_EDITOR
            if (Application.isPlaying && handler != null)
            {
                // Debug.Log($"GetRowByInstructionTable");
                row = handler(uuid);
            }
            else
            {
                var oldRow = CharacterConfigOverview.GetUnityRowByUuid(uuid);
                row = oldRow.Row;
            }

#else
            if (handler == null)
            {
                Debug.LogError($"ICharacterConfigRow Get Table is Null");
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
