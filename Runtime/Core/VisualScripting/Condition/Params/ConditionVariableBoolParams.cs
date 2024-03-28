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

        [JsonMember("VariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableType.ToString(),false)")]
        [LabelText("变量Uuid")]

        public string VariableUuid;


        [JsonMember("CheckBool")]
        [LabelText("检测目标")]
        public bool CheckBool;
    }
}