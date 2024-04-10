using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using Pangoo.Common;
using LitJson;
using Pangoo.MetaTable;

namespace Pangoo.Core.Characters
{
    [Serializable]
    public class CharacterBornInfo
    {
        [LabelText("玩家角色")]
        [ValueDropdown("GetCharacterUuid", IsUniqueList = true)]
        [JsonMember("PlayerUuid")]
        public string PlayerUuid;

        [JsonMember("Pose")]
        [LabelText("玩家姿势")]
        public PPose Pose;

        [JsonMember("ForceMove")]
        [LabelText("强制移动")]
        public bool ForceMove;

#if UNITY_EDITOR
        public IEnumerable GetCharacterUuid()
        {
            return CharacterOverview.GetUuidDropdown(AdditionalOptions: new List<Tuple<string, string>>(){
                new Tuple<string, string>("Default",string.Empty),
                new Tuple<string, string>("最后有效玩家",ConstString.LatestPlayer),
            });
        }

#endif
    }
}