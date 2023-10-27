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

    [Category("Common/SetSelfTriggerEnabled")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionSetDynamicObejctTriggerEnabled : Instruction
    {
        public override InstructionType InstructionType => InstructionType.Coroutine;

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionTriggerEventParams m_Params = new InstructionTriggerEventParams();




        public InstructionSetDynamicObejctTriggerEnabled()
        { }

        protected override IEnumerator Run(Args args)
        {
            if (m_Params.DisableSelfTrigger && Trigger != null)
            {
                Trigger.Enabled = false;
            }
            yield return null;
            args.dynamicObject?.SetTriggerEnabled(m_Params.TriggerId, m_Params.Enabled);
            Debug.Log($"args.{args.Self},{args.dynamicObject},{Trigger}");
        }

        public override void RunImmediate(Args args)
        {
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