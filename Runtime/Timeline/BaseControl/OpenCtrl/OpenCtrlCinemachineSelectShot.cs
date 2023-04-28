

using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Timeline
{
    /// <summary>
    /// Internal use only.  Not part of the public API.
    /// </summary>
    public sealed class OpenCtrlCinemachineSelectShot : PlayableAsset
    {
        /// <summary>The name to display on the track</summary>
        public string DisplayName;

        /// <summary>The virtual camera to activate</summary>
        public ExposedReference<OpenCtrlCinemahineBase> Cameras;

        /// <summary>PlayableAsset implementation</summary>
        /// <param name="graph"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<PangooCinemachineShotPlayable>.Create(graph);
            var cameraBase = Cameras.Resolve(graph.GetResolver());
            playable.GetBehaviour().VirtualCamera = cameraBase.Cameras[cameraBase.Index];
            return playable;
        }


    }
}
