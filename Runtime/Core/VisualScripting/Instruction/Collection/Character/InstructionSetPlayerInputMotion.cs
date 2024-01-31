using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Characters;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Character/设置玩家行走模式")]
    [Serializable]
    public class InstructionSetPlayerInputMotion : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSetPlayerInputMotionParams ParamsRaw = new InstructionSetPlayerInputMotionParams();

        public override IParams Params => ParamsRaw;
        
        public override void RunImmediate(Args args)
        {
            Debug.Log("模式更改为:"+ParamsRaw.InputMotionType);
            args.Main.CharacterService.Player.character.CharacterInput.InputMotionType = ParamsRaw.InputMotionType;
        }
    }
}

