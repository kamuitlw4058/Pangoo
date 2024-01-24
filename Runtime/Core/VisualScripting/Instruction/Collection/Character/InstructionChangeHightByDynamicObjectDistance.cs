using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Character/设置玩家控制器数据")]
    [Serializable]
    public class InstructionChangeHightByDynamicObjectDistance : Instruction
    {
        [SerializeField] [LabelText("参数")] [HideReferenceObjectPicker]
        public InstructionChangeHightByDynamicObjectDistanceParams ParamsRaw =
            new InstructionChangeHightByDynamicObjectDistanceParams();

        public override IParams Params => this.ParamsRaw;
        
        
        public override void RunImmediate(Args args)
        {
            var player = args?.Main?.CharacterService.Player;
            if (player!=null)
            {
                var playerDistance = Vector3.Distance(player.transform.position,args.Target.transform.position);
                if (playerDistance<ParamsRaw.MaxDistance)
                {
                    var progress = 1 - (playerDistance-ParamsRaw.MinDistance) / ParamsRaw.TwoPointDistance;
                    //Debug.Log("当前进度:"+progress);
                    var playerNormalHeight = player.EntityData.InfoRow.Height;
                    //Debug.Log("当前默认高度:"+playerNormalHeight);
                    
                    var height = playerNormalHeight*(1-progress);
                    height = Math.Clamp(height,ParamsRaw.MinHeight,playerNormalHeight);
                    //Debug.Log("当前身高:"+height);
                    player.character.SetCharacterHeight(height);
                }
                else
                {
                    if (player.GetComponent<CharacterController>().height.Equals(player.EntityData.InfoRow.Height))
                    {
                        return;
                    }
                    player.character.SetCharacterHeight(player.EntityData.InfoRow.Height);
                }
                
            }
            
        }
    }
}

