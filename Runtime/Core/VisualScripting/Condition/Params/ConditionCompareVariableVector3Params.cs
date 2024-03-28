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
        public VariableTypeEnum VariableType;

        [LabelText("值来源类型")]
        [JsonMember("ValueSourceType")]
        public ValueSourceTypeEnum ValueSourceType;
        
        [JsonMember("VariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Vector3.ToString(), VariableType.ToString(),false)")]
        [LabelText("变量Uuid")]
        [ShowIf("ValueSourceType",ValueSourceTypeEnum.Variable)]
        public string VariableUuid;
        
        [JsonMember("Value")]
        [LabelText("目标值")]
        public Vector3 Value;

        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("@DynamicObjectOverview.GetUuidDropdown()")]
        [ShowIf("ValueSourceType",ValueSourceTypeEnum.Path)]
        public string DynamicObjectUuid;
        
        [ShowIf("ValueSourceType",ValueSourceTypeEnum.Path)]
        [JsonMember("Path")]
        [ValueDropdown("@GameSupportEditorUtility.RefPrefabStringDropdown(GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(DynamicObjectUuid))")]
        public string Path;
        
        [ShowIf("ValueSourceType",ValueSourceTypeEnum.Path)]
        [JsonMember("DynamicObjectValueType")]
        public DynamicObjectValueTypeEnum DynamicObjectValueType;
    }
    public enum ValueSourceTypeEnum
    {
        Variable,
        Path,
        DynamicObject,
    }

    public enum DynamicObjectValueTypeEnum
    {
        Rotation,
        Position,
    }
}

