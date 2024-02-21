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
    public class InstructionSetLocalIntVariableParams : InstructionSetDynamicObjectVariableParams
    {
        [JsonMember("Value")]
        public int Value;

        public override VariableValueTypeEnum ValueTypeEnum => VariableValueTypeEnum.Int;
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionSetLocalIntVariableParams>(val);
            DynamicObjectUuid = par.DynamicObjectUuid;
            LocalVariableUuid = par.LocalVariableUuid;
            Value = par.Value;
        }
    }
}

