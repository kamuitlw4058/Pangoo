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
    [Category("Timeline/Play Timeline")]
    [Serializable]
    public class InstructionPlayTimeline : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionPlayTimelineParams m_Params = new InstructionPlayTimelineParams();

        [ShowInInspector]
        public override InstructionType InstructionType
        {
            get
            {
                return m_Params.WaitToComplete ? InstructionType.Coroutine : InstructionType.Immediate;
            }
        }

        protected override IEnumerator Run(Args args)
        {
            if (args.Target != null)
            {
                PlayableDirector playableDirector = args.Target.GetComponent<PlayableDirector>();
                playableDirector.Play();

                while (playableDirector.state == PlayState.Playing)
                {
                    yield return null;
                }
            }
        }

        public override void RunImmediate(Args args)
        {
            if (args.Target != null)
            {
                PlayableDirector playableDirector = args.Target.GetComponent<PlayableDirector>();
                playableDirector.Play();
                Debug.Log($"Play :{playableDirector}");
            }
        }

        public override string ParamsString()
        {
            return m_Params.Save();
        }

        public override void LoadParams(string instructionParams)
        {
            m_Params.Load(instructionParams);
        }
    }
}
