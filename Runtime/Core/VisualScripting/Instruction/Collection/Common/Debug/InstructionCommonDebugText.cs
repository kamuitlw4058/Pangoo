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
        InstructionMessageParams ParamsRaw = new InstructionMessageParams();
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        public override IParams Params => this.ParamsRaw;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Log: {this.ParamsRaw.Message}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionCommonDebugText()
        { }

        public InstructionCommonDebugText(string text)
        {
            this.ParamsRaw.Message = text;
        }

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            if (ParamsRaw.ShowTriggerRow)
            {
#if UNITY_EDITOR
                Debug.Log($"TriggerObject:{args?.dynamicObject?.gameObject?.name}");
#else
                Utility.Text.Format("TriggerObject:{0}", args?.dynamicObject);
#endif
            }
#if UNITY_EDITOR
            Debug.Log($"Instruction  Log:{this.ParamsRaw.Message}");
#else
            Utility.Text.Format("Instruction Log:{0}", this.ParamsRaw.Message);
#endif
        }



    }
}
