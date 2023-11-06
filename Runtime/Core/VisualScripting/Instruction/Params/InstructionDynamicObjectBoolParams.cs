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
    public class InstructionDynamicObjectBoolParams : InstructionParams
    {
        [JsonMember("DynamicObjectId")]
        [ValueDropdown("OnDynamicObjectIdDropdown")]
        public int DynamicObjectId;

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
            var par = JsonMapper.ToObject<InstructionDynamicObjectBoolParams>(val);
            Val = par.Val;
            DynamicObjectId = par.DynamicObjectId;

        }
    }
}