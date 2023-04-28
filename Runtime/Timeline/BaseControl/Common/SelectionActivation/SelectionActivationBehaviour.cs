

using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Timeline
{
    sealed class SelectionActivationBehaviour : PlayableBehaviour
    {
        public GameObject Target;


        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            Target.SetActive(true);
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            Target.SetActive(false);
        }


    }
}
