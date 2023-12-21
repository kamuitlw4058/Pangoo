using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionDynamicObjectSubGameObjectBoolParams : InstructionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectUuidDropdown")]
        [LabelText("动态物体")]
        public string DynamicObjectUuid;

        [JsonMember("Path")]
        [LabelText("路径列表")]
        [ValueDropdown("OnDynamicObjectPathDropdown")]
        [ListDrawerSettings(Expanded = true)]
        public string[] Path;

        [JsonMember("Val")]
        [LabelText("设置值")]
        public bool Val;

#if UNITY_EDITOR
        IEnumerable OnDynamicObjectUuidDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }

        IEnumerable OnDynamicObjectPathDropdown()
        {
            var prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(DynamicObjectUuid);
            if (prefab != null)
            {
                return GameSupportEditorUtility.RefPrefabStringDropdown(prefab);
            }
            else
            {
                return null;
            }

        }
#endif

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionDynamicObjectSubGameObjectBoolParams>(val);
            Val = par.Val;
            Path = par.Path;
            DynamicObjectUuid = par.DynamicObjectUuid;

        }
    }
}