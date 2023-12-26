using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionCheckVariableBoolListParams : InstructionParams
    {
        [JsonMember("VariableUuidList")]
        [ValueDropdown("OnGlobalVariableIdValueDropdown")]
        public List<string> VariableUuidList;

        [JsonMember("SetVariableID")]
        [ValueDropdown("OnGlobalVariableIdValueDropdown")]
        public string SetVariableUuid;

#if UNITY_EDITOR
        IEnumerable OnGlobalVariableIdValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.Global.ToString());
        }
#endif

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionCheckVariableBoolListParams>(val);
            VariableUuidList = par.VariableUuidList;
            SetVariableUuid = par.SetVariableUuid;
        }
    }
}
