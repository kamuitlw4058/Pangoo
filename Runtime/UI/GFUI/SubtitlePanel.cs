using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using GameFramework.Event;

namespace Pangoo
{

    public class SubtitlePanel : UIFormLogic
    {
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