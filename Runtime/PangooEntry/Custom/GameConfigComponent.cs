using System;
using System.Collections.Generic;
using System.Reflection;
using GameFramework.Resource;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public class GameConfigComponent : GameFrameworkComponent
    {
        public GameMainConfig m_GameMainConfig;

        Dictionary<string, bool> m_LoadWhenGameStartStatus = new Dictionary<string, bool>();
        readonly Dictionary<string, string> m_LoadWhenGameStart = new Dictionary<string, string>()
        {
            {"GameMainConfig",AssetUtility.GetGameMainConfig()},
        };


        public void LoadAllConfig()
        {
            foreach (var configItem in m_LoadWhenGameStart)
            {
                m_LoadWhenGameStartStatus.Add(configItem.Key, false);
                Debug.Log($"LoadAllConfig Load:{configItem.Key} path:{configItem.Value}");
                PangooEntry.Resource.LoadAsset(configItem.Value, new LoadAssetCallbacks(
                    (assetName, asset, duration, userData) =>
                    {
                        switch (configItem.Key)
                        {
                            case "GameMainConfig":
                                m_GameMainConfig = (GameMainConfig)asset;
                                break;
                        }

                        m_LoadWhenGameStartStatus[configItem.Key] = true;
                        if (CheckLoaded())
                        {
                            Debug.Log($"LoadAllConfig Finish");
                            PangooEntry.Event.Fire(this, PangooLoadGameConfigFinishEventArgs.Create());
                        }
                        else
                        {
                            Debug.Log($"LoadAllConfig False");
                        }
                    },
                     (assetName, asset, errorMessage, userData) => { Debug.Log($"LoadAllConfig Load:{configItem.Key} Failed!: {errorMessage}"); }
                )
                );
            }
        }

        public bool CheckLoaded()
        {
            if (m_LoadWhenGameStartStatus.Count != m_LoadWhenGameStart.Count)
            {
                return false;
            }

            foreach (var kv in m_LoadWhenGameStartStatus)
            {
                if (!kv.Value)
                {
                    return false;
                }
            }
            IsLoaded = true;
            return true;
        }

        [ShowInInspector]
        public bool IsLoaded { get; set; }


        public GameMainConfig GetGameMainConfig()
        {
            return m_GameMainConfig;
        }



    }
}