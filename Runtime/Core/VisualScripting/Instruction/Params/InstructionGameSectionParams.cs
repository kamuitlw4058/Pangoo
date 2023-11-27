using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionGameSectionParams : InstructionParams
    {
        [JsonMember("GameSectionId")]
        [ValueDropdown("OnGameSectionIdDropdown")]
        public int GameSectionId;
#if UNITY_EDITOR
        IEnumerable OnGameSectionIdDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<GameSectionTableOverview>();
        }
#endif
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionGameSectionParams>(val);
            GameSectionId = par.GameSectionId;
        }
    }
}