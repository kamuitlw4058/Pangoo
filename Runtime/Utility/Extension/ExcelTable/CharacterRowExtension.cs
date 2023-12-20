using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using Pangoo.MetaTable;

namespace Pangoo
{

    public static class CharacterRowExtension
    {

        public static ICharacterRow ToInterface(this CharacterTable.CharacterRow row)
        {
            var json = LitJson.JsonMapper.ToJson(row);
            return LitJson.JsonMapper.ToObject<Pangoo.MetaTable.CharacterRow>(json);
        }

    }
}
