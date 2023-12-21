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
    public class InstructionDynamicObjectRunExternalInstructionParams : InstructionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectUuidDropdown")]
        [LabelText("动态物体")]
        public string DynamicObjectUuid;

        [JsonMember("InstruciontUuid")]
        [LabelText("指令Uuid")]
        [ValueDropdown("OnInstructionUuidDropdown")]
        [ListDrawerSettings(Expanded = true)]
        public string InstruciontUuid;

        [JsonMember("Targets")]
        [LabelText("目标列表")]
        [ValueDropdown("OnTargetDropdown")]
        public string[] Targets = new string[0];




#if UNITY_EDITOR
        IEnumerable OnDynamicObjectUuidDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }

        IEnumerable OnInstructionUuidDropdown()
        {
            return InstructionOverview.GetUuidDropdown();
        }

        IEnumerable OnTargetDropdown()
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
            var par = JsonMapper.ToObject<InstructionDynamicObjectRunExternalInstructionParams>(val);
            InstruciontUuid = par.InstruciontUuid;
            DynamicObjectUuid = par.DynamicObjectUuid;
            Targets = par.Targets;

        }
    }
}