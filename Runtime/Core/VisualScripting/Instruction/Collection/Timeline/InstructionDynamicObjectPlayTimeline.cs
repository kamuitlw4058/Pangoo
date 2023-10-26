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
    [Category("Timeline/动态物体播放Timeline")]
    [Serializable]
    public class InstructionDynamicObjectPlayTimeline : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionIntParams m_Params = new InstructionIntParams();

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {

            var dynamicObject = args.dynamicObject;
            var DynamicObjectService = dynamicObject.DynamicObjectService;
            Debug.Log($"m_Params:{m_Params}");
            Debug.Log($"m_Params:{m_Params.Val}");
            Debug.Log($"args.dynamicObject:{dynamicObject}");
            Debug.Log($"args.dynamicObject.DynamicObjectService:{DynamicObjectService}");
            Debug.Log($"args.dynamicObject.DynamicObjectService.DeltaTime:{DynamicObjectService.DeltaTime}");
            var targetEntity = DynamicObjectService.GetLoadedEntity(m_Params.Val);
            Debug.Log($"targetEntity:{targetEntity}");
            targetEntity?.DynamicObj?.PlayTimeline();
            Debug.Log($"Play :{m_Params.Val} Timeline");
        }

        public override string ParamsString()
        {
            return m_Params.Save();
        }

        public override void LoadParams(string instructionParams)
        {
            m_Params.Load(instructionParams);
        }
    }
}
