using System;
using System.Collections;
using Pangoo.Core.Common;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]

    // [Title("Change Camera")]
    // [Description("Prints a message to the Unity Console")]

    [Category("Common/等待时间")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionWaitTime : Instruction
    {
        public override InstructionType InstructionType => InstructionType.Coroutine;

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionFloatParams ParamsRaw = new InstructionFloatParams();
        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {
            var timeMode = new TimeMode();
            yield return WaitTime(ParamsRaw.Val1, timeMode);
        }

        public override void RunImmediate(Args args)
        {
            return;
        }



    }
}
