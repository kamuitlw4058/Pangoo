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
    [Common.Title("PlayTimeline")]
    [Category("Timeline/动态物体播放Timeline")]
    [Serializable]
    public class InstructionDynamicObjectPlayTimeline : Instruction
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
                dynamicObject?.PlayTimeline();
                Debug.Log($"DynamicObject Self PlayTimeline:");
            }
            else
            {
                var DynamicObjectService = dynamicObject.DynamicObjectService;
                var targetEntity = DynamicObjectService.GetLoadedEntity(ParamsRaw.DynamicObjectId);
                targetEntity?.DynamicObj?.PlayTimeline();
                Debug.Log($"DynamicObjectId:{ParamsRaw.DynamicObjectId}  Play Timeline:{ParamsRaw.Val} ");
            }
        }


    }
}
