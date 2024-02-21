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
    public abstract class InstructionSetDynamicObjectVariableParams : InstructionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectUuidDropdown")]
        public string DynamicObjectUuid;

        [JsonMember("LocalVariableUuid")]
        [ValueDropdown("OnVariableUuidDropdown")]
        public string LocalVariableUuid;

        public abstract VariableValueTypeEnum ValueTypeEnum { get;}

#if UNITY_EDITOR
        IEnumerable OnDynamicObjectUuidDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }

        public IEnumerable OnVariableUuidDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(ValueTypeEnum.ToString(),VariableTypeEnum.DynamicObject.ToString());
        }
#endif
        public virtual void CheckFlag(UnityVariablesRow row)
        {
            
        }
    }
}
