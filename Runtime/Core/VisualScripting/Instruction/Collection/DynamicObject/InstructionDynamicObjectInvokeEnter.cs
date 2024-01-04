using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("DynamicObject/调用对象Enter方法")]
    [Serializable]
    public class InstructionDynamicObjectInvokeEnter : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionDynamicObjectInvokeEnterParams ParamsRaw = new InstructionDynamicObjectInvokeEnterParams();
        public override IParams Params { get; }
        
        public override  InstructionType InstructionType => InstructionType.Coroutine;  

        protected override IEnumerator Run(Args args)
        {
            Debug.Log("协程指令");
            if (args.dynamicObject.immersed == null)
            {
                Debug.Log("没有获取到接口");
                yield break;
            }
            if (args.dynamicObject.immersed!=null)
            {
                args.dynamicObject.immersed.OnEnter();
            }
            
            while (args.dynamicObject.immersed.IsRunning)
            {
                yield return null;
            }
        }

        public override void RunImmediate(Args args)
        {
            if (args.dynamicObject.immersed == null)
                return;
            
            if (args.dynamicObject.immersed!=null)
            {
                args.dynamicObject.immersed.CanMidwayExit = ParamsRaw.CanMidwayExit;
                args.dynamicObject.immersed.OnEnter();
            }
            Debug.Log("直接指令");
        }
    }
}

