using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("DynamicObject/调用对象Enter方法")]
    [Serializable]
    public class InstructionDynamicObjectInvokeEnter : Instruction
    {
        public override IParams Params { get; }

        public override void RunImmediate(Args args)
        {
            if (args.dynamicObject.immersed!=null)
            {
                args.dynamicObject.immersed.OnEnter();
            }
        }
    }
}

