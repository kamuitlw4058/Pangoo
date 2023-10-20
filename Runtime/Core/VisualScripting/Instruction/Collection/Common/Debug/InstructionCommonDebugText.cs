using System;
using System.Collections;
using System.Threading.Tasks;
using GameFramework;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework;

namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]

    [Common.Title("Debug Text")]
    // [Description("Prints a message to the Unity Console")]

    [Category("Debug/Log Text")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionCommonDebugText : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionMessageParams m_MessageParams = new InstructionMessageParams();
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Log: {this.m_MessageParams.Message}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionCommonDebugText()
        { }

        public InstructionCommonDebugText(string text)
        {
            this.m_MessageParams.Message = text;
        }

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            if (m_MessageParams.ShowTriggerRow)
            {
#if UNITY_EDITOR
                Debug.Log($"TriggerObject:{args?.dynamicObject?.gameObject?.name}");
#else
                Utility.Text.Format("TriggerObject:{0}", args?.TriggerObject);
#endif
            }
#if UNITY_EDITOR
            Debug.Log($"Instruction  Log:{this.m_MessageParams.Message}");
#else
            Utility.Text.Format("Instruction Log:{0}", this.m_MessageParams.Message);
#endif
        }

        public override string ParamsString()
        {
            return m_MessageParams.ToJson();
        }

        public override void LoadParams(string instructionParams)
        {
            m_MessageParams.LoadFromJson(instructionParams);
        }

        // METHODS: -------------------------------------------------------------------------------

    }
}
