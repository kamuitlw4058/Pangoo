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
    [Category("开发/占位UI")]
    public class UIPlaceholderPanel : UIPanel
    {
        public UIPlaceholderParams ParamsRaw = new UIPlaceholderParams();

        protected override IParams Params => ParamsRaw;
        public TextMeshPro m_Text;

        public float m_StartTime;
        public float m_CurrentTime;

        public float m_Duration;

        public bool m_IsShowing;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            ParamsRaw = userData as UIPlaceholderParams;
            if (ParamsRaw == null)
            {
                return;
            }

            m_Text = GetComponentInChildren<TextMeshPro>();
            m_Text.text = ParamsRaw.MainContext;
            m_Duration = ParamsRaw.KeepDuration;
            m_StartTime = Time.time;
            m_CurrentTime = m_StartTime;
        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_CurrentTime += elapseSeconds;
            if (m_CurrentTime > (m_StartTime + m_Duration))
            {
                CloseSelf();
            }
        }

    }
}