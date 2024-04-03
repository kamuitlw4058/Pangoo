using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionChangeTimelineMode : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionChangeTimelineModeParams paramsRow => new InstructionChangeTimelineModeParams();
        public override IParams Params => paramsRow;

        public override void RunImmediate(Args args)
        {
            
        }
    }
}

