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
    [Category("动态物体/暂停子物体Timeline")]
    [Serializable]
    public class InstructionDynamicObjectPauseSubTimeline : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionDynamicObjectPathParams ParamsRaw = new InstructionDynamicObjectPathParams();

        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {

            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {

            Transform trans = InstructionArgsExtension.GetTransformByPath(args, ParamsRaw.DynamicObjectUuid, ParamsRaw.Path);
            Debug.Log($"PlayTimeline Immediate trans:{trans}");
            if (trans != null)
            {
                var playableDirector = trans.GetComponent<PlayableDirector>();
                if (playableDirector == null)
                {
                    return;
                }

                playableDirector.Pause();

            }
        }


    }
}
