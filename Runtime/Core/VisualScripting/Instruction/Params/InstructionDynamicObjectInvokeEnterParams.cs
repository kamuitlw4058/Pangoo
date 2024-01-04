using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionDynamicObjectInvokeEnterParams : InstructionParams
    {
        [JsonMember("CanMidwayExit")]
        public bool CanMidwayExit;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionDynamicObjectInvokeEnterParams>(val);
            CanMidwayExit = par.CanMidwayExit;
        }
    }
}
