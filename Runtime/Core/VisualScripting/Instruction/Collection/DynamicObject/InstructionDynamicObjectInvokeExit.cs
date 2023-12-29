using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("DynamicObject/调用对象Exit方法")]
    [Serializable]
    public class InstructionDynamicObjectInvokeExit : Instruction
    {
        public override IParams Params { get; }

        public override void RunImmediate(Args args)
        {
            if (args.dynamicObject.immersed != null)
            {
                args.dynamicObject.immersed.OnExit();
            }
        }
    }
}
