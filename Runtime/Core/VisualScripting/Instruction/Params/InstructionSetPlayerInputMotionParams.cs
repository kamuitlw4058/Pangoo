using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.Characters;
using Pangoo.Core.VisualScripting;
using UnityEngine;

[Serializable]
public class InstructionSetPlayerInputMotionParams : InstructionParams
{
    [JsonMember("InputMotionType")]
    public InputMotionType InputMotionType;

    public override void Load(string val)
    {
        var par = JsonMapper.ToObject<InstructionSetPlayerInputMotionParams>(val);
        InputMotionType = par.InputMotionType;
    }
}
