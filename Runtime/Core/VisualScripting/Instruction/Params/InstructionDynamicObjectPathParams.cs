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
    public class InstructionDynamicObjectPathParams : InstructionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectIdDropdown")]
        public string DynamicObjectUuid;


        [JsonMember("Path")]
        [LabelText("路径列表")]
        [ValueDropdown("OnDynamicObjectPathDropdown")]
        [ListDrawerSettings(Expanded = true)]
        public string Path;



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
            var par = JsonMapper.ToObject<InstructionDynamicObjectPathParams>(val);
            Path = par.Path;
            DynamicObjectUuid = par.DynamicObjectUuid;

        }
    }
}