using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class ConditionVariableBoolParams : ConditionParams
    {
        [JsonMember("VariableType")]
        [OnValueChanged("OnVariableTypeChanged")]
        public VariableTypeEnum VariableType;

        [JsonMember("VariableId")]
        [ValueDropdown("OnVariableIdValueDropdown")]
        [LabelText("变量Id")]

        public int VariableId;


        [JsonMember("CheckBool")]
        [LabelText("检测目标")]
        public bool CheckBool;

#if UNITY_EDITOR
        void OnVariableTypeChanged()
        {
            VariableId = 0;
        }

        IEnumerable OnVariableIdValueDropdown()
        {
            return GameSupportEditorUtility.GetVariableIds(VariableValueTypeEnum.Bool.ToString(), VariableType.ToString());
        }
#endif


        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<ConditionVariableBoolParams>(val);
            VariableId = par.VariableId;
            CheckBool = par.CheckBool;
            VariableType = par.VariableType;
        }

    }
}