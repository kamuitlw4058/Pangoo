using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("DynamicObject/SetLocalVariable")]
    [Category("动态物体/设置动态物体本地Bool变量")]
    [Serializable]
    public class InstructionSetLocalBoolVariable : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSetLocalBoolVariableParams ParamsRaw =
            new InstructionSetLocalBoolVariableParams();
        public override IParams Params => this.ParamsRaw;

        public override void RunImmediate(Args args)
        {
            Transform trans = InstructionArgsExtension.GetTransformByPath(args, ParamsRaw.DynamicObjectUuid,null);

            if (trans!=null)
            {
                var dynamicObject=trans.GetComponent<EntityDynamicObject>().DynamicObj;
                dynamicObject.SetVariable(ParamsRaw.LocalVariableUuid,ParamsRaw.Value);
            }
        }
    }
}

