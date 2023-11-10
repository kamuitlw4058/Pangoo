using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionStopSoundParams : InstructionParams
    {
        [JsonMember("SoundId")]
        [ValueDropdown("OnSoundIdDropdown")]

        public int SoundId;


#if UNITY_EDITOR
        IEnumerable OnSoundIdDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<SoundTableOverview>();
        }
#endif
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionStopSoundParams>(val);
            SoundId = par.SoundId;

        }
    }
}
