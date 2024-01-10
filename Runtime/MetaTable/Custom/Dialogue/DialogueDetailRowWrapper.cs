#if UNITY_EDITOR

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;
using UnityEditor;
using Sirenix.Utilities.Editor;


namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class DialogueDetailRowWrapper : MetaTableDetailRowWrapper<DialogueOverview, UnityDialogueRow>
    {
        [LabelText("音频Uuid")]
        [ValueDropdown("SoundUuidValueDropdown")]
        [ShowInInspector]
        [OnValueChanged("OnSoundUuidChanged")]
        public string SoundUuid
        {
            get
            {
                if (!UnityRow.Row.SoundUuid.IsNullOrWhiteSpace() && AssetAudioClip == null)
                {
                    UpdateAudioClip();
                }

                return UnityRow.Row.SoundUuid;
            }
            set
            {

                UnityRow.Row.SoundUuid = value;
                Save();
            }

        }

        [ReadOnly]
        [ShowInInspector]
        public AudioClip AssetAudioClip { get; private set; }

        public void UpdateAudioClip()
        {
            if (UnityRow.Row.SoundUuid.IsNullOrWhiteSpace())
            {
                return;
            }


            var soundRow = SoundOverview.GetUnityRowByUuid(UnityRow.Row.SoundUuid);
            var soundOverview = SoundOverview.GetOverviewByUuid(UnityRow.Row.SoundUuid);

            var packageDir = Overview.Config.PackageDir;
            var path = AssetUtility.GetSoundAssetPath(soundOverview.Config.PackageDir, soundRow.Row.SoundType, soundRow.Row.AssetPath);
            if (File.Exists(path))
            {
                AssetAudioClip = AssetDatabaseUtility.LoadAssetAtPath<AudioClip>(path);
                Max = AssetAudioClip.length;
            }
        }

        public void OnSoundUuidChanged()
        {
            UpdateAudioClip();
        }

        public IEnumerable SoundUuidValueDropdown()
        {
            return SoundOverview.GetUuidDropdown();
        }

        bool m_IsPlaying;


        bool isPlaying
        {
            get
            {
                return m_IsPlaying || (m_AudioSource?.isPlaying ?? false);
            }
            set
            {
                m_IsPlaying = value;
            }
        }


        string PlayerText
        {
            get
            {

                if (isPlaying)
                {
                    return "暂停";
                }
                else
                {
                    return "播放";
                }

            }
        }

        AudioSource m_AudioSource;

        private void StartPlayback()
        {

            if (AssetAudioClip != null)
            {
                if (!isPlaying)
                {
                    if (m_AudioSource == null)
                    {
                        m_AudioSource = EditorUtility.CreateGameObjectWithHideFlags("Audio Player", HideFlags.HideAndDontSave).AddComponent<AudioSource>();
                        m_AudioSource.clip = AssetAudioClip;
                    }

                    // m_AudioSource.pu


                    m_AudioSource.Play();
                    isPlaying = true;
                }
            }

        }

        void PausePlayback()
        {
            if (isPlaying)
            {
                if (m_AudioSource != null)
                {
                    m_AudioSource.Pause();
                }
                isPlaying = false;
                return;
            }

        }

        void StopPlayback()
        {
            if (isPlaying)
            {
                if (m_AudioSource != null)
                {
                    m_AudioSource.Stop();
                }
                isPlaying = false;
                return;
            }
        }


        [ProgressBar(0, "Max")]
        [ShowInInspector]
        [InlineButton("StopPlayback", Icon = SdfIconType.StopFill, Label = "")]
        [InlineButton("PausePlayback", Icon = SdfIconType.Pause, Label = "")]
        [InlineButton("StartPlayback", Icon = SdfIconType.CaretRightFill, Label = "")]


        public float Progress
        {
            get
            {
                if (m_AudioSource != null)
                {
                    GUIHelper.RequestRepaint();
                    return m_AudioSource.time;
                }

                return 0;
            }
            set
            {
                if (m_AudioSource != null)
                {
                    m_AudioSource.time = value;
                    PausePlayback();
                }
            }
        }


        float Max;




    }
}
#endif

