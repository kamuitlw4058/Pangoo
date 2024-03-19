using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("设置Int增量")]
    [Category("Variables/设置Int增量")]
    public class InstructionSetVariableIntDelta : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSetVariableIntDeltaParams ParamsRaw = new InstructionSetVariableIntDeltaParams();

        public override IParams Params => this.ParamsRaw;
        int variable = 0;
        
        public override void RunImmediate(Args args)
        {
            var variableType = args?.Main.RuntimeData.GetVariableType(ParamsRaw.VariableUuid);
            if (variableType!=null)
            {
                variable=(int)args?.Main.RuntimeData.GetVariable<int>(ParamsRaw.VariableUuid);
                Debug.Log($"1>原始Int变量值:{variable}");
            }
            else
            {
                variable = (int)args?.dynamicObject.GetVariable<int>(ParamsRaw.VariableUuid);
                Debug.Log($"0>原始Int变量值:{variable}");
            }
            
            variable += ParamsRaw.DeltaValue;
            Debug.Log($"当前Int变量值:{variable} : {ParamsRaw.DeltaValue}");
            args.dynamicObject.SetVariable<int>(ParamsRaw.VariableUuid, variable);
        }
    }
}

