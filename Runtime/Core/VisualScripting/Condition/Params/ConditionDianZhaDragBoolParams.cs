using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class ConditionDianZhaDragBoolParams : ConditionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectUuidValueDropdown")]
        public string DynamicObjectUuid;
        
        [JsonMember("Path")]
        [ValueDropdown("OnPathValueDropDown")]
        public string Path;
        
        [JsonMember("Value")]
        [LabelText("目标旋转值")]
        public Vector3 Value;

        [JsonMember("CountVariableUuid")]
        [ValueDropdown("OnIntVariableUuidValueDropdown")]
        public string CountVariableUuid;
        [LabelText("目标次数")]
        [JsonMember("TargetCount")]
        public int TargetCount;

        [LabelText("执行标记变量")]
        [JsonMember("InvokeFlagVariableUuid")]
        [ValueDropdown("OnBoolVariableUuidValueDropdown")]
        public string InvokeFlagVariableUuid;
        
#if UNITY_EDITOR
        IEnumerable OnBoolVariableUuidValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString());
        }
        
        IEnumerable OnIntVariableUuidValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Int.ToString());
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
}

