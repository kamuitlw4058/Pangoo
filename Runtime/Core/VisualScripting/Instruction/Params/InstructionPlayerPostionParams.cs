using System;
using UnityEngine;
using LitJson;
using System.Collections;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionPlayerPostionParams : InstructionParams
    {
        [JsonMember("Id")]
        [ValueDropdown("GetCharacterIds", IsUniqueList = true)]
        public int CharacterId;

        [JsonMember("Position")]
        public Vector3 Position;


        [JsonMember("Rotation")]
        public Vector3 Rotation;


        [JsonMember("Height")]
        public float Height = -1;

        [JsonMember("IsInteractive")]
        public bool IsInteractive = true;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionPlayerPostionParams>(val);
            CharacterId = par.CharacterId;
            Position = par.Position;
            Rotation = par.Rotation;
            Height = par.Height;
            IsInteractive = par.IsInteractive;
        }

#if UNITY_EDITOR
        public IEnumerable GetCharacterIds()
        {
            return GameSupportEditorUtility.GetCharacterIds(onlyPlayer: true, hasDefault: true);
        }
#endif

    }
}