

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
    [TrackClipType(typeof(OpenCtrlClip))]
    [TrackColor(0.08f, 0.08f, 0.53f)]
    public class OpenCtrlTrack : TrackAsset
    {
    }
}
