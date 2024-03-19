using System;
using System.Collections.Generic;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;
using GameFramework;
using GameFramework.Sound;
using UnityGameFramework.Runtime;


namespace Pangoo.Core.Services
{
    public class SoundService : MainSubService
    {


        public struct SoundPlayingInfo
        {
            public string SoundUuid;
            public Action ResetCallBack;
        }

        [ShowInInspector]
        public Dictionary<int, SoundPlayingInfo> m_SerialPlaying = new();


        protected override void DoAwake()
        {
            base.DoAwake();

        }

        protected override void DoStart()
        {

            Event.Subscribe(PlaySoundResetEventArgs.EventId, OnPlaySoundReset);


            // m_EntityGroupRow = EntityGroupRowExtension.CreateDynamicObjectGroup();

            // m_DynamicObjectInfo = m_GameInfoService.GetGameInfo<DynamicObjectInfo>();
            // Debug.Log($"DoStart DynamicObjectService :{m_EntityGroupRow} m_EntityGroupRow:{m_EntityGroupRow.Name}");
        }

        [Button("播放")]
        public void PlaySound(string uuid, Action playResetCallback = null, bool loop = false, float fadeTime = 0, float offsetTime = 0, float volume = 1)
        {
            var row = MetaTableSrv.GetSoundByUuid(uuid);
            var path = AssetUtility.GetSoundAssetPath(row.PackageDir, row.SoundType, row.AssetPath);
            int serialId = 0;
            var playSoundParams = PlaySoundParams.Create();
            playSoundParams.Loop = loop;
            playSoundParams.FadeInSeconds = fadeTime;
            playSoundParams.Time = offsetTime;
            playSoundParams.VolumeInSoundGroup = volume;

            serialId = PangooEntry.Sound.PlaySound(path, "Default", playSoundParams);
            // Debug.Log($"Start Play :{path}. serialId:{serialId}");
            if (serialId != 0)
            {
                var info = new SoundPlayingInfo();
                info.SoundUuid = uuid;
                info.ResetCallBack = () =>
                {
                    if (playResetCallback != null)
                    {
                        playResetCallback.Invoke();
                    }
                };
                m_SerialPlaying.Add(serialId, info);
            }

        }

        [Button("停止")]
        public void StopSound(string soundUuid, float fadeOutSeconds = 0, bool canelResetCallback = false)
        {
            List<int> removeSerials = new List<int>();
            foreach (var kv in m_SerialPlaying)
            {
                if (kv.Value.SoundUuid == soundUuid)
                {
                    PangooEntry.Sound.StopSound(kv.Key, fadeOutSeconds);
                    if (canelResetCallback)
                    {
                        removeSerials.Add(kv.Key);
                    }
                }
            }
            foreach (var serialId in removeSerials)
            {
                m_SerialPlaying.Remove(serialId);
            }
        }

        public bool SoundTime(string soundUuid, out float time)
        {
            foreach (var kv in m_SerialPlaying)
            {
                if (kv.Value.SoundUuid == soundUuid)
                {

                    if (PangooEntry.Sound.SoundTime(kv.Key, out time))
                    {
                        return true;
                    }
                }
            }
            time = 0;
            return false;
        }

        public void SoundReplace(string oldSoundUuid, string newSoundUuid, float offsetTime, float fadeTime = 0, Action playResetCallback = null, bool loop = false)
        {
            float CurrentTime = 0;
            if (SoundTime(oldSoundUuid, out CurrentTime))
            {
                StopSound(oldSoundUuid, fadeTime);
            }
            Debug.Log($"Sound Replace CurrentTime:{CurrentTime}");

            PlaySound(newSoundUuid, playResetCallback, loop, offsetTime: CurrentTime + offsetTime, fadeTime: fadeTime);
        }

        void OnPlaySoundReset(object sender, GameFrameworkEventArgs e)
        {
            var args = e as PlaySoundResetEventArgs;
            // Debug.Log($"OnPlaySoundReset:{args},m_SerialPlaying:{m_SerialPlaying}");
            // Debug.Log($"OnPlaySoundReset:{args.SerialId}");
            if (m_SerialPlaying.ContainsKey(args.SerialId))
            {
                m_SerialPlaying[args.SerialId].ResetCallBack.Invoke();
                m_SerialPlaying.Remove(args.SerialId);
            }
        }


    }
}