using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionPlaySoundParams : InstructionParams
    {
        [JsonMember("SoundUuid")]
        [ValueDropdown("OnSoundUuidDropdown")]

        public string SoundUuid;

        [JsonMember("FadeTime")]
        public float FadeTime;

        [JsonMember("Loop")]
        public bool Loop;

        [JsonMember("WaitToComplete")]
        public bool WaitToComplete;

#if UNITY_EDITOR
        IEnumerable OnSoundUuidDropdown()
        {
            return SoundOverview.GetUuidDropdown();
        }
#endif
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionPlaySoundParams>(val);
            WaitToComplete = par.WaitToComplete;
            SoundUuid = par.SoundUuid;
            Loop = par.Loop;
            FadeTime = par.FadeTime;

        }
    }
}
