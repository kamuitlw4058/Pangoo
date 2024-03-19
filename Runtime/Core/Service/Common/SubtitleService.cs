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
    public class SubtitleService : MainSubService
    {
        public override int Priority => 7;


        protected override void DoStart()
        {
            var subtitleUuid = GameMainConfigSrv.GameMainConfig?.DefaultSubtitlePanelUuid;

            if (!subtitleUuid.IsNullOrWhiteSpace())
            {
                UISrv.ShowUI(subtitleUuid);
            }

        }

        public void ShowString(string context, float duration)
        {
            Event.Fire(this, SubtitleShowEventArgs.Create(context, duration));
        }

    }
}