using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    [Category("通用/字幕")]

    public class UIPreviewParams : UIPanelParams
    {
        [JsonMember("DragFactorX")]
        public float DragFactorX;

        [JsonMember("DragFactorY")]
        public float DragFactorY;

        public override void Load(string val)
        {
            if (val.IsNullOrWhiteSpace())
            {
                return;
            }
            var par = JsonMapper.ToObject<UIPreviewParams>(val);
            DragFactorX = par.DragFactorX;
            DragFactorY = par.DragFactorY;
        }
    }
}