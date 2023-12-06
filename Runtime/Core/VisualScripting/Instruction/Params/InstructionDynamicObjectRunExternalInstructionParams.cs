using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;
using Unity.VisualScripting.YamlDotNet.Core;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionDynamicObjectRunExternalInstructionParams : InstructionParams
    {
        [JsonMember("DynamicObjectId")]
        [ValueDropdown("OnDynamicObjectIdDropdown")]
        [LabelText("动态物体")]
        public int DynamicObjectId;

        [JsonMember("InstruciontId")]
        [LabelText("指令Id")]
        [ValueDropdown("OnInstructionIdDropdown")]
        [ListDrawerSettings(Expanded = true)]
        public int InstruciontId;

        [JsonMember("Targets")]
        [LabelText("目标列表")]
        [ValueDropdown("OnTargetDropdown")]
        public string[] Targets = new string[0];




#if UNITY_EDITOR
        IEnumerable OnDynamicObjectIdDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<DynamicObjectTableOverview>();
        }

        IEnumerable OnInstructionIdDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<InstructionTableOverview>();
        }

        IEnumerable OnTargetDropdown()
        {
            var prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectId(DynamicObjectId);
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
            InstruciontId = par.InstruciontId;
            DynamicObjectId = par.DynamicObjectId;
            Targets = par.Targets;

        }
    }
}