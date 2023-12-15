using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Pangoo.Core.VisualScripting
{
    [Category("UI/ImageFade")]
    [Serializable]
    public class InstructionImageFade : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionImageFadeParams ParamsRaw = new InstructionImageFadeParams();
        public override IParams Params => this.ParamsRaw;
        
        
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
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            Transform target=args.dynamicObject.CachedTransfrom.Find(ParamsRaw.TargetName);
            if (!target.GetComponent<Image>())
            {
                return;
            }
            Image image=target.GetComponent<Image>();
            image.DOFade(ParamsRaw.AlphaValue,ParamsRaw.TweenTime).SetId(ParamsRaw.TweenID);
        }
    }
}
