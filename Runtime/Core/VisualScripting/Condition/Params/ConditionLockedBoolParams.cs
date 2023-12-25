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
    public class ConditionLockedBoolParams : ConditionParams
    {
        [JsonMember("OpenedVariableUuid")]
        [ValueDropdown("OnOpenedVariableIdValueDropdown")]
        [LabelText("变量Id")]

        public string OpenedVariableUuid;

        [JsonMember("OpenedChecked")]
        [LabelText("打开状态检查")]
        public bool OpenedChecked;

        [JsonMember("LockedVariableUuid")]
        [ValueDropdown("OnLockedVariableIdValueDropdown")]
        [LabelText("变量Id")]

        public string LockedVariableUuid;


        [JsonMember("LockedCheck")]
        [LabelText("锁定状态")]
        public bool LockedCheck;

#if UNITY_EDITOR


        IEnumerable OnOpenedVariableIdValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.DynamicObject.ToString());
        }

        IEnumerable OnLockedVariableIdValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.Global.ToString());
        }
#endif


        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<ConditionLockedBoolParams>(val);
            OpenedVariableUuid = par.OpenedVariableUuid;
            OpenedChecked = par.OpenedChecked;

            LockedVariableUuid = par.LockedVariableUuid;
            LockedCheck = par.LockedCheck;

        }

    }
}