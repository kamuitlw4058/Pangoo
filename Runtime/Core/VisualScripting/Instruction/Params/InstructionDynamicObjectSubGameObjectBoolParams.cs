using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionDynamicObjectSubGameObjectBoolParams : InstructionParams
    {
        [JsonMember("DynamicObjectId")]
        [ValueDropdown("OnDynamicObjectIdDropdown")]
        public int DynamicObjectId;

        [JsonMember("Path")]
        public string Path;

        [JsonMember("Val")]
        public bool Val;

#if UNITY_EDITOR
        IEnumerable OnDynamicObjectIdDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<DynamicObjectTableOverview>();
        }
#endif

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionDynamicObjectSubGameObjectBoolParams>(val);
            Val = par.Val;
            Path = par.Path;
            DynamicObjectId = par.DynamicObjectId;

        }
    }
}