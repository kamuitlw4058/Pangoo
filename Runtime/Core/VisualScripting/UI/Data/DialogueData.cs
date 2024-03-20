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

    [Serializable]

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


        [ListDrawerSettings(ShowFoldout = false)]
        public List<DialogueUpdateData> dialogueUpdateDatas = new List<DialogueUpdateData>();


    }
}