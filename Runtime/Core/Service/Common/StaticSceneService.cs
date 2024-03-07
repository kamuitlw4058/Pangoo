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

namespace Pangoo.Core.Services
{
    public class SceneRuntimeData
    {
        public EntityStaticScene Entity;
        public StaticSceneService Service;

        public EntityStaticSceneData EntityData
        {
            get
            {

                return Entity?.SceneData;
            }
        }
    }

    [Serializable]
    public class StaticSceneService : MainSubService
    {
        public override string ServiceName => "StaticSceneService";
        public override int Priority => 5;

        IEntityGroupRow m_EntityGroupRow;


        StaticSceneInfo m_StaticSceneInfo;

        EntityLoader Loader = null;

        [ShowInInspector]
        Dictionary<string, int> m_EnterAssetCountDict;

        public Dictionary<string, int> EnterAssetCountDict
        {
            get
            {
                return m_EnterAssetCountDict;
            }
        }

        public int EnterAssetCount
        {
            get
            {
                return m_EnterAssetCountDict.Count;
            }
        }

        [ShowInInspector]
        Dictionary<string, EntityStaticScene> m_LoadedSceneAssetDict;


        public Dictionary<string, EntityStaticScene> LoadedSceneAssetDict
        {
            get
            {
                return m_LoadedSceneAssetDict;
            }
        }

        public string LastestEnterUuid;

        [ShowInInspector]
        List<string> m_LoadingAssetUuids;



        List<string> m_SceneUuids;

        [ShowInInspector]

        Dictionary<string, StaticSceneInfoRow> m_SceneInfos;



        // 需要加载的场景列表。 Key: StaticSceneId Value:AssetPathId
        [ShowInInspector]
        Dictionary<string, string> NeedLoadDict;



        public Action OnInitSceneLoaded;



        protected override void DoAwake()
        {
            base.DoAwake();
            NeedLoadDict = new Dictionary<string, string>();

            m_LoadingAssetUuids = new List<string>();
            m_LoadedSceneAssetDict = new Dictionary<string, EntityStaticScene>();
            m_EnterAssetCountDict = new Dictionary<string, int>();

            m_SceneUuids = new List<string>();
            m_SceneInfos = new Dictionary<string, StaticSceneInfoRow>();

            Event.Subscribe(EnterStaticSceneEventArgs.EventId, OnEnterStaticSceneEvent);
            Event.Subscribe(ExitStaticSceneEventArgs.EventId, OnExitStaticSceneEvent);
        }

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

        protected override void DoStart()
        {

            // m_StaticSceneTable = m_ExcelTableService.GetExcelTable<StaticSceneTable>();
            m_StaticSceneInfo = GameInfoSrv.GetGameInfo<StaticSceneInfo>();
            m_EntityGroupRow = EntityGroupRowExtension.CreateStaticSceneGroup();
            Debug.Log($"DoStart StaticSceneService :{m_EntityGroupRow} m_EntityGroupRow:{m_EntityGroupRow.Name}");
        }


        public void SetGameSectionScenes(List<string> sceneUuids)
        {
            m_SceneUuids.Clear();
            m_SceneUuids.AddRange(sceneUuids);
            m_SceneInfos.Clear();

            foreach (var uuid in m_SceneUuids)
            {
                var sceneInfo = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(uuid);
                if (!m_SceneInfos.ContainsKey(sceneInfo.Uuid))
                {
                    m_SceneInfos.Add(sceneInfo.Uuid, sceneInfo);
                }
            }
        }

        public void SetSceneModelActive(string uuid, bool val)
        {
            if (m_SceneInfos.TryGetValue(uuid, out StaticSceneInfoRow info))
            {
                if (m_LoadedSceneAssetDict.TryGetValue(info.AssetPathUuid, out EntityStaticScene entity))
                {
                    entity.SceneData.Hide = !val;
                }
            }
        }

