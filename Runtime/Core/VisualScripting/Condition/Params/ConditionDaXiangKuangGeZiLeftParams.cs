using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pangoo.Core.VisualScripting
{
        [Serializable]
        public class ConditionDaXiangKuangGeZiLeftParams : ConditionParams
        {
                [JsonMember("StaticVariableUuid")]
                [ValueDropdown("OnGlobalVariableIdValueDropdown")]
                public string StaticVariableUuid;

                [JsonMember("HasVariableUuid")]
                [ValueDropdown("OnGlobalVariableIdValueDropdown")]
                public string HasVariableUuid;
                [JsonMember("UsedVariableUuid")]
                [ValueDropdown("OnGlobalVariableIdValueDropdown")]
                public string UsedVariableUuid;
                [JsonMember("SelectVariableUuid")]
                [ValueDropdown("OnGlobalVariableIdValueDropdown")]
                public string SelectVariableUuid;

#if UNITY_EDITOR
                IEnumerable OnGlobalVariableIdValueDropdown()
                {
                        return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.Global.ToString());
                }

#endif

                public override void Load(string val)
                {
                        var par = JsonMapper.ToObject<ConditionDaXiangKuangGeZiLeftParams>(val);
                        StaticVariableUuid = par.StaticVariableUuid;
                        HasVariableUuid = par.HasVariableUuid;
                        UsedVariableUuid = par.UsedVariableUuid;
                        SelectVariableUuid = par.SelectVariableUuid;
                }
        }
}
