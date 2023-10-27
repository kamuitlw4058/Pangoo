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
        public InstructionIntParams m_Params = new InstructionIntParams();

        public InstructionChangeGameSection()
        { }

        protected override IEnumerator Run(Args args)
        {
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            args.dynamicObject.Event.Fire(this, GameSectionChangeEventArgs.Create(m_Params.Val));
            return;
        }

        public override string ParamsString()
        {
            return m_Params.Save();
        }

        public override void LoadParams(string instructionParams)
        {
            m_Params.Load(instructionParams);
        }
        // METHODS: -------------------------------------------------------------------------------

    }
}
