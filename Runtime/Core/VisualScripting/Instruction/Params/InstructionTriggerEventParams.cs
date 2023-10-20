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
        public int TriggerId;

        public bool Enabled;

        public override void LoadFromJson(string val)
        {
            var par = JsonMapper.ToObject<InstructionTriggerEventParams>(val);
        }

    }
}