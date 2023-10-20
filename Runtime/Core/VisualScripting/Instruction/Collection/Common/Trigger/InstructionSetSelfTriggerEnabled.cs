using System;
using System.Collections;
using System.Threading.Tasks;
using Cinemachine;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]

    // [Title("Change Camera")]
    // [Description("Prints a message to the Unity Console")]

    [Category("Common/SetSelfTriggerEnabled")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionSetSelfTriggerEnabled : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------




        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionSetSelfTriggerEnabled()
        { }

        protected override IEnumerator Run(Args args)
        {
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            return;
        }

        public override string ParamsString()
        {
            return string.Empty;
        }

        public override void LoadParams(string instructionParams)
        {

        }

        // METHODS: -------------------------------------------------------------------------------

    }
}
