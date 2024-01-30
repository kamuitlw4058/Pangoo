using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class ConditionCheckForPresenceInTheSectionListParams : ConditionParams
    {
        [FormerlySerializedAs("GameSectionUuidList")]
        [JsonMember("GameSectionUuidList")]
        [ValueDropdown("OnGameSectionUuidValueDropdown")]
        [LabelText("包含的章节段落")]
        public string[] GameSectionUuidArray = Array.Empty<string>();

#if UNITY_EDITOR
        IEnumerable OnGameSectionUuidValueDropdown()
        {
            return GameSectionOverview.GetUuidDropdown();
        }
#endif
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<ConditionCheckForPresenceInTheSectionListParams>(val);
            GameSectionUuidArray = par.GameSectionUuidArray;
        }
    }
}

