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
    public class InstructionDynamicObjectPathBoolParams : InstructionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectUuidDropdown")]
        public string DynamicObjectUuid;


        [JsonMember("Path")]
        [LabelText("路径列表")]
        [ValueDropdown("OnDynamicObjectPathDropdown")]
        [ListDrawerSettings(Expanded = true)]
        public string Path;


        [JsonMember("Val")]
        public bool Val;

#if UNITY_EDITOR
        IEnumerable OnDynamicObjectUuidDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown(AdditionalOptions: new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Self","Self")
        });
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
            var par = JsonMapper.ToObject<InstructionDynamicObjectPathBoolParams>(val);
            Val = par.Val;
            Path = par.Path;
            DynamicObjectUuid = par.DynamicObjectUuid;

        }
    }
}