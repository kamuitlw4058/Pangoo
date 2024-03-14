using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class ConditionGetKeyParams : ConditionParams
    {
        [JsonMember("KeyCodeParams")]
        public KeyCode KeyCodeParams;

        public Dictionary<string, int> StringMapper = new Dictionary<string, int>()
        {
            {"None",0},
            {"Down",1},
            {"Press",2},
            {"Up",3},
        };

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<ConditionGetKeyParams>(val);
            KeyCodeParams = par.KeyCodeParams;
        }
    }
}

