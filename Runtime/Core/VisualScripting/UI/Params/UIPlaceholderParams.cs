using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    [Category("开发/占位UI")]

    public class UIPlaceholderParams : UIPanelParams
    {
        [JsonMember("MainContext")]
        public string MainContext;

        [JsonMember("KeepDuration")]
        public float KeepDuration;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<UIPlaceholderParams>(val);
            MainContext = par.MainContext;
            KeepDuration = par.KeepDuration;
        }
    }
}