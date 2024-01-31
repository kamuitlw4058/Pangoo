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

    [Category("Variables/设置Int")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionSetVariableInt : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSetVariableIntParams ParamsRaw = new InstructionSetVariableIntParams();

        public override IParams Params => this.ParamsRaw;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Log: {this.ParamsRaw}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionSetVariableInt()
        { }


        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            var VariableType = args?.Main.RuntimeData.GetVariableType(ParamsRaw.VariableUuid);
            Debug.Log($"Set VariableType:{VariableType} Set VariableType:{ParamsRaw.VariableType}, VariableId:{ParamsRaw.VariableUuid} val:{ParamsRaw.Val}");
            if (VariableType != null && VariableType == VariableTypeEnum.DynamicObject && args?.dynamicObject != null)
            {
                args?.dynamicObject?.SetVariable<int>(ParamsRaw.VariableUuid, ParamsRaw.Val);
            }

            if (VariableType != null && VariableType == VariableTypeEnum.Global)
            {
                args?.Main.RuntimeData.SetVariable<int>(ParamsRaw.VariableUuid, ParamsRaw.Val);
            }

        }


    }
}
