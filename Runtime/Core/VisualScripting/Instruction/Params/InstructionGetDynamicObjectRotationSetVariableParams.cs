using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionGetDynamicObjectRotationSetVariableParams : InstructionParams
    {
        [JsonMember("LocalVariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Vector3.ToString())")]
        public string VariableUuid;
    }
}

