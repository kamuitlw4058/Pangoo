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

    [Category("Variables/动态物体CanBan")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionInverseDynamiaObjectCanBan : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionBoolParams ParamsRaw = new InstructionBoolParams();

        public override IParams Params => this.ParamsRaw;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Log: {this.ParamsRaw}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionInverseDynamiaObjectCanBan()
        { }


        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            // Debug.Log($"Set CanBan To:{ParamsRaw.Val} Old:{args.dynamicObject.CanHotspotBan}");
            args.dynamicObject.CanHotspotBan = ParamsRaw.Val;
        }


    }
}
