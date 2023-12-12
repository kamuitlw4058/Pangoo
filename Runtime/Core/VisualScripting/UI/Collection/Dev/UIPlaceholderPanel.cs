using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using GameFramework.Event;
using Pangoo.Core.Common;
using GameFramework;


namespace Pangoo.Core.VisualScripting
{
    [Category("开发/占位UI")]
    public class UIPlaceholderPanel : UIPanel
    {
        public UIPlaceholderParams ParamsRaw = new UIPlaceholderParams();

        protected override IParams Params => ParamsRaw;
        public TextMeshProUGUI m_Text;

        public float m_StartTime;
        public float m_CurrentTime;

        public float m_Duration;

        public bool m_IsShowing;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_Text = GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log($"OnOpen m_Text:{m_Text}");
            if (m_Text != null)
            {
                m_Text.text = ParamsRaw.MainContext;
            }
            m_Duration = ParamsRaw.KeepDuration;
            m_CurrentTime = 0;
        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_CurrentTime += elapseSeconds;
            if (m_CurrentTime > m_Duration)
            {
                CloseSelf();
            }
            // Debug.Log($"m_Text:{m_Text}");
            if (m_Text != null)
            {
                m_Text.text = GameFramework.Utility.Text.Format("{0} {1}...", ParamsRaw.MainContext, (int)(m_Duration - m_CurrentTime));
            }
        }

    }
}