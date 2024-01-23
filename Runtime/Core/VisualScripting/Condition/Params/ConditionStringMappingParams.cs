using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class ConditionStringMappingParams : ConditionParams
    {
        [JsonMember("StringMapper")]
        public Dictionary<string, int> StringMapper = new Dictionary<string, int>();

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<ConditionStringMappingParams>(val);
            StringMapper = par.StringMapper;
        }

    }
}