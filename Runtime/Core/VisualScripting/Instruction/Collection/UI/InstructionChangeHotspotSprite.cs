using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Pangoo.Core.VisualScripting
{
    [Category("DynamicObject/调用对象MouseDrag方法")]
    [Serializable]
    public class InstructionChangeHotspotSprite : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionChangeHotspotSpriteParams ParamsRaw = new InstructionChangeHotspotSpriteParams();
        public override IParams Params { get; }
        
        public override void RunImmediate(Args args)
        {
            Transform target=args.dynamicObject.CachedTransfrom.Find(ParamsRaw.TargetName);
            if (!target.GetComponent<DynamicObjectMouseInteract>())
            {
                return;
            }
            DynamicObjectMouseInteract mouseInteract = target.GetComponent<DynamicObjectMouseInteract>();

            SpotDynamicObjectMouseInteract spotDynamicObjectMouseInteract=(SpotDynamicObjectMouseInteract)mouseInteract.hotSpot;
            spotDynamicObjectMouseInteract.CurrentSpotState = ParamsRaw.State; 
        }
    }
}