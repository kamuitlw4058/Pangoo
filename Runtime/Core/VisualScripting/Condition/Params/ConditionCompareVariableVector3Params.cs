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
        [ValueDropdown("OnVariableUuidValueDropdown")]
        [LabelText("变量Uuid")]
        [ShowIf("ValueSourceType",ValueSourceTypeEnum.Variable)]
        public string VariableUuid;
        
        [JsonMember("Value")]
        [LabelText("目标值")]
        public Vector3 Value;

        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectUuidValueDropdown")]
        [ShowIf("ValueSourceType",ValueSourceTypeEnum.Path)]
        public string DynamicObjectUuid;
        
        [ShowIf("ValueSourceType",ValueSourceTypeEnum.Path)]
        [JsonMember("Path")]
        [ValueDropdown("OnPathValueDropDown")]
        public string Path;
        
        [ShowIf("ValueSourceType",ValueSourceTypeEnum.Path)]
        [JsonMember("DynamicObjectValueType")]
        public DynamicObjectValueTypeEnum DynamicObjectValueType;
        
#if UNITY_EDITOR
        IEnumerable OnVariableUuidValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Vector3.ToString(), VariableType.ToString());
        }

        IEnumerable OnDynamicObjectUuidValueDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }

        IEnumerable OnPathValueDropDown()
        {
            GameObject prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(DynamicObjectUuid);
            return GameSupportEditorUtility.RefPrefabStringDropdown(prefab);
        }
#endif
    }
    public enum ValueSourceTypeEnum
    {
        Variable,
        Path,
    }

    public enum DynamicObjectValueTypeEnum
    {
        Rotation,
        Position,
    }
}

