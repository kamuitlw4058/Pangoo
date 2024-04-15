using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("DynamicObject/设置动画状态机Float参数")]
    [Serializable]
    public class InstructionDynamicObjectSetAnimatorFloat : Instruction
    {
        public InstructionDynamicObjectSetAnimatorFloatParams ParamsRaw =
            new InstructionDynamicObjectSetAnimatorFloatParams();
        public override IParams Params { get; }

        public override void RunImmediate(Args args)
        {
            if (ParamsRaw.DynamicObjectUuid.IsNullOrWhiteSpace() || ParamsRaw.AnimatorParamName.IsNullOrWhiteSpace()) return;

            var entity = args.Main.DynamicObject.GetLoadedEntity(ParamsRaw.DynamicObjectUuid);
            if (entity != null)
            {
                var animator = entity.DynamicObj.GetComponent<Animator>(ParamsRaw.Path);
                animator?.SetFloat(ParamsRaw.AnimatorParamName, ParamsRaw.Val);
            }
        }
    }
}

