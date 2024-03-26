using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class ConditionCompareVariableVector3Params : ConditionParams
    {
        [JsonMember("VariableType")]
        [OnValueChanged("OnVariableTypeChanged")]
        public VariableTypeEnum VariableType;

        [JsonMember("VariableUuid")]
        [ValueDropdown("OnVariableUuidValueDropdown")]
        [LabelText("变量Uuid")]
        public string VariableUuid;
        
        [JsonMember("Value")]
        [LabelText("目标值")]
        public Vector3 Value;
        
        [JsonMember("Path")]
        public string Path;
        
#if UNITY_EDITOR
        void OnVariableTypeChanged()
        {
            VariableUuid = string.Empty;
        }

        IEnumerable OnVariableUuidValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Vector3.ToString(), VariableType.ToString());
        }
#endif
    }
}

