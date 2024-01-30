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
    public class ConditionCheckForPresenceInTheSectionListParams : ConditionParams
    {
        [JsonMember("GameSectionUuidList")]
        [ValueDropdown("OnGameSectionUuidValueDropdown")]
        [LabelText("包含的章节段落")]
        public List<string> GameSectionUuidList=new List<string>();

        [ValueDropdown("OnGameSectionUuidValueDropdown")]
        [LabelText("当前的章节段落")]
        [ReadOnly]
        public string CurrentGameSection;

        
#if UNITY_EDITOR
        IEnumerable OnGameSectionUuidValueDropdown()
        {
            return GameSectionOverview.GetUuidDropdown();
        }
#endif
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<ConditionCheckForPresenceInTheSectionListParams>(val);
            GameSectionUuidList = par.GameSectionUuidList;
        }
    }
}

