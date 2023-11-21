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
    public class ConditionLockedBoolParams : ConditionParams
    {
        [JsonMember("OpenedVariableId")]
        [ValueDropdown("OnOpenedVariableIdValueDropdown")]
        [LabelText("变量Id")]

        public int OpenedVariableId;

        [JsonMember("OpenedChecked")]
        [LabelText("打开状态检查")]
        public bool OpenedChecked;

        [JsonMember("LockedVariableId")]
        [ValueDropdown("OnLockedVariableIdValueDropdown")]
        [LabelText("变量Id")]

        public int LockedVariableId;


        [JsonMember("LockedCheck")]
        [LabelText("锁定状态")]
        public bool LockedCheck;

#if UNITY_EDITOR


        IEnumerable OnOpenedVariableIdValueDropdown()
        {
            return GameSupportEditorUtility.GetVariableIds(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.DynamicObject.ToString());
        }

        IEnumerable OnLockedVariableIdValueDropdown()
        {
            return GameSupportEditorUtility.GetVariableIds(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.Global.ToString());
        }
#endif


        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<ConditionLockedBoolParams>(val);
            OpenedVariableId = par.OpenedVariableId;
            OpenedChecked = par.OpenedChecked;

            LockedVariableId = par.LockedVariableId;
            LockedCheck = par.LockedCheck;

        }

    }
}