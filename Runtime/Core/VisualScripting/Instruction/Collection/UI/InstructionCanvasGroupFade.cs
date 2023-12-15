using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("UI/CanvasGroupFade")]
    [Serializable]
    public class InstructionCanvasGroupFade : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionCanvasGroupFadeParams ParamsRaw = new InstructionCanvasGroupFadeParams();

        public override IParams Params => ParamsRaw;
        [ShowInInspector]
        public override InstructionType InstructionType
        {
            get
            {
                return ParamsRaw.WaitFinsh ? InstructionType.Coroutine : InstructionType.Immediate;
            }
        }
        
        protected override IEnumerator Run(Args args)
        {
            Transform target=args.dynamicObject.CachedTransfrom.Find(ParamsRaw.TargetName);
            if (!target.GetComponent<CanvasGroup>())
            {
                return null;
            }
            CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, ParamsRaw.AlphaValue, ParamsRaw.TweenTime);
            return null;
        }

        public override void RunImmediate(Args args)
        {
            Transform target=args.dynamicObject.CachedTransfrom.Find(ParamsRaw.TargetName);
            if (!target.GetComponent<CanvasGroup>())
            {
                return;
            }
            CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, ParamsRaw.AlphaValue, ParamsRaw.TweenTime);
        }
    }
}
