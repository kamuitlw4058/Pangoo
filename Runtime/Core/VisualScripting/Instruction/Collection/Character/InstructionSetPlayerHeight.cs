using System;
using System.Collections;
using System.Threading.Tasks;
using GameFramework;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework;

namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]
    // [Common.Title("Debug Text")]
    [Category("Character/设置玩家的身高")]

    [Serializable]
    public class InstructionSetPlayerHeight : Instruction
    {
        [ShowInInspector]
        public Args LastestArgs { get; set; }

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionFloatParams ParamsRaw = new InstructionFloatParams();
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        public override IParams Params => this.ParamsRaw;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Character Height: {this.ParamsRaw.Val1}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionSetPlayerHeight()
        { }

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            LastestArgs = args;
            Debug.Log($"InstructionSetPlayerHeight:{this.ParamsRaw.Val1},{args?.Main}");

            if (args?.Main != null)
            {
                var characterService = args?.Main?.CharacterService;
                characterService?.SetPlayerCameraHeight(ParamsRaw.Val1);
            }

        }



    }
}
