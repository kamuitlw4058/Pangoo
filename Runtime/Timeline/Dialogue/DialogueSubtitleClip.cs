

using Cinemachine;
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Timeline
{
    /// <summary>
    /// Internal use only.  Not part of the public API.
    /// </summary>
    [Serializable]
    public sealed class DialogueSubtitleClip : PlayableAsset
    {
        public DialogueSubtitleBehaviour template = new DialogueSubtitleBehaviour();
        /// <summary>PlayableAsset implementation</summary>
        /// <param name="graph"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {

            return ScriptPlayable<DialogueSubtitleBehaviour>.Create(graph, template);
        }

    }
}
