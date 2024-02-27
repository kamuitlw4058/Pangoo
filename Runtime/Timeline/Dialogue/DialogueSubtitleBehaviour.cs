

using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Timeline
{
    sealed class DialogueSubtitleBehaviour : PlayableBehaviour
    {
        public string Subtitle;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
        }


    }
}
