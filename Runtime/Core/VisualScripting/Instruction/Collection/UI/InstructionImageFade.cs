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
            Image image = args.Target.GetComponent<Image>();
            image.DOFade(ParamsRaw.AlphaValue,ParamsRaw.TweenTime).SetId(ParamsRaw.TweenID);
            Debug.Log("FadeRun");
            return null;
        }

        public override void RunImmediate(Args args)
        {
            Image iamge = args.Target.GetComponent<Image>();
            iamge.DOFade(ParamsRaw.AlphaValue,ParamsRaw.TweenTime).SetId(ParamsRaw.TweenID);
            Debug.Log("FadeRunImmediate");
        }
    }
}
