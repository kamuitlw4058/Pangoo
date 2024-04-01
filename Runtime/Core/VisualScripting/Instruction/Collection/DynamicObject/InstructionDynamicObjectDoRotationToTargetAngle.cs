using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("DynamicObject/DoRotation")]
    [Serializable]
    public class InstructionDynamicObjectDoRotationToTargetAngle : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionDynamicObjectDoRotationToTargetAngleParams ParamsRaw = new InstructionDynamicObjectDoRotationToTargetAngleParams();

        public override IParams Params => ParamsRaw;
        
        public override void RunImmediate(Args args)
        {
    
            var trans = args?.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            
            if (trans.localEulerAngles.Equals(ParamsRaw.TargetRotation))
            {
                return;
            }
            //计算旋转进度
            float totalMagnitude=Quaternion.Angle(Quaternion.Euler(ParamsRaw.InitRotation) , Quaternion.Euler(ParamsRaw.TargetRotation));
            float currentMagnitude=Quaternion.Angle(trans.rotation , Quaternion.Euler(ParamsRaw.TargetRotation));

            var damping = 1f;
            //TODO:计算阻尼
            if (ParamsRaw.UseDamping)
            {
                damping = ParamsRaw.AnimationCurve.Evaluate(1-currentMagnitude/totalMagnitude);
                Debug.Log("当前电闸阻力:"+damping);
            }

            if (currentMagnitude > 0)
            {
                trans.rotation=Quaternion.RotateTowards(trans.rotation, Quaternion.Euler(ParamsRaw.TargetRotation), 
                    damping*ParamsRaw.RotationSpeed*Time.deltaTime);
            }
            else
            {
                trans.localEulerAngles = ParamsRaw.TargetRotation;
            }
        }
    }
}

