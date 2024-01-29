using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("DynamicObject/设置动画状态机bool参数")]
    [Serializable]
    public class InstructionDynamicObjectSetAnimatorBoolParams : Instruction
    {
        public InstructionDynamicObjectSetAnimatorBoolParamsParams ParamsRaw =
            new InstructionDynamicObjectSetAnimatorBoolParamsParams();
        public override IParams Params { get; }
        
        public override void RunImmediate(Args args)
        {
            Transform target=args.dynamicObject.CachedTransfrom.Find(ParamsRaw.TargetPath);
            if (!target.GetComponent<Animator>())
            {
                return;
            }
            Animator animator=target.GetComponent<Animator>();
            animator.SetBool(ParamsRaw.ParamsName,ParamsRaw.Val);
        }
    }
}

