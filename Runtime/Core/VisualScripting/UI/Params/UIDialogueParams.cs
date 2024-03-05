using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    [Category("通用/对话")]

    public class UIDialogueParams : UIPanelParams
    {

        public override void Load(string val)
        {
            if (val.IsNullOrWhiteSpace())
            {
                return;
            }
            var par = JsonMapper.ToObject<UIPreviewParams>(val);

        }
    }
}