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

        [ShowInInspector] public override InstructionType InstructionType => InstructionType.Coroutine;
        private float time = 0;

        protected override IEnumerator Run(Args args)
        {
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            
            while (time<ParamsRaw.Duration)
            {
                time += Time.deltaTime;
                trans.rotation=Quaternion.Slerp(trans.rotation,Quaternion.Euler(ParamsRaw.Rotation),time/ParamsRaw.Duration);
                //yield return WaitTime(Time.deltaTime,new TimeMode());
                yield return null;
            }
        }
    }
}

