using System;
using System.Collections;
using System.Threading.Tasks;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting{

    // [Version(0, 1, 1)]

    [Title("Debug Text")]
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
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField]
        public string m_Message = string.Empty;
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string Title => $"Log: {this.m_Message}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionCommonDebugText()
        { }

        public InstructionCommonDebugText(string text)
        {
            this.m_Message = text;
        }

        protected override IEnumerator Run(Args args)
        {
            Debug.Log(this.m_Message);
            yield break;
        }

        public override string ParamsString()
        {
            return m_Message;
        }

        public override void LoadParams(string instructionParams)
        {
            m_Message = instructionParams;
        }

        // METHODS: -------------------------------------------------------------------------------

    }
}
