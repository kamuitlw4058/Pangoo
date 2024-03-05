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
    public class InstructionUIDialogueParams : InstructionParams
    {
        [JsonMember("DialogueUuid")]
        [ValueDropdown("OnDialogueUuidDropdown")]
        public string DialogueUuid;

        [JsonMember("WaitClosed")]
        public bool WaitClosed;

#if UNITY_EDITOR
        IEnumerable OnDialogueUuidDropdown()
        {
            return  DialogueOverview.GetUuidDropdown();
        }
#endif

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionUIDialogueParams>(val);
            WaitClosed = par.WaitClosed;
            DialogueUuid = par.DialogueUuid;
        }
    }
}