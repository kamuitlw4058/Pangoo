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
    public class ConditionVariableBoolParams : ConditionParams
    {
        [JsonMember("VariableType")]
        [OnValueChanged("OnVariableTypeChanged")]
        public VariableTypeEnum VariableType;

        [JsonMember("ValueSourceType")]
        public ValueSourceTypeEnum ValueSourceType = ValueSourceTypeEnum.Variable;
        
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectUuidValueDropdown")]
        [ShowIf("ValueSourceType",ValueSourceTypeEnum.DynamicObject)]
        public string DynamicObjectUuid;
        
        [JsonMember("VariableUuid")]
        [ValueDropdown("OnVariableUuidValueDropdown")]
        [LabelText("变量Uuid")]
        public string VariableUuid;

        [JsonMember("CheckBool")]
        [LabelText("检测目标")]
        public bool CheckBool;

#if UNITY_EDITOR
        void OnVariableTypeChanged()
        {
            VariableUuid = string.Empty;
        }
        IEnumerable OnVariableUuidValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableType.ToString());
        }

        IEnumerable OnDynamicObjectUuidValueDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }
#endif
        

    }
}