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
    [Category("Character/设置玩家是否可交互")]

    [Serializable]
    public class InstructionSetPlayerIsInteractive : Instruction
    {
        [ShowInInspector]
        public Args LastestArgs { get; set; }

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionBoolParams ParamsRaw = new InstructionBoolParams();
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        public override IParams Params => this.ParamsRaw;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Character IsInteractive: {this.ParamsRaw.Val}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionSetPlayerIsInteractive()
        { }

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            LastestArgs = args;
            Debug.Log($"InstructionSetPlayerIsInteractive:{this.ParamsRaw.Val},{args?.Main}");

            if (args?.Main != null)
            {
                var character = args?.Main?.CharacterService?.Player.character;
                if (character != null) character.IsInteractive = this.ParamsRaw.Val;
            }

        }



    }
}
