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
            Debug.Log($"InstructionCreateOrMovePlayer:{this.ParamsRaw.CharacterId},{args?.Main}");

            if (args?.Main != null)
            {
                var characterService = args.Main.CharacterService;
                var characterId = this.ParamsRaw.CharacterId;
                Debug.Log($"InstructionCreateOrMovePlayer  get characterService:{characterService} characterId:{characterId}");
                if (characterId == 0)
                {
                    characterId = args.Main?.GameConfig?.GetGameMainConfig()?.DefaultPlayer ?? 0;
                    if (characterId == 0)
                    {
                        Debug.LogError("Get Player Id failed!");
                        return;
                    }

                }
                characterService?.ShowCharacter(characterId, ParamsRaw.Position, ParamsRaw.Rotation, ParamsRaw.Height, ParamsRaw.IsInteractive);
            }

        }



    }
}
