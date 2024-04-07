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
        public InstructionChangeTimelineUpdateModeParams ParamsRaw => new InstructionChangeTimelineUpdateModeParams();
        public override IParams Params => ParamsRaw;

        public override void RunImmediate(Args args)
        {
            var timelineHelper = args.dynamicObject.GetComponent<PangooTimelineHelper>(ParamsRaw.Path);
            timelineHelper.TimelineOptType = ParamsRaw.TimelineOperationType;
            timelineHelper.Speed = ParamsRaw.Speed;
        }
    }
}

