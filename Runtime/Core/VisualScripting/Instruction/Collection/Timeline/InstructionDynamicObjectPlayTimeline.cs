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
        public InstructionIntParams ParamsRaw = new InstructionIntParams();

        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {

            var dynamicObject = args.dynamicObject;
            var DynamicObjectService = dynamicObject.DynamicObjectService;

            var targetEntity = DynamicObjectService.GetLoadedEntity(ParamsRaw.Val);
            targetEntity?.DynamicObj?.PlayTimeline();
            Debug.Log($"Play :{ParamsRaw.Val} Timeline");
        }


    }
}
