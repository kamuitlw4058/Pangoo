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
    [Category("动态物体/设置动态物体本地Int变量")]
    [Serializable]
    public class InstructionSetLocalIntVariable : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSetLocalIntVariableParams ParamsRaw =
            new InstructionSetLocalIntVariableParams();
        public override IParams Params => this.ParamsRaw;

        public override void RunImmediate(Args args)
        {
            var entity=args.dynamicObject.GetService<DynamicObjectService>().GetLoadedEntity(ParamsRaw.DynamicObjectUuid);
            if (entity!=null)
            {
                Debug.Log($"{ParamsRaw.LocalVariableUuid} <<<o>>>{ParamsRaw.Value}");
                if (entity.DynamicObj!=null && entity.DynamicObj.Variables.KeyValueDict.ContainsKey(ParamsRaw.LocalVariableUuid))
                {
                    entity.DynamicObj?.SetVariable(ParamsRaw.LocalVariableUuid,ParamsRaw.Value);
                }
            }
        }
    }
}

