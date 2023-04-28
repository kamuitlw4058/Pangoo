

using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Timeline
{
    /// <summary>
    /// Timeline track for Cinemachine virtual camera activation
    /// </summary>
    [Serializable]
    [TrackClipType(typeof(PangooCinemachineShot))]
    [TrackColor(0.53f, 0.0f, 0.08f)]
    public class PangooCinemachineTrack : TrackAsset
    {
        /// <summary>
        /// TrackAsset implementation
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="go"></param>
        /// <param name="inputCount"></param>
        /// <returns></returns>
        public override Playable CreateTrackMixer(
            PlayableGraph graph, GameObject go, int inputCount)
        {

#if !UNITY_2019_2_OR_NEWER
            // Hack to set the display name of the clip to match the vcam
            foreach (var c in GetClips())
            {
                PangooCinemachineShot shot = (PangooCinemachineShot)c.asset;
                CinemachineVirtualCameraBase vcam = shot.VirtualCamera.Resolve(graph.GetResolver());
                if (vcam != null)
                    c.displayName = vcam.Name;
            }
#endif
            var mixer = ScriptPlayable<PangooCinemachineMixer>.Create(graph);
            mixer.SetInputCount(inputCount);
            return mixer;
        }
    }
}
