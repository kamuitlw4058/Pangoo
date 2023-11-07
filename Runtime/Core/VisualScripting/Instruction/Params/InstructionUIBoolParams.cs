using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionUIBoolParams : InstructionParams
    {
        [JsonMember("UIId")]
        [ValueDropdown("OnUIIdDropdown")]
        public int UIId;

        [JsonMember("WaitClosed")]
        public bool WaitClosed;

#if UNITY_EDITOR
        IEnumerable OnUIIdDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<SimpleUITableOverview>();
        }
#endif

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionUIBoolParams>(val);
            WaitClosed = par.WaitClosed;
            UIId = par.UIId;

        }
    }
}