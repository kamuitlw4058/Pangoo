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
    [Category("Timeline/子物体暂停Timeline")]
    [Serializable]
    public class InstructionSubGameObjectPauseTimeline : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSubGameObjectBoolParams ParamsRaw = new InstructionSubGameObjectBoolParams();


        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            if (trans == null)
            {
                return;
            }


            var playableDirector = trans.GetComponent<PlayableDirector>();
            if (playableDirector == null)
            {
                return;
            }

            playableDirector.Pause();

        }


    }
}