        public bool CheckGameSectionScenesLoaded()
        {
            foreach (var sceneInfo in m_SceneInfos)
            {
                if (!LoadedSceneAssetDict.TryGetValue(sceneInfo.Value.AssetPathUuid, out EntityStaticScene entityStaticScene))
                {
                    return false;
                }
            }

            return true;
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
                    return GetInfoRowByAssetUuid(k);
                }
            }

            StaticSceneInfoRow ret = GetInfoRowByAssetUuid(LastestEnterUuid);
            return ret;
        }



        public void ShowStaticScene(string staticSceneUuid)
        {
            if (Loader == null)
            {
                Loader = EntityLoader.Create(this);
            }

            //通过路径ID去判断是否被加载。用来在不同的章节下用了不用的静态场景ID,但是使用不同的加载Ids
            var sceneInfo = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(staticSceneUuid);
            var AssetPathUuid = sceneInfo.AssetPathUuid;
            if (m_LoadedSceneAssetDict.ContainsKey(AssetPathUuid))
            {
                return;
            }

            // if (!IsInGameSectionConfig(staticSceneUuid))
            // {
            //     return;
            // }

            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if (m_LoadingAssetUuids.Contains(AssetPathUuid))
            {
                return;
            }
            else
            {
                Log($"ShowStaticScene:{sceneInfo.Name}[{staticSceneUuid.ToShortUuid()}]");
                EntityStaticSceneData data = EntityStaticSceneData.Create(sceneInfo, sceneInfo.CreateEntityInfo(m_EntityGroupRow), this);
                m_LoadingAssetUuids.Add(AssetPathUuid);
                Loader.ShowEntity(EnumEntity.StaticScene,
                    (o) =>
                    {
                        Log($"ShowStaticScene Loaded:{staticSceneUuid}-{sceneInfo.Name}");
                        if (m_LoadingAssetUuids.Contains(AssetPathUuid))
                        {
                            m_LoadingAssetUuids.Remove(AssetPathUuid);
                        }
                        var entity = o.Logic as EntityStaticScene;
                        if (entity == null)
                        {
                            Log($"On Entity Showed. entity is null. staticSceneUuid:{staticSceneUuid},AssetPathUuid:{AssetPathUuid}!");
                        }

                        m_LoadedSceneAssetDict.Add(AssetPathUuid, entity);
                    },
                    data.EntityInfo,
                    data);
            }
        }



        public StaticSceneInfoRow GetInfoRowByAssetUuid(string assetUuid)
        {
            StaticSceneInfoRow sceneInfo;
            if (assetUuid.IsNullOrWhiteSpace()) return null;

            if (m_SceneInfos.TryGetValue(assetUuid, out sceneInfo))
            {
                return sceneInfo;
            }

            return null;
        }

        public void UpdateNeedLoadDict()
        {
            NeedLoadDict.Clear();
            foreach (var sceneInfo in m_SceneInfos)
            {
                NeedLoadDict.Add(sceneInfo.Value.Uuid, sceneInfo.Value.AssetPathUuid);
            }

        }

        public void UpdateAutoLoad()
        {
            foreach (var loadUuid in NeedLoadDict.Keys)
            {
                ShowStaticScene(loadUuid);
            }
        }

        public void UpdateAutoRelease()
        {
            List<string> removeScene = new List<string>();
            foreach (var item in m_LoadedSceneAssetDict)
            {
                if (!NeedLoadDict.ContainsValue(item.Key) && !m_EnterAssetCountDict.ContainsKey(item.Key))
                {
                    if (Loader.GetEntity(item.Value.Id))
                    {
                        Loader.HideEntity(item.Value.Id);
                        Log($"Hide Scene:{item.Key}");
                    }
                    removeScene.Add(item.Key);
                }
            }

            foreach (var removeUuid in removeScene)
            {
                m_LoadedSceneAssetDict.Remove(removeUuid);
            }

        }


        protected override void DoUpdate()
        {
            UpdateNeedLoadDict();
            UpdateAutoLoad();
            UpdateAutoRelease();

        }

    }
}