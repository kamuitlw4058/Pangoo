using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Variables/CheckVariableBoolList")]
    [Serializable]
    public class InstructionCheckVariableBoolList : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionCheckVariableBoolListParams ParamsRaw = new InstructionCheckVariableBoolListParams();

        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;

        }

        public override void RunImmediate(Args args)
        {
            for (int i = 0; i < ParamsRaw.VariableUuidList.Count; i++)
            {
                var isUsed = args.dynamicObject.GetVariable<bool>(ParamsRaw.VariableUuidList[i]);
                if (!isUsed)
                {
                    return;
                }
                args.dynamicObject.SetVariable(ParamsRaw.SetVariableUuid, true);
            }
        }
    }
}
