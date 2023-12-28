using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionUIPreviewParams : InstructionParams
    {
        [JsonMember("PreivewUuid")]
        [ValueDropdown("OnPreviewUuidDropdown")]
        public string PreivewUuid;

        [JsonMember("WaitClosed")]
        public bool WaitClosed;

#if UNITY_EDITOR
        IEnumerable OnPreviewUuidDropdown()
        {
            return DynamicObjectPreviewOverview.GetUuidDropdown();
        }
#endif

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionUIPreviewParams>(val);
            WaitClosed = par.WaitClosed;
            PreivewUuid = par.PreivewUuid;
        }
    }
}