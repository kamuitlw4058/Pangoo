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
    public class CaseService : MainSubService
    {
        public override int Priority => 7;

        public UICasePanel Panel;
        protected override void DoStart()
        {
            var panelUuid = GameMainConfigSrv.GameMainConfig?.CasePanelUuid;

            if (!panelUuid.IsNullOrWhiteSpace())
            {
                UISrv.ShowUI(panelUuid, showAction: (o) =>
                {
                    Panel = o as UICasePanel;
                });
            }
        }


    }
}