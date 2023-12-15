#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;
using UnityEditor;
using Pangoo.Editor;


namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class SoundRowWrapper : MetaTableRowWrapper<SoundOverview, SoundNewRowWrapper, UnitySoundRow>
    {
        [ShowInInspector]
        [DelayedProperty]
        public override string Name
        {
            get { return UnityRow.Name; }
            set
            {
                UnityRow.Row.Name = value;
                Save();
            }
        }
        public string PackageDir
        {
            get
            {
                if (UnityRow.Row.PackageDir != Overview.Config.PackageDir)
                {
                    UnityRow.Row.PackageDir = Overview.Config.PackageDir;
                    Save();
                }

                return UnityRow.Row.PackageDir;
            }
        }


        [ShowInInspector]
        [ValueDropdown("GetSoundTypeList")]
        public string SoundType
        {
            get
            {
                return UnityRow.Row.SoundType;
            }
            set
            {
                UnityRow.Row.SoundType = value;
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

                return UnityRow.Row?.AssetPath;
            }
            set
            {
                UnityRow.Row.AssetPath = value;
                Save();
                UpdateAudioClip();
            }
        }
        [ReadOnly]
        public AudioClip AssetAudioClip;

        public void UpdateAudioClip()
        {
            var packageDir = Overview.Config.PackageDir;
            var path = AssetUtility.GetSoundAssetPath(packageDir, SoundType, UnityRow.Row.AssetPath);
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
        [TableColumnWidth(60, resizable: false)]
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

        public override void Save()
        {
            UnityRow.Row.PackageDir = Overview.Config.PackageDir;
            base.Save();

        }

    }
}
#endif

