using System;
using System.IO;
using UnityEngine;
using LitJson;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class DialogueSubtitleInfo
    {
        [JsonMember("InfoType")]
        public DialogueSubtitleType InfoType;

        [JsonMember("SoundUuid")]
        [ShowIf("@this.InfoType == DialogueSubtitleType.Sound")]
        [LabelText("音频Uuid")]
        public string SoundUuid;

#if UNITY_EDITOR

        [field: NonSerialized]
        [JsonNoMember]
        AudioClip m_AssetAudioClip;

        [ShowInInspector]
        [ShowIf("@this.InfoType == DialogueSubtitleType.Sound")]
        [JsonNoMember]
        public AudioClip AudioClip
        {
            get
            {
                if (m_AssetAudioClip == null && !SoundUuid.IsNullOrWhiteSpace())
                {
                    var soundRow = SoundOverview.GetUnityRowByUuid(SoundUuid);
                    var soundOverview = SoundOverview.GetOverviewByUuid(SoundUuid);
                    var packageDir = soundOverview.Config.PackageDir;
                    var path = AssetUtility.GetSoundAssetPath(packageDir, soundRow.Row.SoundType, soundRow.Row.AssetPath);
                    if (File.Exists(path))
                    {
                        m_AssetAudioClip = AssetDatabaseUtility.LoadAssetAtPath<AudioClip>(path);
                    }
                }
                return m_AssetAudioClip;
            }
        }
#endif


        [JsonMember("Range")]
        [LabelText("片段起始")]
        [ShowIf("@this.InfoType != DialogueSubtitleType.RecoverPoint")]
        public Vector2 Range;

        [LabelText("内容")]
        [JsonMember("Content")]
        [ShowIf("@this.InfoType == DialogueSubtitleType.Subtitle")]
        public string Content;


        [LabelText("恢复点")]
        [JsonMember("RecoverPoint")]
        [ShowIf("@this.InfoType == DialogueSubtitleType.RecoverPoint")]
        public float RecoverPoint;
    }
}