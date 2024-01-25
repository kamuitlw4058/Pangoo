using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Common;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Character/设置玩家身高随动态对象距离变化")]
    [Serializable]
    public class InstructionChangeHightByDynamicObjectDistance : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionChangeHightByDynamicObjectDistanceParams ParamsRaw =
            new InstructionChangeHightByDynamicObjectDistanceParams();

        public override IParams Params => this.ParamsRaw;
        
        
        public override void RunImmediate(Args args)
        {
            var player = args?.Main?.CharacterService.Player;
            if (player!=null)
            {
                Vector3 playerPos = new Vector3(player.transform.position.x,0,player.transform.position.z);
                Vector3 targetPos = new Vector3(args.Target.transform.position.x, 0, args.Target.transform.position.z);
                var playerDistance = Vector3.Distance(playerPos,targetPos);
                var height = MathUtility.ClampRemap(playerDistance,
                    new Vector2(ParamsRaw.MinDistance, ParamsRaw.MaxDistance),
                    new Vector2(ParamsRaw.StartHeight, ParamsRaw.EndHeight));
                
                player.character.SetCharacterHeight(height);
            }
        }
    }
}

