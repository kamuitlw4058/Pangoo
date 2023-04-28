

using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Timeline
{
    /// <summary>
    /// Internal use only.  Not part of the public API.
    /// </summary>
    public sealed class SelectionActivationClip : PlayableAsset
    {
        /// <summary>The name to display on the track</summary>
        public string DisplayName;

        /// <summary>The virtual camera to activate</summary>
        public ExposedReference<SelectionActivationBase> ActivationList;

        /// <summary>PlayableAsset implementation</summary>
        /// <param name="graph"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<SelectionActivationBehaviour>.Create(graph);
            var BaseSelectList = ActivationList.Resolve(graph.GetResolver());
            playable.GetBehaviour().Target = BaseSelectList.Gos[BaseSelectList.Index];
            return playable;
        }


    }
}
