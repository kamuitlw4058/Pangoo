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
        public class ConditionDaXiangKuangGeZiRightParams : ConditionParams
        {
                [JsonMember("PlayingVariableUuid")]
                [ValueDropdown("OnGlobalVariableIdValueDropdown")]
                public string PlayingVariableUuid;

                [JsonMember("TipShowVariableUuid")]
                [ValueDropdown("OnGlobalVariableIdValueDropdown")]
                public string TipShowVariableUuid;

#if UNITY_EDITOR
                IEnumerable OnGlobalVariableIdValueDropdown()
                {
                        return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.Global.ToString());
                }

#endif

                public override void Load(string val)
                {
                        var par = JsonMapper.ToObject<ConditionDaXiangKuangGeZiRightParams>(val);
                        PlayingVariableUuid = par.PlayingVariableUuid;
                        TipShowVariableUuid = par.TipShowVariableUuid;
                }
        }
}
