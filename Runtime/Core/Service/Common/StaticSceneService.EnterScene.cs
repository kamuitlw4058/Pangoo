using Pangoo;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using System;
using UnityGameFramework.Runtime;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;
using Pangoo.Common;
using Sirenix.Utilities;

namespace Pangoo.Core.Services
{

    public partial class StaticSceneService
    {


        [ShowInInspector]
        Dictionary<string, int> m_EnterAssetCountDict;

        [ShowInInspector]
        public int EnterAssetCount
        {
            get
            {
                return m_EnterAssetCountDict.Count;
            }
        }

        public string LastestEnterUuid;

        void OnEnterStaticSceneEvent(object sender, GameFrameworkEventArgs e)
        {
            var args = e as EnterStaticSceneEventArgs;
            EnterSceneAsset(args.AssetPathUuid);
        }

        void OnExitStaticSceneEvent(object sender, GameFrameworkEventArgs e)
        {
            var args = e as ExitStaticSceneEventArgs;
            ExitSceneAsset(args.AssetPathUuid);
        }


        public void EnterSceneAsset(string assetPathUuid)
        {
            int count;

            Log($"EnterSceneAsset:{assetPathUuid} {m_EnterAssetCountDict.Count}");
            if (!m_EnterAssetCountDict.TryGetValue(assetPathUuid, out count))
            {
                m_EnterAssetCountDict.Add(assetPathUuid, 1);
            }
            else
            {
                m_EnterAssetCountDict[assetPathUuid] = count + 1;
            }

            LastestEnterUuid = assetPathUuid;

        }



        public void ExitSceneAsset(string assetPathUuid)
        {
            int count;
            Log($"ExitSceneAsset:{assetPathUuid},{m_EnterAssetCountDict.Count}");
            if (m_EnterAssetCountDict.TryGetValue(assetPathUuid, out count))
            {
                count -= 1;
                if (count <= 0)
                {
                    m_EnterAssetCountDict.Remove(assetPathUuid);
                    if (!LastestEnterUuid.IsNullOrWhiteSpace() && LastestEnterUuid.Equals(assetPathUuid))
                    {
                        LastestEnterUuid = null;
                    }
                }
                else
                {
                    m_EnterAssetCountDict[assetPathUuid] = count;
                }
            }
        }


        public string GetAssetPathUuid(string sceneUuid)
        {
            var info = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(sceneUuid);
            return info?.AssetPathUuid;
        }

        public bool CheckEnterScenes(string[] LoadScenes, string selfUuid)
        {
            var selfAssetPathUuid = GetAssetPathUuid(selfUuid);
            if (m_EnterAssetCountDict.ContainsKey(selfAssetPathUuid))
            {
                return true;
            }


            foreach (var scene in LoadScenes)
            {
                var assetPathUuid = GetAssetPathUuid(scene);
                if (m_EnterAssetCountDict.ContainsKey(assetPathUuid))
                {
                    return true;
                }
            }

            return false;
        }

        public StaticSceneInfoRow GetLastestEnterScene()
        {
            if (LastestEnterUuid.IsNullOrWhiteSpace())
            {
                if (m_EnterAssetCountDict.Count > 0)
                {
                    var k = m_EnterAssetCountDict.Keys.ToList()[0];
                    LastestEnterUuid = k;
                    return m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(k);
                }
            }

            StaticSceneInfoRow ret = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(LastestEnterUuid);
            return ret;
        }


    }
}