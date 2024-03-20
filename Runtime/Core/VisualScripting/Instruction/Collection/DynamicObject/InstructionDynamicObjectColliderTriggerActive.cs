using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("DynamicObject/设置动态物体碰撞触发开关")]
    [Serializable]
    public class InstructionDynamicObjectColliderTriggerActive : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionDynamicObjectBoolParams ParamsRaw = new InstructionDynamicObjectBoolParams();
        public override IParams Params => this.ParamsRaw;

        public override void RunImmediate(Args args)
        {

            var dynamicObject = args.dynamicObject;
            if (ParamsRaw.DynamicObjectUuid.IsNullOrWhiteSpace() || ParamsRaw.DynamicObjectUuid.Equals("Self"))
            {
                dynamicObject?.SetColliderTriggerActive(ParamsRaw.Val);
                Debug.Log($"DynamicObject Self:{dynamicObject.Row.Name} Set ColliderTrigger:{ParamsRaw.Val} ");
            }
            else
            {
                var DynamicObjectService = args.Main.DynamicObject;
                var targetEntity = DynamicObjectService.GetLoadedEntity(ParamsRaw.DynamicObjectUuid);
                var targetDynamicObject = targetEntity?.DynamicObj;
                if (targetDynamicObject != null)
                {
                    targetDynamicObject?.SetColliderTriggerActive(ParamsRaw.Val);
                    Debug.Log($"DynamicObjectUuid:{ParamsRaw.DynamicObjectUuid},{targetDynamicObject.Row.Name}   Set ColliderTrigger:{ParamsRaw.Val} ");
                }

            }

        }
    }
}

