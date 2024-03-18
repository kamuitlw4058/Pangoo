using System;
using UnityEngine;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionPlayerPostionParams : InstructionParams
    {
        [JsonMember("CharacterUuid")]
        [ValueDropdown("GetCharacterUuid", IsUniqueList = true)]
        public string CharacterUuid;

        [JsonMember("Position")]
        public Vector3 Position;


        [JsonMember("Rotation")]
        public Vector3 Rotation;


        [JsonMember("CameraHeight")]
        [LabelText("相机高度")]
        public float CameraHeight = ConstFloat.InvaildCameraHeight;

        [JsonMember("ColliderHeight")]
        [LabelText("碰撞高度")]
        public float ColliderHeight = 0;

        [JsonMember("IsInteractive")]
        public bool IsInteractive = true;

        [JsonMember("NotMoveWhenPlayerCreated")]
        [LabelText("不移动玩家，当玩家已经被创建")]
        public bool NotMoveWhenPlayerCreated;


#if UNITY_EDITOR
        public IEnumerable GetCharacterUuid()
        {
            return CharacterOverview.GetUuidDropdown(AdditionalOptions: new List<Tuple<string, string>>(){
                new Tuple<string, string>("Default",string.Empty),
            });
        }
#endif

    }
}