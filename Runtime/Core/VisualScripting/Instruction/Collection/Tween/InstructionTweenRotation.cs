using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("TweenRotation")]
    [Category("Tween/补间旋转")]
    [Serializable]
    public class InstructionTweenRotation : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionTweenRotationParams ParamsRaw = new InstructionTweenRotationParams();

        public override IParams Params => ParamsRaw;

        // [ShowInInspector]
        // public override InstructionType InstructionType
        // {
        //     get
        //     {
        //         return ParamsRaw.WaitToComplete ? InstructionType.Coroutine : InstructionType.Immediate;
        //     }
        // }
        public override InstructionType InstructionType => InstructionType.Coroutine;
        private float time = 0;

        protected override IEnumerator Run(Args args)
        {
            var trans = args?.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            if (trans==null)
            {
                yield break;
            }

            while (time<ParamsRaw.Duration)
            {
                time += Time.deltaTime;
                trans.rotation=Quaternion.Slerp(Quaternion.Euler(ParamsRaw.InitRotation),Quaternion.Euler(ParamsRaw.TargetRotation),time/ParamsRaw.Duration);
                
                yield return null;
            }

            if (time > ParamsRaw.Duration)
            {
                time = 0;
            }
        }
        
        public override void RunImmediate(Args args)
        {
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            trans.DOLocalRotate(ParamsRaw.TargetRotation,ParamsRaw.Duration);
        }
    }
}

