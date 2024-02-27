

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
    [TrackClipType(typeof(DialogueSubtitleClip))]
    [TrackColor(0.8f, 0.8f, 0.8f)]
    public class DialogueSubtitleTrack : TrackAsset
    {

    }
}
