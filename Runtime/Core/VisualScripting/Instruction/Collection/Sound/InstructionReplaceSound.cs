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
    // [Common.Title("PlayTimeline")]
    [Category("Sound/替换音频")]
    [Serializable]
    public class InstructionReplaceSound : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionReplaceSoundParams ParamsRaw = new InstructionReplaceSoundParams();
        public override IParams Params => this.ParamsRaw;

        [ShowInInspector]
        public override InstructionType InstructionType
        {
            get
            {
                return ParamsRaw.WaitToComplete ? InstructionType.Coroutine : InstructionType.Immediate;
            }
        }
        [HideInEditorMode]
        public bool SoundReset = false;

        protected override IEnumerator Run(Args args)
        {
            SoundReset = false;
            Debug.Log($"Play Sound:{ParamsRaw.OldSoundUuid}");
            args.Main.Sound.SoundReplace(ParamsRaw.OldSoundUuid, ParamsRaw.NewSoundUuid, 0, ParamsRaw.FadeTime, () =>
            {
                SoundReset = true;
            }, ParamsRaw.Loop);
            while (!SoundReset)
            {
                yield return null;
            }
        }

        public override void RunImmediate(Args args)
        {
            SoundReset = false;
            args.Main.Sound.SoundReplace(ParamsRaw.OldSoundUuid, ParamsRaw.NewSoundUuid, 0, ParamsRaw.FadeTime, null, loop: ParamsRaw.Loop);
        }


    }
}
