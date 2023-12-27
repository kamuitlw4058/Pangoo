using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionReplaceSoundParams : InstructionParams
    {
        [JsonMember("OldSoundUuid")]
        [ValueDropdown("OnSoundIdDropdown")]
        public string OldSoundUuid;

        [JsonMember("NewSoundUuid")]
        [ValueDropdown("OnSoundIdDropdown")]
        public string NewSoundUuid;

        [JsonMember("Loop")]
        public bool Loop;


        [JsonMember("FadeTime")]
        public float FadeTime;

        [JsonMember("WaitToComplete")]
        public bool WaitToComplete;

#if UNITY_EDITOR
        IEnumerable OnSoundIdDropdown()
        {
            return SoundOverview.GetUuidDropdown();
        }
#endif
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionReplaceSoundParams>(val);
            OldSoundUuid = par.OldSoundUuid;
            NewSoundUuid = par.NewSoundUuid;
            Loop = par.Loop;
            FadeTime = par.FadeTime;
            WaitToComplete = par.WaitToComplete;


        }
    }
}
