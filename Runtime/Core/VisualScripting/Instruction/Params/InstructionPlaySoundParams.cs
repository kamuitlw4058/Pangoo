using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionPlaySoundParams : InstructionParams
    {
        [JsonMember("SoundId")]
        [ValueDropdown("OnSoundIdDropdown")]

        public int SoundId;

        [JsonMember("Loop")]
        public bool Loop;

        [JsonMember("WaitToComplete")]
        public bool WaitToComplete;

#if UNITY_EDITOR
        IEnumerable OnSoundIdDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<SoundTableOverview>();
        }
#endif
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionPlaySoundParams>(val);
            WaitToComplete = par.WaitToComplete;
            SoundId = par.SoundId;
            Loop = par.Loop;

        }
    }
}
