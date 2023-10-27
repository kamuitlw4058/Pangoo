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
    [Common.Title("Debug Text")]
    [Category("Character/设置玩家是否可控制")]



    [Serializable]
    public class InstructionSetPlayerControllable : Instruction
    {
        [ShowInInspector]
        public Args LastestArgs { get; set; }

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionBoolParams ParamsRaw = new InstructionBoolParams();
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        public override IParams Params => this.ParamsRaw;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Character: {this.ParamsRaw}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionSetPlayerControllable()
        { }

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            LastestArgs = args;

            if (args?.Main != null)
            {
                var characterService = args.Main.GetService<CharacterService>();
                if (characterService.Player != null)
                {
                    characterService.Player.character.IsControllable = ParamsRaw.Val;
                    Debug.Log($"SetPlayer val:{ParamsRaw.Val}");
                }
            }

        }



    }
}
