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

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionPlayerPostionParams ParamsRaw = new InstructionPlayerPostionParams();
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        public override IParams Params => this.ParamsRaw;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Character: {this.ParamsRaw.CharacterUuid}";

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
            Debug.Log($"InstructionCreateOrMovePlayer:{this.ParamsRaw.CharacterUuid},{args?.Main}");

            if (args?.Main != null)
            {
                var characterService = args.Main.CharacterService;
                var characterUuid = this.ParamsRaw.CharacterUuid;
                Debug.Log($"InstructionCreateOrMovePlayer  get characterService:{characterService} characterId:{characterUuid}");
                if (characterUuid.IsNullOrWhiteSpace())
                {
                    characterUuid = args.Main?.GameConfig?.GetGameMainConfig()?.DefaultPlayer;
                    if (characterUuid.IsNullOrWhiteSpace())
                    {
                        Debug.LogError("Get Player Id failed!");
                        return;
                    }

                }
                characterService?.ShowCharacter(characterUuid, ParamsRaw.Position, ParamsRaw.Rotation, ParamsRaw.Height, ParamsRaw.IsInteractive);
            }

        }



    }
}
