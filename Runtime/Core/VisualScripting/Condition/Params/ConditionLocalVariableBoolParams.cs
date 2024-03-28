using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class ConditionLocalVariableBoolParams : ConditionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("@DynamicObjectOverview.GetUuidDropdown()")]
        public string DynamicObjectUuid;
        
        [JsonMember("VariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(),VariableTypeEnum.DynamicObject.ToString(),false)")]
        [LabelText("变量Uuid")]
        public string VariableUuid;

        [JsonMember("CheckBool")]
        [LabelText("检测目标")]
        public bool CheckBool;
    }
}
