using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.VisualScripting;
using UnityEngine;

public class InstructionSetLocalVector3VariableParams : InstructionSetDynamicObjectVariableParams
{
    [JsonMember("Value")]
    public Vector3 Value;

    public override VariableValueTypeEnum ValueTypeEnum => VariableValueTypeEnum.Vector3;
}

