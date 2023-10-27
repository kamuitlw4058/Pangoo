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

    [Category("Common/开关当前Trigger")]


    [Serializable]
    public class InstructionSetSelfTriggerEnabled : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionBoolParams ParamsRaw = new InstructionBoolParams();
        public override IParams Params => this.ParamsRaw;

        public InstructionSetSelfTriggerEnabled()
        { }

        protected override IEnumerator Run(Args args)
        {

            yield break;

        }

        public override void RunImmediate(Args args)
        {
            Trigger.Enabled = ParamsRaw.Val;
            return;
        }



    }
}
