using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.Characters;
using Pangoo.Core.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionSetPlayerDriverInfoParams : InstructionParams
    {
        [JsonMember("DriverInfo")]
        public DriverInfo DriverInfo;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionSetPlayerDriverInfoParams>(val);
            Debug.Log(par.DriverInfo.StepOffset);
            DriverInfo = par.DriverInfo;
        }
    }
}

