using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionSetGameSectionIntVariableParams : InstructionParams
    {
        [JsonMember("GameSectionUuid")]
        [ValueDropdown("@GameSectionOverview.GetUuidDropdown()")]
        public string GameSectionUuid;

        [JsonMember("VariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(ValueTypeEnum.Int.ToString(),VariableTypeEnum.GameSection.ToString(),false)")]
        public string VariableUuid;

        [JsonMember("Value")]
        public int Value;

    }
}

