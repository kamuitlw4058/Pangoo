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
        [JsonMember("TriggerId")]
        public int TriggerId;

        [JsonMember("Enabled")]
        public bool Enabled;

        [JsonMember("DisableSelfTrigger")]
        [LabelText("关闭当前触发器")]
        public bool DisableSelfTrigger;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionTriggerEventParams>(val);
            TriggerId = par.TriggerId;
            Enabled = par.Enabled;
            DisableSelfTrigger = par.DisableSelfTrigger;
        }

    }
}