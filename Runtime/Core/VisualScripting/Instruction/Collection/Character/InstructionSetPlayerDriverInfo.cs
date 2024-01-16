using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Character/设置玩家控制器数据")]
    [Serializable]
    public class InstructionSetPlayerDriverInfo : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSetPlayerDriverInfoParams ParamsRaw = new InstructionSetPlayerDriverInfoParams();

        public override IParams Params => this.ParamsRaw;
        public override void RunImmediate(Args args)
        {
            if (args?.Main != null)
            {
                EntityCharacter player = args?.Main?.CharacterService.Player;
                player.character.SetControllerData(ParamsRaw.DriverInfo);
            }

        }
    }
}

