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
    // [Common.Title("")]
    [Category("Sound/停止音频")]
    [Serializable]
    public class InstructionStopSound : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionStopSoundParams ParamsRaw = new InstructionStopSoundParams();
        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            Debug.Log($"Close Sound:{ParamsRaw.SoundUuid}");
            args.Main.Sound.StopSound(ParamsRaw.SoundUuid, fadeOutSeconds: ParamsRaw.FadeTime);
        }


    }
}
