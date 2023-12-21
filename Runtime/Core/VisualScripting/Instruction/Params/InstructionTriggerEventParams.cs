using System;
using UnityEngine;
using LitJson;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{


    [Serializable]
    public class InstructionTriggerEventParams : InstructionParams
    {
        [JsonMember("TriggerEventUuid")]
        public string TriggerEventUuid;

        [JsonMember("Enabled")]
        public bool Enabled;

        [JsonMember("DisableSelfTrigger")]
        [LabelText("关闭当前触发器")]
        public bool DisableSelfTrigger;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionTriggerEventParams>(val);
            TriggerEventUuid = par.TriggerEventUuid;
            Enabled = par.Enabled;
            DisableSelfTrigger = par.DisableSelfTrigger;
        }

    }
}