using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionCheckVariableBoolListParams : InstructionParams
    {
        [JsonMember("VariableIdList")]
        [ValueDropdown("OnGlobalVariableIdValueDropdown")]
        public List<int> VariableIdList;

        [JsonMember("SetVariableID")]
        [ValueDropdown("OnGlobalVariableIdValueDropdown")]
        public int SetVariableID;
        
        IEnumerable OnGlobalVariableIdValueDropdown()
        {
            return GameSupportEditorUtility.GetVariableIds(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.Global.ToString());
        }

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionCheckVariableBoolListParams>(val);
            VariableIdList = par.VariableIdList;
            SetVariableID = par.SetVariableID;
        }
    }
}
