using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionSetDynamicObjectVariableParams : InstructionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectUuidDropdown")]
        public string DynamicObjectUuid;

        [JsonMember("LocalVariableUuid")]
        [ValueDropdown("OnBoolVariableUuidDropdown")]
        public string LocalVariableUuid;

        protected bool flag;
        
#if UNITY_EDITOR
        IEnumerable OnDynamicObjectUuidDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }

        public IEnumerable OnVariableUuidDropdown()
        {
            var ret = new ValueDropdownList<string>();
            var overviews = AssetDatabaseUtility.FindAsset<VariablesOverview>();
            foreach (var overview in overviews)
            {
                foreach (UnityVariablesRow row in overview.Rows)
                {
                    if (row.Row.VariableType.Equals(VariableTypeEnum.DynamicObject.ToString()) ||
                        row.Row.VariableType.IsNullOrWhiteSpace())
                    {
                        CheckFlag(row);
                        
                        if (flag)
                        {
                            ret.Add($"{row.UuidShort}-{row.Name}", row.Uuid);
                        }
                    }
                }
            }
            return ret;
        }
#endif
        public virtual void CheckFlag(UnityVariablesRow row)
        {
            
        }
    }
}
