using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionGameSectionParams : InstructionParams
    {
        [JsonMember("GameSectionUuid")]
        [ValueDropdown("OnGameSectionUuiDropdown")]
        public string GameSectionUuid;
#if UNITY_EDITOR
        IEnumerable OnGameSectionUuiDropdown()
        {
            return GameSectionOverview.GetUuidDropdown();
        }
#endif
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionGameSectionParams>(val);
            GameSectionUuid = par.GameSectionUuid;
        }
    }
}