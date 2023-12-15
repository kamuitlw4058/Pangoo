using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using System;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace Pangoo.Core.VisualScripting
{
    // [Common.Title("PlayTimeline")]
    [Category("动态物体/动态物体开关交互")]
    [Serializable]
    public class InstructionDynamicObjectInteractEnable : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionDynamicObjectBoolParams ParamsRaw = new InstructionDynamicObjectBoolParams();

        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {

            var dynamicObject = args.dynamicObject;
            if (ParamsRaw.DynamicObjectId == 0)
            {
                dynamicObject.InteractEnable = ParamsRaw.Val;
                Debug.Log($"DynamicObject :{dynamicObject},{dynamicObject.Row.Name} Set Interact:{ParamsRaw.Val} ");
            }
            else
            {
                var DynamicObjectService = dynamicObject.DynamicObjectService;
                var targetEntity = DynamicObjectService.GetLoadedEntity(ParamsRaw.DynamicObjectId);
                var targetDynamicObject = targetEntity?.DynamicObj;
                if (targetDynamicObject != null)
                {
                    targetDynamicObject.InteractEnable = ParamsRaw.Val;
                    Debug.Log($"DynamicObjectId:{ParamsRaw.DynamicObjectId}  SetModelActive:{ParamsRaw.Val} ");
                }

            }

        }


    }
}
