using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Tween/KillID")]
    [Serializable]
    public class InstructionDoTweenKill : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionTweenKillParams ParamsRaw = new InstructionTweenKillParams();
        public override IParams Params => ParamsRaw;
        
        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            DOTween.Kill(ParamsRaw.TweenID);
        }
    }
}
