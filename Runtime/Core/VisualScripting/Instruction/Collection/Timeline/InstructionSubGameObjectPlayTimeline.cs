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
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            Debug.Log($"PlayTimeline trans:{trans}");
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
                Debug.Log($"Start playableDirector:{trans}");

                playableDirector.Play();
                yield return null;



                while (ParamsRaw.Val && (playableDirector.time != playableDirector.playableAsset.duration) && playableDirector.time != 0)
                {
                    // Debug.Log($"Start playableDirector:{trans} :{ParamsRaw.Val} :{playableDirector.time} :{playableDirector.playableAsset.duration}");
                    yield return null;
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
