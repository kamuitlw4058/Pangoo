using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionReplaceSoundParams : InstructionParams
    {
        [JsonMember("OldSoundId")]
        [ValueDropdown("OnSoundIdDropdown")]
        public int OldSoundId;

        [JsonMember("NewSoundId")]
        [ValueDropdown("OnSoundIdDropdown")]
        public int NewSoundId;

        [JsonMember("Loop")]
        public bool Loop;


        [JsonMember("FadeTime")]
        public float FadeTime;

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
            var par = JsonMapper.ToObject<InstructionReplaceSoundParams>(val);
            OldSoundId = par.OldSoundId;
            NewSoundId = par.NewSoundId;
            Loop = par.Loop;
            FadeTime = par.FadeTime;
            WaitToComplete = par.WaitToComplete;


        }
    }
}
