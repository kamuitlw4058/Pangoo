using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class ConditionObjectDragBoolParams : ConditionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("@DynamicObjectOverview.GetUuidDropdown()")]
        public string DynamicObjectUuid;
        
        [JsonMember("Path")]
        [ValueDropdown("@GameSupportEditorUtility.RefPrefabStringDropdown(GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(DynamicObjectUuid))")]
        public string Path;
        
        [JsonMember("Value")]
        [LabelText("目标旋转值")]
        public Vector3 Value;

        [JsonMember("CountVariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Int.ToString(),null,false)")]
        public string CountVariableUuid;
        [LabelText("目标次数")]
        [JsonMember("TargetCount")]
        public int TargetCount;

        [LabelText("执行标记变量")]
        [JsonMember("InvokeFlagVariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(),null,false)")]
        public string InvokeFlagVariableUuid;

        public void a()
        {
            VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(),null,false);
        }
    }
}

