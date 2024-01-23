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
    [Category("UI/显示字幕文字")]
    [Serializable]
    public class InstructionShowString : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSubtitleStringParams ParamsRaw = new InstructionSubtitleStringParams();
        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            Debug.Log($"Show String:{ParamsRaw.Context} ,{ParamsRaw.Duration}");
            args?.Main?.Subtitle?.ShowString(ParamsRaw.Context, ParamsRaw.Duration);
        }

    }
}
