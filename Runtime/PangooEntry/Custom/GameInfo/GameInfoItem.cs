using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

namespace Pangoo
{
    [System.Serializable]
    public class GameInfoItem 
    {
        public bool Enable;

        [ValueDropdown("GetGameInfoType")]
        public string GameInfoTypeName;

#if UNITY_EDITOR
        IEnumerable GetGameInfoType(){
            return GameSupportEditorUtility.GetGameInfoItem();
        }
#endif

    }
}