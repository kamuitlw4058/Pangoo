using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class ConditionDaXiangKuangGeZiLeftParams : ConditionParams
    {
        [JsonMember("IsStaticVariableId")]
        [ValueDropdown("OnGlobalVariableIdValueDropdown")]
        public int IsStaticVariableId;
        [JsonMember("IsHasVariableId")]
        [ValueDropdown("OnGlobalVariableIdValueDropdown")]
        public int IsHasVariableId;
        [JsonMember("IsUsedVariableId")]
        [ValueDropdown("OnGlobalVariableIdValueDropdown")]
        public int IsUsedVariableId;
        
        #if UNITY_EDITOR
        IEnumerable OnGlobalVariableIdValueDropdown()
        {
            return GameSupportEditorUtility.GetVariableIds(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.Global.ToString());
        }
        #endif
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<ConditionDaXiangKuangGeZiLeftParams>(val);
            IsStaticVariableId = par.IsStaticVariableId;
            IsHasVariableId = par.IsHasVariableId;
            IsUsedVariableId = par.IsUsedVariableId;
        }
    }
}
