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
    [Category("Character/设置玩家是否显示Hotspot")]


    [Serializable]
    public class InstructionSetPlayerEnabledHotspot : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionBoolParams ParamsRaw = new InstructionBoolParams();

        public override IParams Params => this.ParamsRaw;


        public override string Title => $"Character: {this.ParamsRaw}";


        public InstructionSetPlayerEnabledHotspot()
        { }

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            if (args?.Main != null)
            {
                args.Main.CharacterService.PlayerEnabledHotspot = ParamsRaw.Val;
            }

        }



    }
}
