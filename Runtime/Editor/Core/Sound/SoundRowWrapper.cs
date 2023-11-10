#if UNITY_EDITOR
using System;
using System.Collections;
using Sirenix.OdinInspector;
using Pangoo.Core.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEditor;
using Pangoo.Editor;

namespace Pangoo
{
    public class SoundRowWrapper : ExcelTableTableRowWrapper<SoundTableOverview, ExcelTableRowNewWrapper<SoundTableOverview, SoundTable.SoundRow>, SoundTable.SoundRow>
    {


        [ShowInInspector]
        [DelayedProperty]
        public override string Name
        {
            get { return m_Row?.Name ?? null; }
            set
            {
                if (m_Row != null && m_Overview != null)
                {
                    m_Row.Name = value;
                    Save();
                }
            }
        }
        [ShowInInspector]
        public string PackageDir
        {
            get
            {
                if (Row.PackageDir != Overview.Config.PackageDir)
                {
                    Row.PackageDir = Overview.Config.PackageDir;
                    Save();
                }

                return Row.PackageDir;
            }
        }


        [ShowInInspector]
        [ValueDropdown("GetSoundTypeList")]
        public string SoundType
        {
            get
            {
                return Row?.SoundType;
            }
            set
            {
                Row.SoundType = value;
                Save();
            }
        }

        IEnumerable GetSoundTypeList()
        {
            var dropdown = new ValueDropdownList<string>();
            dropdown.Add("Effect");
            dropdown.Add("Music");
            dropdown.Add("Voice");
            return dropdown;
        }


        [ShowInInspector]
        [ValueDropdown("GetSoundAssetPathList")]
        public string AssetPath
        {
            get
            {
                if (AssetAudioClip == null)
                {
                    UpdateAudioClip();
                }

                return Row?.AssetPath;
            }
            set
            {
                Row.AssetPath = value;
                Save();
                UpdateAudioClip();
            }
        }
        [ReadOnly]
        public AudioClip AssetAudioClip;

        public void UpdateAudioClip()
        {
            var packageDir = Overview.Config.PackageDir;
            var path = AssetUtility.GetSoundAssetPath(packageDir, SoundType, Row.AssetPath);
            if (File.Exists(path))
            {
                AssetAudioClip = AssetDatabaseUtility.LoadAssetAtPath<AudioClip>(path);
            }
        }


        IEnumerable GetSoundAssetPathList()
        {
            var dropdown = new ValueDropdownList<string>();
            var packageDir = Overview.Config.PackageDir;
            if (SoundType.IsNullOrWhiteSpace())
            {
                return dropdown;
            }

            var dirPath = AssetUtility.GetSoundTypeDir(packageDir, SoundType);
            DirectoryUtility.ExistsOrCreate(dirPath);

            var clips = AssetDatabaseUtility.FindAsset<AudioClip>(dirPath);
            foreach (var clip in clips)
            {
                var path = AssetDatabase.GetAssetPath(clip);
                var filepath = Path.GetFileName(path);
                dropdown.Add(filepath);
            }


            return dropdown;
        }
        bool m_IsPlaying;


        bool isPlaying
        {
            get
            {
                return m_IsPlaying || EditorAudioUtility.IsPreviewClipPlaying(AssetAudioClip);
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

        [Button("@PlayerText")]
        private void StartPlayback()
        {
            if (isPlaying)
            {
                EditorAudioUtility.StopAllPreviewClips();
                isPlaying = false;
                return;
            }

            if (AssetAudioClip != null)
            {
                if (!isPlaying)
                {
                    EditorAudioUtility.PlayPreviewClip(AssetAudioClip);
                    isPlaying = true;
                    return;
                }
            }




        }










    }


}
#endif