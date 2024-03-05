using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    [Category("通用/主菜单")]

    public class UIMainParams : UIPanelParams
    {
        [JsonMember("DragFactorX")]
        public float DragFactorX;

        [JsonMember("DragFactorY")]
        public float DragFactorY;


    }
}