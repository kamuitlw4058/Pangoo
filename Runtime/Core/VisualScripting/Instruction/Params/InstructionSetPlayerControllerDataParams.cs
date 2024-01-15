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
    public class InstructionSetPlayerControllerDataParams : InstructionParams
    {
        [JsonMember("DriverInfo")]
        public DriverInfo DriverInfo;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionSetPlayerControllerDataParams>(val);
            DriverInfo = par.DriverInfo;
        }
    }
}

