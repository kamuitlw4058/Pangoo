using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Sound/周期性播放音頻")]
    [Serializable]
    public class InstructionCyclePlaySound : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionCyclePlaySoundParams ParamsRaw = new InstructionCyclePlaySoundParams();

        public override IParams Params => ParamsRaw;
        public override void RunImmediate(Args args)
        {
            var currentTimerValue = args.dynamicObject.GetVariable<float>(ParamsRaw.CurrentTimeVariableUuid);
            var flagValue = args.dynamicObject.GetVariable<bool>(ParamsRaw.StartPlayFlagVariableUuid);
            
            if (ParamsRaw.OnStartPlay&&!flagValue)
            {
                args.Main.Sound.PlaySound(ParamsRaw.SoundUuid);
                args.dynamicObject.SetVariable(ParamsRaw.StartPlayFlagVariableUuid,true);
            }
            if (currentTimerValue<ParamsRaw.CycleTime)
            {
                currentTimerValue += Time.deltaTime;
            }
            else
            {
                args.Main.Sound.PlaySound(ParamsRaw.SoundUuid);
                currentTimerValue = 0;
            }
            args.dynamicObject.SetVariable(ParamsRaw.CurrentTimeVariableUuid,currentTimerValue);
        }
    }
}

