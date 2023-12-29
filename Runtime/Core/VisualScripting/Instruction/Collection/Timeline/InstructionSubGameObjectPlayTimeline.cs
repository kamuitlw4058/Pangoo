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

                //运行一帧后再进行检查。运行状态。
                yield return null;

                switch (playableDirector.extrapolationMode)
                {
                    case DirectorWrapMode.None:
                        //None 类型的会运行完成后回到开头。这种类型只要检测是否播放一集是否已经在开头就可以了。由于，这边已经运行了2帧。所以时间一定不为0
                        while (ParamsRaw.Val && (!timelineStarted || (timelineStarted && playableDirector.time != 0)))
                        {
                            if (!timelineStarted)
                            {
                                timelineStarted = true;
                            }
                            yield return null;
                        }
                        break;
                    case DirectorWrapMode.Hold:
                        //Hold状态会停在结尾。那只要检测Time是否为clip的时长就可以进行退出。
                        while (ParamsRaw.Val && (playableDirector.time != playableDirector.playableAsset.duration))
                        {
                            yield return null;
                        }
                        break;
                    case DirectorWrapMode.Loop:
                        // Loop类型的Timeline播放是不会结束的。
                        Debug.LogError($"Loop Timeline No Stop");
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
