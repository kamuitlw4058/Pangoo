using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionCyclePlaySoundParams : InstructionParams
    {
        [JsonMember("SoundUuid")]
        [ValueDropdown("@SoundOverview.GetUuidDropdown()")]
        public string SoundUuid;

        [JsonMember("CurrentTimeVariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Float.ToString())")]
        public string CurrentTimeVariableUuid;
        
        [JsonMember("CycleTime")]
        public float CycleTime;

        [LabelText("是否在开始时播放一次")]
        [JsonMember("OnStartPlay")]
        public bool OnStartPlay;

        [JsonMember("StartPlayFlagVariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString())")]
        public string StartPlayFlagVariableUuid;
    }
}

