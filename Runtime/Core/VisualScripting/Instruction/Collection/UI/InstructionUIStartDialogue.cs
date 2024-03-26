using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using System;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace Pangoo.Core.VisualScripting
{
    // [Common.Title("PlayTimeline")]
    [Category("UI/开始对话流程")]
    [Serializable]
    public class InstructionUIStartDialogue : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionUIDialogueParams ParamsRaw = new InstructionUIDialogueParams();
        public override IParams Params => this.ParamsRaw;

        [HideInEditorMode]
        public bool IsUICloed = false;

        [ShowInInspector]
        public override InstructionType InstructionType
        {
            get
            {
                return ParamsRaw.WaitClosed ? InstructionType.Coroutine : InstructionType.Immediate;
            }
        }

        DialogueData BuildDialogueData(Args args, Action closeAction = null)
        {
            var ret = new DialogueData();
            ret.args = args.Clone;
            ret.FinishAction = closeAction;
            ret.DontControllPlayer = ParamsRaw.DontControllPlayer;
            ret.WaitClosed = ParamsRaw.WaitClosed;
            ret.ShowCursor = ParamsRaw.ShowCursor;
            ret.StopDialogueWhenFinish = ParamsRaw.StopDialogueWhenFinish;
            ret.DialogueRow = args.Main.MetaTable.GetDialogueByUuid(ParamsRaw.DialogueUuid);

            return ret;
        }


        protected override IEnumerator Run(Args args)
        {
            if (args.dynamicObject == null)
            {
                Debug.LogError($"Preview DynamicObject Is Failed! DynamicObject is null");
                yield break;
            }

            IsUICloed = false;
            args?.Main?.Dialogue.InsertDialogue(BuildDialogueData(args, () =>
            {
                IsUICloed = true;
            }));
            while (!IsUICloed)
            {
                yield return null;
            }
        }

        public override void RunImmediate(Args args)
        {
            if (args.dynamicObject == null)
            {
                Debug.LogError($"Preview DynamicObject Is Failed! DynamicObject is null");
            }

            args?.Main?.Dialogue.InsertDialogue(BuildDialogueData(args));
        }
    }
}