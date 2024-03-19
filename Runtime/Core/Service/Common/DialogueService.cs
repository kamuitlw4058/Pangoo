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
    public class DialogueService : MainSubService
    {
        public override int Priority => 7;

        public UIDialoguePanel Panel;


        protected override void DoStart()
        {
            base.DoAwake();
            var dialogueUuid = GameMainConfigSrv.GetGameMainConfig().DialoguePanelUuid;
            if (!dialogueUuid.IsNullOrWhiteSpace())
            {
                UISrv.ShowUI(dialogueUuid, showAction: (o) =>
                {
                    Panel = o as UIDialoguePanel;
                });
            }

        }

        public void InsertDialogue(DialogueData dialogueData)
        {
            if (Panel != null)
            {
                Panel.InsertDialogue(dialogueData);
            }
        }

    }
}