using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using Pangoo.Common;

namespace Pangoo.Core.VisualScripting
{
    // [Common.Title("ChangeTimelineMode")]
    [Category("Timeline/通过MakerTime去改变Timeline的时间")]
    [Serializable]
    public class InstructionTimelineSetTimeByMarkerTime : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionPathParams ParamsRaw = new InstructionPathParams();
        public override IParams Params => ParamsRaw;

        public override void RunImmediate(Args args)
        {
            var timelineHelper = args.dynamicObject?.GetComponent<PangooTimelineHelper>(ParamsRaw.Path);
            if (timelineHelper != null)
            {
                Debug.Log($"Set Timeline Marker Time:{args.MarkerTime} ");
                if (args.MarkerTime >= 0 && timelineHelper.playableDirector != null)
                {

                    timelineHelper.playableDirector.time = args.MarkerTime;
                    timelineHelper.playableDirector.UpdateManuel();
                }
            }
            else
            {
                if (args.dynamicObject == null)
                {
                    Debug.LogError($"InstructionTimelineSetTimeByMarkerTime 输入没有传入dynamicObject ");
                }
                else
                {
                    Debug.LogError($"没有在{ParamsRaw.Path} 上获取到PangooTimelineHelper ");
                }


            }
        }
    }
}

