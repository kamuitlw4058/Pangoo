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
    [Category("Timeline/子物体播放Timeline")]
    [Serializable]
    public class InstructionSubGameObjectPlayTimeline : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSubGameObjectBoolParams ParamsRaw = new InstructionSubGameObjectBoolParams();

        [ShowInInspector]
        public override InstructionType InstructionType
        {
            get
            {
                return ParamsRaw.Val ? InstructionType.Coroutine : InstructionType.Immediate;
            }
        }


        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {
            Transform trans = InstructionArgsExtension.GetTransformByPath(args, ParamsRaw.Path);
            bool timelineStarted = false;
            Debug.Log($"PlayTimeline path:{ParamsRaw.Path} trans:{trans}");
            if (trans != null)
            {


                var playableDirector = trans.GetComponent<PlayableDirector>();
                if (playableDirector == null)
                {
                    Debug.Log($"No Find  playableDirector:{trans}");
                    yield break;
                }

                playableDirector.playOnAwake = false;
                trans.gameObject.SetActive(true);
                playableDirector.enabled = true;

                playableDirector.Play();
                yield return null;
                // Debug.Log($"Start playableDirector:{trans} val:{ParamsRaw.Val} playableDirector.time:{playableDirector.time},  playableDirector.playableAsset.duration:{playableDirector.playableAsset.duration}");

                switch (playableDirector.extrapolationMode)
                {
                    case DirectorWrapMode.None:
                        while (ParamsRaw.Val && (!timelineStarted || (timelineStarted && playableDirector.time != 0)))
                        {
                            // Debug.Log($"Start playableDirector:{trans} val:{ParamsRaw.Val} playableDirector.time:{playableDirector.time},  playableDirector.playableAsset.duration:{playableDirector.playableAsset.duration}");
                            if (!timelineStarted)
                            {
                                timelineStarted = true;
                            }
                            yield return null;
                        }
                        break;
                    case DirectorWrapMode.Hold:

                        while (ParamsRaw.Val && (playableDirector.time != playableDirector.playableAsset.duration))
                        {
                            yield return null;
                        }
                        break;
                }

            }
            else
            {
                yield break;
            }
        }

        public override void RunImmediate(Args args)
        {
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            Debug.Log($"PlayTimeline Immediate trans:{trans}");
            if (trans != null)
            {

                var playableDirector = trans.GetComponent<PlayableDirector>();
                if (playableDirector == null)
                {
                    return;
                }

                playableDirector.playOnAwake = false;
                trans.gameObject.SetActive(true);
                playableDirector.enabled = true;

                playableDirector.Play();

            }
        }


    }
}
