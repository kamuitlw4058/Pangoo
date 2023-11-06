using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using GameFramework.Event;
using Pangoo.Core.Common;

namespace Pangoo.Core.VisualScripting
{
    [Category("通用/字幕")]

    public class UISubtitlePanel : UIPanel
    {
        public UISubtitleParams ParamsRaw = new UISubtitleParams();

        protected override IParams Params => ParamsRaw;


        public TextMeshPro m_Text;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_Text = GetComponentInChildren<TextMeshPro>();
            PangooEntry.Event.Subscribe(SubtitleShowEventArgs.EventId, OnSubtitleShowEvent);
        }

        void OnSubtitleShowEvent(object sender, GameEventArgs e)
        {
            SubtitleShowEventArgs args = e as SubtitleShowEventArgs;
            m_Text.text = args.Context;
        }

    }
}