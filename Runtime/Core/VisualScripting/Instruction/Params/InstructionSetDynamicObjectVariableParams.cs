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
        [ValueDropdown("@DynamicObjectOverview.GetUuidDropdown()")]
        public string DynamicObjectUuid;

        [JsonMember("LocalVariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(ValueTypeEnum.ToString(),VariableTypeEnum.DynamicObject.ToString(),false)")]
        public string LocalVariableUuid;

        public abstract VariableValueTypeEnum ValueTypeEnum { get; }
    }
}
