using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionContinuousMessageParams : InstructionMessageParams
    {

        [JsonMember("SecoundCount")]
        public int SecoundCount;


        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionContinuousMessageParams>(val);
            Message = par.Message;
            ShowTriggerRow = par.ShowTriggerRow;
            SecoundCount = par.SecoundCount;
        }
    }
}