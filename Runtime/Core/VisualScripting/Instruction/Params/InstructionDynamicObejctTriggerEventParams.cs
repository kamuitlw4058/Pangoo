using System;
using UnityEngine;
using LitJson;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{


    [Serializable]
    public class InstructionDynamicObejctTriggerEventParams : InstructionParams
    {


        [JsonMember("DynamicObjectId")]
        public int DynamicObjectId;

        [JsonMember("TriggerId")]
        public int TriggerId;

        [JsonMember("Enabled")]
        public bool Enabled;

        [JsonMember("DisableSelfTrigger")]
        [LabelText("关闭当前触发器")]
        public bool DisableSelfTrigger;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionDynamicObejctTriggerEventParams>(val);
            DynamicObjectId = par.DynamicObjectId;
            TriggerId = par.TriggerId;
            Enabled = par.Enabled;
            DisableSelfTrigger = par.DisableSelfTrigger;
        }

    }
}