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
    [Category("UI/显示UI")]
    [Serializable]
    public class InstructionShowUI : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionUIBoolParams ParamsRaw = new InstructionUIBoolParams();
        public override IParams Params => this.ParamsRaw;

        public bool IsUICloed = false;

        [ShowInInspector]
        public override InstructionType InstructionType
        {
            get
            {
                return ParamsRaw.WaitClosed ? InstructionType.Coroutine : InstructionType.Immediate;
            }
        }

        protected override IEnumerator Run(Args args)
        {
            IsUICloed = false;
            args?.Main?.UI?.ShowUI(ParamsRaw.UIId, () =>
            {
                Debug.Log($"UI Closed");
                IsUICloed = true;
            });
            while (!IsUICloed)
            {
                yield return null;
            }
        }

        public override void RunImmediate(Args args)
        {
            args?.Main?.UI?.ShowUI(ParamsRaw.UIId);
        }

    }
}