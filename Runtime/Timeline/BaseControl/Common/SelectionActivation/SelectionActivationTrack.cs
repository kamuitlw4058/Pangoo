

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
    [TrackClipType(typeof(SelectionActivationClip))]
    [TrackColor(0.0f, 0.53f, 0.08f)]
    public class SelectActivationTrack : TrackAsset
    {

    }
}
