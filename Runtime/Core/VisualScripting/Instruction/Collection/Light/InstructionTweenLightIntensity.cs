using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Light/TweenLightIntensity")]
    [Serializable]
    public class InstructionTweenLightIntensity : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionTweenLightIntensityParams ParamsRaw = new InstructionTweenLightIntensityParams();

        public override IParams Params => ParamsRaw;
        
        [ShowInInspector]
        public override InstructionType InstructionType
        {
            get
            {
                return ParamsRaw.WaitFinsh ? InstructionType.Coroutine : InstructionType.Immediate;
            }
        }
        
        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            Transform target=null;
            Debug.Log("oop:"+ParamsRaw.TargetPath);
            if (ParamsRaw.TargetPath=="Self")
            {
                target=args.Target.transform;
            }
            else
            {
                target=args.dynamicObject.CachedTransfrom.Find(ParamsRaw.TargetPath);
            }
            
            if (!target.GetComponent<Light>())
            {
                return;
            }
            Light light=target.GetComponent<Light>();
            light.DOIntensity(ParamsRaw.Value,ParamsRaw.TweenTime);
        }
    }
}

