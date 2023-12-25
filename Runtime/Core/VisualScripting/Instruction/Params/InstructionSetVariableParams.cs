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
    public class InstructionSetVariableParams : InstructionParams
    {
        [JsonMember("VariableType")]
        [OnValueChanged("OnVariableTypeChanged")]
        public VariableTypeEnum VariableType;

        [JsonMember("VariableUuid")]
        [ValueDropdown("OnVariableUuidValueDropdown")]
        public string VariableUuid;


        [JsonMember("Val")]
        public bool Val;

#if UNITY_EDITOR
        IEnumerable OnVariableUuidValueDropdown()
        {
            return VariablesOverview.GetUuidDropdown();
        }

        void OnVariableTypeChanged()
        {
            VariableUuid = string.Empty;
        }

#endif


        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionSetVariableParams>(val);
            VariableUuid = par.VariableUuid;
            Val = par.Val;
        }
    }
}