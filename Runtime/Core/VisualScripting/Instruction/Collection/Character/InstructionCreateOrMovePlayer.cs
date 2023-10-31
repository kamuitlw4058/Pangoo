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
    [Category("Character/CreateOrMovePlayer")]



    [Serializable]
    public class InstructionCreateOrMovePlayer : Instruction
    {
        [ShowInInspector]
        public Args LastestArgs { get; set; }

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionPlayerPostionParams ParamsRaw = new InstructionPlayerPostionParams();
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        public override IParams Params => this.ParamsRaw;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Character: {this.ParamsRaw.CharacterId}";

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionCreateOrMovePlayer()
        { }

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            LastestArgs = args;
            Debug.Log($"Instruction  Log:{this.ParamsRaw.CharacterId}");
#if UNITY_EDITOR
            Debug.Log($"Instruction  Log:{this.ParamsRaw.CharacterId}");
#else
            Utility.Text.Format("Instruction Log:{0}", this.ParamsRaw.CharacterId);
#endif
            if (args?.Main != null)
            {
                var characterService = args.Main.GetService<CharacterService>();
                characterService?.ShowCharacter(ParamsRaw.CharacterId, ParamsRaw.Position, ParamsRaw.Rotation);
            }

        }



    }
}
