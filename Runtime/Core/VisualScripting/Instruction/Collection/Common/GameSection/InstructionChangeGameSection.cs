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

    [Category("Common/切换GameSection")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionChangeGameSection : Instruction
    {

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionGameSectionParams ParamsRaw = new InstructionGameSectionParams();
        public override IParams Params => this.ParamsRaw;

        public InstructionChangeGameSection()
        { }

        protected override IEnumerator Run(Args args)
        {
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            if (args.dynamicObject != null)
            {
                args.dynamicObject.Event.Fire(this, GameSectionChangeEventArgs.Create(ParamsRaw.GameSectionId));
            }
            else
            {
                PangooEntry.Event.Fire(this, GameSectionChangeEventArgs.Create(ParamsRaw.GameSectionId));
            }
            return;
        }


    }
}
