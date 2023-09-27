using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionMessageParams : InstructionParams
    {
        [JsonMember("Message")]
        public string Message;

        [JsonMember("ShowTrigger")]
        public bool ShowTriggerRow;

        public override void LoadFromJson(string val)
        {
            var par = JsonMapper.ToObject<InstructionMessageParams>(val);
            Message = par.Message;
            ShowTriggerRow = par.ShowTriggerRow;
        }
    }
}