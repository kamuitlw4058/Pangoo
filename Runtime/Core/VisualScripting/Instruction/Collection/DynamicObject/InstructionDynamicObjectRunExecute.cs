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
    [Category("动态物体/运行触发")]
    [Serializable]
    public class InstructionDynamicObjectRunExecute : Instruction
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
                if (ParamsRaw.Val)
                {
                    dynamicObject.StartExecuteEvent();
                }
                else
                {
                    dynamicObject.StopExecuteEvent();
                }

                // Debug.Log($"DynamicObject Set Self ModelActive:{ParamsRaw.Val} ");
            }
            else
            {
                var DynamicObjectService = dynamicObject.DynamicObjectService;
                var targetEntity = DynamicObjectService.GetLoadedEntity(ParamsRaw.DynamicObjectId);
                if (ParamsRaw.Val)
                {
                    targetEntity?.DynamicObj?.StartExecuteEvent();
                }
                else
                {
                    targetEntity?.DynamicObj?.StopExecuteEvent();
                }
            }

        }


    }
}
