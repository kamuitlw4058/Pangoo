using Pangoo;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using System;
using UnityGameFramework.Runtime;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.Core.VisualScripting;

namespace Pangoo.Core.Services
{
    public class SubtitleService : BaseService
    {
        public override int Priority => 7;

        UIService m_UIService;
        GameMainConfigService m_GameMainConfigService;




        protected override void DoAwake()
        {
            base.DoAwake();
            m_UIService = Parent.GetService<UIService>();
            m_GameMainConfigService = Parent.GetService<GameMainConfigService>();
        }


        protected override void DoStart()
        {
            base.DoAwake();
            var subtitleId = m_GameMainConfigService?.GetGameMainConfig()?.DefaultSubtitlePanelId ?? 0;

            if (subtitleId != 0)
            {
                m_UIService.ShowUI(subtitleId);
            }

        }

        public void ShowString(string context, float duration)
        {
            Event.Fire(this, SubtitleShowEventArgs.Create(context, duration));
        }

    }
}