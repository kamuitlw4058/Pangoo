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

    // [Common.Title("设置Bool")]
    // [Description("Prints a message to the Unity Console")]

    [Category("Variables/等待Bool变成预定值")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionWaitVariableBool : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionWaitBoolVariableParams ParamsRaw = new InstructionWaitBoolVariableParams();

        public override IParams Params => this.ParamsRaw;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Wait: {this.ParamsRaw}";

        public override InstructionType InstructionType => InstructionType.Coroutine;


        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionWaitVariableBool()
        { }


        protected override IEnumerator Run(Args args)
        {
            var VariableType = args?.Main.RuntimeData.GetVariableType(ParamsRaw.VariableUuid);
            if (VariableType == null)
            {
                Debug.LogError($"VariableType Is null");
                yield break;
            }

            switch (VariableType)
            {
                case VariableTypeEnum.DynamicObject:
                    if (args?.dynamicObject == null)
                    {
                        Debug.LogError($"InstructionWaitVariableBool dynamicObject Is null");
                        yield break;
                    }
                    var val = args.dynamicObject.GetVariable<bool>(ParamsRaw.VariableUuid);
                    while (val != ParamsRaw.Val)
                    {
                        yield return null;
                    }

                    break;
                case VariableTypeEnum.Global:
                    var globalVal = args?.Main.RuntimeData.GetVariable<bool>(ParamsRaw.VariableUuid);
                    while (globalVal != ParamsRaw.Val)
                    {
                        yield return null;
                    }
                    break;
            }


        }

        public override void RunImmediate(Args args)
        {


        }


    }
}
