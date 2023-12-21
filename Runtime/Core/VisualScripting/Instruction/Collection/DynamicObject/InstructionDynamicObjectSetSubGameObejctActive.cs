using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using System;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("PlayTimeline")]
    [Category("动态物体/开关子GameObject")]
    [Serializable]
    public class InstructionDynamicObjectSetSubGameObejctActive : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionDynamicObjectSubGameObjectBoolParams ParamsRaw = new InstructionDynamicObjectSubGameObjectBoolParams();

        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            if (ParamsRaw.Path == null)
            {
                Debug.LogWarning($"Set SubGameObjectActive DynamicObjectId:{ParamsRaw.DynamicObjectUuid}  Active:{ParamsRaw.Val} Path Is Null");
                return;
            }

            var dynamicObject = args.dynamicObject;
            if (ParamsRaw.DynamicObjectUuid.IsNullOrWhiteSpace() || ParamsRaw.DynamicObjectUuid.Equals("Self"))
            {
                dynamicObject?.SetSubGameObjectsActive(ParamsRaw.Path, ParamsRaw.Val);
                Debug.Log($"DynamicObject Set Self ModelActive:{ParamsRaw.Val} ");
            }
            else
            {
                var DynamicObjectService = args.Main.DynamicObject;
                var targetEntity = DynamicObjectService.GetLoadedEntity(ParamsRaw.DynamicObjectUuid);
                targetEntity?.DynamicObj?.SetSubGameObjectsActive(ParamsRaw.Path, ParamsRaw.Val);
                Debug.Log($"DynamicObjectId:{ParamsRaw.DynamicObjectUuid}  SetPath:{ParamsRaw.Path} Active:{ParamsRaw.Val} ");
            }

        }


    }
}
