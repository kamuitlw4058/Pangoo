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
            Transform target=null;
            Debug.Log("oop:"+ParamsRaw.TargetPath);
            if (ParamsRaw.TargetPath=="Self")
            {
                target=args.Target.transform;
            }
            else
            {
                target=args.dynamicObject.CachedTransfrom.Find(ParamsRaw.TargetPath);
            }
            
            if (!target.GetComponent<MeshRenderer>())
            {
                return;
            }
            MeshRenderer meshRenderer=target.GetComponent<MeshRenderer>();

            meshRenderer.material = args.Target.GetComponent<MaterialList>().materialList[ParamsRaw.Index];
        }
    }
}

