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
    [Category("动态物体/动态物体开关Hotspot")]
    [Serializable]
    public class InstructionDynamicObjectHotspotActive : Instruction
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
            if (ParamsRaw.DynamicObjectUuid.IsNullOrWhiteSpace() || ParamsRaw.DynamicObjectUuid.Equals("Self"))
            {
                dynamicObject.IsHotspotActive = ParamsRaw.Val;
                Debug.Log($"DynamicObject :{dynamicObject},{dynamicObject.Row.Name} Set Self IsHotspotActive:{ParamsRaw.Val} ");
            }
            else
            {
                var DynamicObjectService = dynamicObject.DynamicObjectService;
                var targetEntity = DynamicObjectService.GetLoadedEntity(ParamsRaw.DynamicObjectUuid);
                var targetDynamicObject = targetEntity?.DynamicObj;
                if (targetDynamicObject != null)
                {
                    targetDynamicObject.IsHotspotActive = ParamsRaw.Val;
                    Debug.Log($"DynamicObjectId:{ParamsRaw.DynamicObjectUuid}  SetModelActive:{ParamsRaw.Val} ");
                }

            }

        }


    }
}
