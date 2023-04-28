

using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Timeline
{
    /// <summary>
    /// Internal use only.  Not part of the public API.
    /// </summary>
    public sealed class OpenCtrlClip : PlayableAsset
    {
        public bool IsOpen;

        public Ease EaseType;

        /// <summary>The virtual camera to activate</summary>
        public ExposedReference<OpenCtrlBase> OpenCtrlTarget;

        /// <summary>PlayableAsset implementation</summary>
        /// <param name="graph"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<OpenCtrlBehaviour>.Create(graph);
            var behviour = playable.GetBehaviour();

            behviour.OpenCtrlTarget = OpenCtrlTarget.Resolve(graph.GetResolver());
            behviour.IsOpen = IsOpen;
            behviour.EaseType = EaseType;
            return playable;
        }

    }

}
