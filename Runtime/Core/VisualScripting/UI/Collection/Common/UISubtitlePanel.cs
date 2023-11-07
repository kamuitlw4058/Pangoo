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


        public TextMeshProUGUI m_Text;


        float m_CurrentTime;

        float m_Duration;

        string m_Context;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_Text = GetComponentInChildren<TextMeshProUGUI>();
            PangooEntry.Event.Subscribe(SubtitleShowEventArgs.EventId, OnSubtitleShowEvent);
        }

        void OnSubtitleShowEvent(object sender, GameEventArgs e)
        {
            SubtitleShowEventArgs args = e as SubtitleShowEventArgs;
            m_Context = args.Context;
            m_CurrentTime = 0;
            m_Duration = args.Duration;
            SetText();
            m_Text.gameObject.SetActive(true);
        }

        void SetText()
        {
            if (m_Text != null)
            {
                if (m_Text.text != m_Context)
                {
                    m_Text.text = m_Context;
                }
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_CurrentTime += elapseSeconds;
            if (m_CurrentTime <= m_Duration)
            {
                SetText();
            }

            if (m_CurrentTime > m_Duration)
            {
                m_Text.gameObject.SetActive(false);
            }

        }

    }
}