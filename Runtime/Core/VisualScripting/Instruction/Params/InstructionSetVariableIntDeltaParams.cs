using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionSetVariableIntDeltaParams : InstructionParams
    {
        [JsonMember("VariableUuid")]
        [ValueDropdown("OnVariableUuidDropdown")]
        public string VariableUuid;


        [JsonMember("DeltaValue")]
        public int DeltaValue;
        
        public VariableValueTypeEnum ValueTypeEnum => VariableValueTypeEnum.Int;
        
#if UNITY_EDITOR
        public IEnumerable OnVariableUuidDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(ValueTypeEnum.ToString());
        }

#endif
    }
}

