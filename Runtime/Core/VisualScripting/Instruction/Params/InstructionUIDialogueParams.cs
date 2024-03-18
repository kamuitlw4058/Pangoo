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
        [ValueDropdown("DialogueOverview.GetUuidDropdown()")]
        public string DialogueUuid;

        [JsonMember("WaitClosed")]
        public bool WaitClosed;

        [JsonMember("DontControllPlayer")]
        public bool DontControllPlayer;
    }
}