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
    public class SoundService : BaseService
    {
        ExcelTableService m_ExcelTableService;


        public ExcelTableService TableService
        {
            get
            {
                return m_ExcelTableService;
            }
        }
        SoundTable m_SoundTable;

        public struct SoundPlayingInfo
        {
            public int SoundId;
            public Action ResetCallBack;
        }

        [ShowInInspector]
        public Dictionary<int, SoundPlayingInfo> m_SerialPlaying = new Dictionary<int, SoundPlayingInfo>();


        protected override void DoAwake()
        {
            base.DoAwake();

            m_ExcelTableService = Parent.GetService<ExcelTableService>();
        }

        protected override void DoStart()
        {

            m_SoundTable = m_ExcelTableService.GetExcelTable<SoundTable>();
            Event.Subscribe(PlaySoundResetEventArgs.EventId, OnPlaySoundReset);


            // m_EntityGroupRow = EntityGroupRowExtension.CreateDynamicObjectGroup();

            // m_DynamicObjectInfo = m_GameInfoService.GetGameInfo<DynamicObjectInfo>();
            // Debug.Log($"DoStart DynamicObjectService :{m_EntityGroupRow} m_EntityGroupRow:{m_EntityGroupRow.Name}");
        }

        [Button("播放")]
        public void PlaySound(int soundId, Action playResetCallback = null, bool loop = false)
        {
            var row = m_SoundTable.GetRowById(soundId);
            var path = AssetUtility.GetSoundAssetPath(row.PackageDir, row.SoundType, row.AssetPath);
            int serialId = 0;
            if (loop)
            {
                PlaySoundParams playSoundParams = new PlaySoundParams();
                playSoundParams.Loop = true;
                serialId = PangooEntry.Sound.PlaySound(path, "Default", playSoundParams);

            }
            else
            {
                serialId = PangooEntry.Sound.PlaySound(path, "Default");
            }
            Debug.Log($"Start Play :{path}. serialId:{serialId}");
            if (serialId != 0)
            {
                var info = new SoundPlayingInfo();
                info.SoundId = soundId;
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
        public void StopSound(int soundId)
        {
            foreach (var kv in m_SerialPlaying)
            {
                if (kv.Value.SoundId == soundId)
                {
                    PangooEntry.Sound.StopSound(kv.Key);
                }
            }
        }

        void OnPlaySoundReset(object sender, GameFrameworkEventArgs e)
        {
            var args = e as PlaySoundResetEventArgs;
            Debug.Log($"OnPlaySoundReset:{args},m_SerialPlaying:{m_SerialPlaying}");
            Debug.Log($"OnPlaySoundReset:{args.SerialId}");
            if (m_SerialPlaying.ContainsKey(args.SerialId))
            {
                m_SerialPlaying[args.SerialId].ResetCallBack.Invoke();
                m_SerialPlaying.Remove(args.SerialId);
            }
        }


    }
}