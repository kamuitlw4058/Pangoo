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
    [Category("动态物体/运行外部指令")]
    [Serializable]
    public class InstructionDynamicObjectRunExternalInstruction : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionDynamicObjectRunExternalInstructionParams ParamsRaw = new InstructionDynamicObjectRunExternalInstructionParams();

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
                dynamicObject?.StartExternalInstruction(ParamsRaw.InstruciontId, ParamsRaw.Targets.ToListString());
            }
            else
            {
                var DynamicObjectService = dynamicObject.DynamicObjectService;
                var targetEntity = DynamicObjectService.GetLoadedEntity(ParamsRaw.DynamicObjectId);
                targetEntity?.DynamicObj?.StartExternalInstruction(ParamsRaw.InstruciontId, ParamsRaw.Targets.ToListString());
            }

        }


    }
}
