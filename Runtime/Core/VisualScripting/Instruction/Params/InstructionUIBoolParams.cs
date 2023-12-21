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
    public class InstructionUIBoolParams : InstructionParams
    {
        [JsonMember("UIUuid")]
        [ValueDropdown("OnUIUuidDropdown")]
        public string UIUuid;

        [JsonMember("WaitClosed")]
        public bool WaitClosed;

#if UNITY_EDITOR
        IEnumerable OnUIUuidDropdown()
        {
            return SimpleUIOverview.GetUuidDropdown();
        }
#endif

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionUIBoolParams>(val);
            WaitClosed = par.WaitClosed;
            UIUuid = par.UIUuid;

        }
    }
}