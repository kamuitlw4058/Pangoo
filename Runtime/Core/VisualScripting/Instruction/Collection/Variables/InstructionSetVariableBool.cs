using System;
using System.Collections;
using System.Threading.Tasks;
using GameFramework;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework;
using Pangoo.Core.Services;

namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]

    [Common.Title("设置Bool")]
    // [Description("Prints a message to the Unity Console")]

    [Category("Variables/设置Bool")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionSetVariableBool : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSetVariableParams m_Params = new InstructionSetVariableParams();
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Log: {this.m_Params}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionSetVariableBool()
        { }


        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            var runtimeService = args.Main.GetService<RuntimeDataService>();
            runtimeService.SetVariable<bool>(m_Params.VariableId, m_Params.Val);
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
