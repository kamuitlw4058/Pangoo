using System;
using LitJson;
using Sirenix.OdinInspector;

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

        [JsonMember("StopDialogueWhenFinish")]
        public bool StopDialogueWhenFinish;

        [JsonMember("ShowCursor")]
        public bool ShowCursor;
    }
}