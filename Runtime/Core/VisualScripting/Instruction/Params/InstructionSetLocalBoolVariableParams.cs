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

        public override VariableValueTypeEnum ValueTypeEnum => VariableValueTypeEnum.Bool;
    }
}

