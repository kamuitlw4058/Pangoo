using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("DynamicObject/SetLocalVariable")]
    [Category("动态物体/设置动态物体本地Vector3变量")]
    [Serializable]
    public class InstructionSetLocalVector3Variable : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSetLocalVector3VariableParams ParamsRaw =
            new InstructionSetLocalVector3VariableParams();
        public override IParams Params => this.ParamsRaw;

        public override void RunImmediate(Args args)
        {
            var entity = args.dynamicObject.DynamicObjectService.GetLoadedEntity(ParamsRaw.DynamicObjectUuid);
            
            if (entity.DynamicObj != null)
            {
                entity.DynamicObj.Main.RuntimeData.SetDynamicObjectVariable<Vector3>(ParamsRaw.DynamicObjectUuid,ParamsRaw.LocalVariableUuid, ParamsRaw.Value);
            }
        }
    }
}
