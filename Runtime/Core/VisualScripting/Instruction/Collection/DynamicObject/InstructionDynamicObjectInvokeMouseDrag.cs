using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("DynamicObject/调用对象MouseDrag方法")]
    [Serializable]
    public class InstructionDynamicObjectInvokeMouseDrag : Instruction
    {
        public override IParams Params { get; }
        public override void RunImmediate(Args args)
        {
            if (args.dynamicObject.immersed == null)
                return;
            
            if (args.dynamicObject.immersed!=null)
            {
                var mouseImmersed = (MouseImmersed)args.dynamicObject.immersed;
                mouseImmersed.OnExtraMouseDrag();
            }
            Debug.Log("调用对象MouseDrag方法");
        }
    }
}
