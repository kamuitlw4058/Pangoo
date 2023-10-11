using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class ConditionBoolParams : ConditionParams
    {
        [JsonMember("Ok")]
        public bool Ok;


        public override void LoadFromJson(string val)
        {
            var par = JsonMapper.ToObject<ConditionBoolParams>(val);
            Ok = par.Ok;
        }

    }
}