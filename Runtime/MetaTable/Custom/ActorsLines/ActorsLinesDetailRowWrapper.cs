#if UNITY_EDITOR

using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using MetaTable;
using UnityEngine.Timeline;
using Pangoo.Timeline;
using Pangoo.Core.VisualScripting;


namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class ActorsLinesDetailRowWrapper : MetaTableDetailRowWrapper<ActorsLinesOverview, UnityActorsLinesRow>
    {

        TimelineAsset m_Clip;

        [ShowInInspector]
        [LabelText("Timeline文件")]
        public TimelineAsset TimelineAsset
        {
            get
            {
                if (m_Clip == null && !UnityRow.Row.TimelinePath.IsNullOrWhiteSpace())
                {
                    m_Clip = AssetDatabase.LoadAssetAtPath<TimelineAsset>(UnityRow.Row.TimelinePath);
                }

                return m_Clip;
            }
            set
            {
                m_Clip = value;
                UnityRow.Row.TimelinePath = value != null ? AssetDatabase.GetAssetPath(value) : string.Empty;
                Save();
            }
        }



        List<DialogueSubtitleInfo> m_DialogueInfos;

        [ShowInInspector]
        public string TimelinePath
        {
            get
            {
                return UnityRow.Row.TimelinePath;
            }
        }

        [ShowInInspector]
        public float Duration
        {
            get
            {
                return UnityRow.Row.Duration;
            }
            set
            {
                UnityRow.Row.Duration = value;
                Save();
            }
        }


        [ShowInInspector]
        [HideReferenceObjectPicker]
        [ListDrawerSettings(ShowFoldout = true)]
        [LabelText("对话信息")]
        public List<DialogueSubtitleInfo> DialogueInfos
        {
            get
            {
                if (m_DialogueInfos == null)
                {
                    try
                    {
                        m_DialogueInfos = JsonMapper.ToObject<List<DialogueSubtitleInfo>>(UnityRow.Row.DialogueSubtitles);
                    }
                    catch
                    {

                    }
                }
                return m_DialogueInfos;
            }
            set
            {
                m_DialogueInfos = value;
                UnityRow.Row.DialogueSubtitles = JsonMapper.ToJson(m_DialogueInfos);
                Save();
            }
        }


        [Button("分析Timeline设置对话")]
        public void AnalysisTimeline()
        {
            Debug.Log($"timeline Asset: Clip:{TimelineAsset.duration} root:{TimelineAsset?.rootTrackCount} output:{TimelineAsset?.outputTrackCount}");
            List<DialogueSubtitleInfo> timelineDialogueInfos = new List<DialogueSubtitleInfo>();
            var tracks = TimelineAsset.GetOutputTracks();
            Duration = (float)TimelineAsset.duration;
            foreach (var track in tracks)
            {
                var clips = track.GetClips();
                var clipsCount = clips.Count();
                if (track is AudioTrack)
                {

                    Debug.Log($"Audio Track Clips Count:{clipsCount}");
                    if (clipsCount > 0)
                    {

                        foreach (var audioClip in clips)
                        {
                            var audioPlayableAsset = audioClip.asset as AudioPlayableAsset;
                            if (audioPlayableAsset != null && audioPlayableAsset.clip != null)
                            {
                                var assetPath = AssetDatabase.GetAssetPath(audioPlayableAsset.clip);
                                var ClipPathSoundUuid = SoundOverview.GetSoundUuidByPath(assetPath);
                                Debug.Log($"Audio Clip Path:{assetPath} Uuid:{ClipPathSoundUuid}");
                                if (!ClipPathSoundUuid.IsNullOrWhiteSpace())
                                {
                                    var dialogueInfo = new DialogueSubtitleInfo();
                                    dialogueInfo.SoundUuid = ClipPathSoundUuid;
                                    dialogueInfo.InfoType = DialogueSubtitleType.Sound;
                                    dialogueInfo.Range = new Vector2() { x = (float)audioClip.start, y = (float)audioClip.end };
                                    timelineDialogueInfos.Add(dialogueInfo);
                                }

                            }
                        }

                    }

                }

                if (track is DialogueSubtitleTrack)
                {
                    if (clipsCount > 0)
                    {
                        Debug.Log($"Dialogue Subtitle");
                        foreach (var clip in clips)
                        {
                            var clipAsset = clip.asset as DialogueSubtitleClip;
                            var dialogueInfo = new DialogueSubtitleInfo();
                            dialogueInfo.Content = clipAsset.template.Subtitle;
                            dialogueInfo.InfoType = DialogueSubtitleType.Subtitle;
                            dialogueInfo.Range = new Vector2() { x = (float)clip.start, y = (float)clip.end };
                            timelineDialogueInfos.Add(dialogueInfo);
                        }


                    }
                }



            }

            if (timelineDialogueInfos.Count > 0)
            {
                DialogueInfos = timelineDialogueInfos;
            }
        }


    }
}
#endif

