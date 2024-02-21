using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionSetLocalBoolVariableParams : InstructionSetDynamicObjectVariableParams
    {
        [JsonMember("Value")]
        public bool Value;

        public override void CheckFlag(UnityVariablesRow row)
        {
            flag = VariableValueTypeEnum.Bool.ToString().IsNullOrWhiteSpace() ? true :
                VariableValueTypeEnum.Bool.ToString().Equals(row.Row.ValueType) ? true : false;
        }

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionSetLocalBoolVariableParams>(val);
            DynamicObjectUuid = par.DynamicObjectUuid;
            LocalVariableUuid = par.LocalVariableUuid;
            Value = par.Value;
        }
    }
}

