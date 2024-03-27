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
        public float timer;
        public bool playFlag;
        public override void RunImmediate(Args args)
        {
            if (ParamsRaw.OnStartPlay&&!playFlag)
            {
                args.Main.Sound.PlaySound(ParamsRaw.SoundUuid);
                playFlag = true;
            }
            if (timer<ParamsRaw.CycleTime)
            {
                timer+=Time.deltaTime;
            }
            else
            {
                args.Main.Sound.PlaySound(ParamsRaw.SoundUuid);
                timer = 0;
            }
            
        }
    }
}

