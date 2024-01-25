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

                var inputMinMax = new Vector2();

                var playerDistance = Vector3.Distance(player.transform.position,args.Target.transform.position);
                var height = MathUtility.ClampRemap(playerDistance,
                    new Vector2(ParamsRaw.StartDistance, ParamsRaw.EndDistance),
                    new Vector2(ParamsRaw.StartHeight, ParamsRaw.EndHeight));
                //Debug.Log($"playerDistance:{playerDistance},ParamsRaw.MinDistance:{ParamsRaw.StartDistance},ParamsRaw.MaxDistance:{ParamsRaw.EndDistance}  , height:{height},ParamsRaw.StartHeight:{ParamsRaw.StartHeight},ParamsRaw.EndHeight:{ParamsRaw.EndHeight}");;
                player.character.SetCharacterHeight(height);
            }
        }
    }
}

