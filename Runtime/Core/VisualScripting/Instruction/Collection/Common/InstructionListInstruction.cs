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

    [Category("Common/指令列表")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionListInstruction : Instruction
    {
        public override InstructionType InstructionType => InstructionType.Coroutine;

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionListParams ParamsRaw = new InstructionListParams();
        public override IParams Params => this.ParamsRaw;

        [HideInEditorMode]
        public InstructionList InstructionList;

        protected override IEnumerator Run(Args args)
        {
            var instructionHandler = args.Main.GetInstructionRowByUuidHandler();
            InstructionList = ParamsRaw.Instructions.ToInstructionList(instructionHandler);
            InstructionList.Start(args);
            while (InstructionList.IsRunning)
            {
                InstructionList.OnUpdate();
                // Debug.Log($"Instructions:{InstructionList.IsRunning}");
                yield return null;
            }

        }

        public override void RunImmediate(Args args)
        {
            return;
        }



    }
}
