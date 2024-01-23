using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    [Category("通用/字幕")]

    public class UISubtitleParams : UIPanelParams
    {
        [JsonMember("Message")]
        public string Message;

        [JsonMember("ShowTrigger")]
        public bool ShowTriggerRow;

        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<UISubtitleParams>(val);
            Message = par.Message;
            ShowTriggerRow = par.ShowTriggerRow;
        }
    }
}