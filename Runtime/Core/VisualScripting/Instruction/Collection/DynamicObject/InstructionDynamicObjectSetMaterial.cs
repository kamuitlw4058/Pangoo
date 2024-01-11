using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("DynamicObject/SetMaterial")]
    [Serializable]
    public class InstructionDynamicObjectSetMaterial : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionDynamicObjectSetMaterialParams ParamsRaw = new InstructionDynamicObjectSetMaterialParams();
        public override IParams Params { get; }

        public override void RunImmediate(Args args)
        {
            if (!args.dynamicObject.Entity.GetComponent<MaterialList>())
            {
                return;
            }
            
            Transform target= args.dynamicObject.GetSubGameObjectTransformPath(ParamsRaw.TargetPath);
            args.dynamicObject.Entity.GetComponent<MaterialList>().SetModelMaterial(target,ParamsRaw.Index);
        }
    }
}

