using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Pangoo.Core.Services;
using Pangoo.MetaTable;
using Pangoo.Common;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{


    public class DialogueData
    {
        public Args args;

        public UIService UIService;

        [ShowInInspector]
        public IDialogueRow DialogueRow;


        public bool WaitClosed;


        public bool DontControllPlayer;

        public bool ShowCursor;


        public bool StopDialogueWhenFinish;


        public Action FinishAction;


        public List<DialogueUpdateData> dialogueUpdateDatas = new List<DialogueUpdateData>();


    }
}