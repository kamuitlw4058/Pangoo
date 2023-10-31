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

    [Category("Debug/Continuous Log Text")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionCommonContinuousDebugText : Instruction
    {
        protected static readonly TimeMode DefaultTime = new TimeMode(TimeMode.UpdateMode.GameTime);


        public override InstructionType InstructionType => InstructionType.Coroutine;

        public int CurrentCount { get; set; }

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionContinuousMessageParams ParamsRaw = new InstructionContinuousMessageParams();

        public override IParams Params => this.ParamsRaw;


        public override string Title => $"Log: {this.ParamsRaw.Message}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionCommonContinuousDebugText()
        {

        }

        public InstructionCommonContinuousDebugText(string text)
        {
            this.ParamsRaw.Message = text;
        }

        protected override IEnumerator Run(Args args)
        {
            CurrentCount = ParamsRaw.SecoundCount;
            while (CurrentCount > 0)
            {
                RunImmediate(args);
                CurrentCount -= 1;
                yield return WaitTime(1, DefaultTime);
            }
        }

        public override void RunImmediate(Args args)
        {
            if (ParamsRaw.ShowTriggerRow)
            {
#if UNITY_EDITOR
                Debug.Log($"TriggerRow:{args?.dynamicObject?.gameObject?.name}");
#else
                Utility.Text.Format("TriggerObject:{0}", args?.dynamicObject);
#endif
            }
#if UNITY_EDITOR
            Debug.Log($"Instruction Index:{CurrentCount}:{Time.frameCount} Log:{this.ParamsRaw.Message}");
#else
            Utility.Text.Format("Instruction Log:{0}", this.ParamsRaw.Message);
#endif
        }




    }
}
