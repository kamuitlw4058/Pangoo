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


        [JsonMember("Height")]
        public float Height = -1;

        [JsonMember("IsInteractive")]
        public bool IsInteractive = true;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionPlayerPostionParams>(val);
            CharacterUuid = par.CharacterUuid;
            Position = par.Position;
            Rotation = par.Rotation;
            Height = par.Height;
            IsInteractive = par.IsInteractive;
        }

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