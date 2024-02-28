

using System;
using UnityEngine.Playables;

namespace Pangoo.Timeline
{
    [Serializable]
    public class DialogueSubtitleBehaviour : PlayableBehaviour
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
