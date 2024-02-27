

using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Timeline
{
    /// <summary>
    /// Internal use only.  Not part of the public API.
    /// </summary>
    public sealed class DialogueSubtitleClip : PlayableAsset
    {

        string m_Subtitle;

        /// <summary>The virtual camera to activate</summary>
        [ShowInInspector]
        public string Subtitle
        {
            get
            {
                return m_Subtitle;
            }
            set
            {
                Debug.Log($"name:{name}");
                m_Subtitle = value;
                name = value;
            }
        }
        /// <summary>PlayableAsset implementation</summary>
        /// <param name="graph"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<DialogueSubtitleBehaviour>.Create(graph);
            // var SubtitleParams = Subtitle.Resolve(graph.GetResolver());
            playable.GetBehaviour().Subtitle = Subtitle;
            return playable;
        }


    }
}
