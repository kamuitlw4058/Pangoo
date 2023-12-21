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
    public class InstructionDynamicObjectBoolParams : InstructionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectIdDropdown")]
        public string DynamicObjectUuid;

        [JsonMember("Val")]
        public bool Val;

#if UNITY_EDITOR
        IEnumerable OnDynamicObjectUuidDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }
#endif

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionDynamicObjectBoolParams>(val);
            Val = par.Val;
            DynamicObjectUuid = par.DynamicObjectUuid;

        }
    }
}