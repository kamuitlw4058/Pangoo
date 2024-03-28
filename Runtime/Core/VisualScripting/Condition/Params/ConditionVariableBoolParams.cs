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
        [OnValueChanged("@VariableUuid = string.Empty")]
        public VariableTypeEnum VariableType;

        [JsonMember("ValueSourceType")]
        public ValueSourceTypeEnum ValueSourceType = ValueSourceTypeEnum.Variable;
        
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("@DynamicObjectOverview.GetUuidDropdown()")]
        [ShowIf("ValueSourceType",ValueSourceTypeEnum.DynamicObject)]
        public string DynamicObjectUuid;
        
        [JsonMember("VariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(),"+"this.VariableType.ToString(),false)")]
        [LabelText("变量Uuid")]
        public string VariableUuid;

        [JsonMember("CheckBool")]
        [LabelText("检测目标")]
        public bool CheckBool;

    }
    public enum ValueSourceTypeEnum
    {
        Variable,
        Path,
        DynamicObject,
    }
}