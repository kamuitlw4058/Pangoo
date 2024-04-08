using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("ChangeTimelineMode")]
    [Category("Timeline/改变Timeline更新模式")]
    [Serializable]
    public class InstructionChangeTimelineUpdateMode : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionChangeTimelineUpdateModeParams ParamsRaw = new InstructionChangeTimelineUpdateModeParams();
        public override IParams Params => ParamsRaw;

        public override void RunImmediate(Args args)
        {
            var timelineHelper = args.dynamicObject.GetComponent<PangooTimelineHelper>(ParamsRaw.Path);
            if (timelineHelper!=null)
            {
                timelineHelper.TimelineOptType = ParamsRaw.TimelineOperationType;
                timelineHelper.Speed = ParamsRaw.Speed;
                switch (timelineHelper.TimelineOptType)
                {
                    case TimelineOperationTypeEnum.Manual:
                        timelineHelper.playableDirector.timeUpdateMode = DirectorUpdateMode.Manual;
                        break;
                    case TimelineOperationTypeEnum.ManualAndUpdate:
                        timelineHelper.playableDirector.timeUpdateMode = DirectorUpdateMode.GameTime;
                        break;
                }
                
            }
            else
            {
                Debug.LogError($"没有在{ParamsRaw.Path}上获取到PangooTimelineHelper");
            }
        }
    }
}

