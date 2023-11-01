using Pangoo;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using System;
using UnityGameFramework.Runtime;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Services
{
    public class StaticSceneService : BaseService
    {
        public override int Priority => 5;

        ExcelTableService m_ExcelTableService;

        GameInfoService m_GameInfoService;

        EntityGroupTable m_EntityGroupTable;

        EntityGroupTable.EntityGroupRow m_EntityGroupRow;

        // StaticSceneTable m_StaticSceneTable;

        StaticSceneInfo m_StaticSceneInfo;

        EntityLoader Loader = null;

        [ShowInInspector]
        Dictionary<int, int> m_EnterAssetCountDict;

        [ShowInInspector]
        Dictionary<int, EntityStaticScene> m_LoadedSceneAssetDict;

        [ShowInInspector]
        List<int> m_LoadingAssetIds;


        [ShowInInspector]

        Dictionary<int, StaticSceneInfoRow> m_SectionSceneInfos;

        [ShowInInspector]
        List<int> m_DynamicStaticSceneIds;

        [ShowInInspector]
        List<int> m_HoldStaticSceneIds;


        [ShowInInspector]
        List<int> m_InitStaticSceneIds;


        // 需要加载的场景列表。 Key: StaticSceneId Value:AssetPathId
        [ShowInInspector]
        Dictionary<int, int> NeedLoadDict;





        public Tuple<int, int> m_SectionChange;


        public bool m_SectionInited = false;

        public Action OnInitSceneLoaded;



        protected override void DoAwake()
        {
            base.DoAwake();
            m_LoadingAssetIds = new List<int>();
            m_DynamicStaticSceneIds = new List<int>();
            m_HoldStaticSceneIds = new List<int>();
            m_InitStaticSceneIds = new List<int>();

            NeedLoadDict = new Dictionary<int, int>();
            m_SectionSceneInfos = new Dictionary<int, StaticSceneInfoRow>();

            m_ExcelTableService = Parent.GetService<ExcelTableService>();
            m_GameInfoService = Parent.GetService<GameInfoService>();

            m_LoadedSceneAssetDict = new Dictionary<int, EntityStaticScene>();
            m_EnterAssetCountDict = new Dictionary<int, int>();

            Event.Subscribe(EnterStaticSceneEventArgs.EventId, OnEnterStaticSceneEvent);
            Event.Subscribe(ExitStaticSceneEventArgs.EventId, OnExitStaticSceneEvent);
        }

        void OnEnterStaticSceneEvent(object sender, GameFrameworkEventArgs e)
        {
            var args = e as EnterStaticSceneEventArgs;
            EnterSceneAsset(args.AssetPathId);
        }

        void OnExitStaticSceneEvent(object sender, GameFrameworkEventArgs e)
        {
            var args = e as ExitStaticSceneEventArgs;
            ExitSceneAsset(args.AssetPathId);
        }

        protected override void DoStart()
        {

            // m_StaticSceneTable = m_ExcelTableService.GetExcelTable<StaticSceneTable>();
            m_StaticSceneInfo = m_GameInfoService.GetGameInfo<StaticSceneInfo>();
            m_EntityGroupTable = m_ExcelTableService.GetExcelTable<EntityGroupTable>();
            m_EntityGroupRow = EntityGroupRowExtension.CreateStaticSceneGroup();
            Debug.Log($"DoStart StaticSceneService :{m_EntityGroupRow} m_EntityGroupRow:{m_EntityGroupRow.Name}");
        }


        public bool IsInGameSectionConfig(int id)
        {
            if (m_InitStaticSceneIds.Contains(id))
            {
                return true;
            }

            if (m_HoldStaticSceneIds.Contains(id))
            {
                return true;
            }

            if (m_DynamicStaticSceneIds.Contains(id))
            {
                return true;
            }

            return false;
        }


        public void SetGameScetion(List<int> dynamicIds, List<int> holdIds, List<int> initIds)
        {
            m_SectionInited = false;
            m_DynamicStaticSceneIds.Clear();
            m_DynamicStaticSceneIds.AddRange(dynamicIds);

            m_SectionSceneInfos.Clear();
            foreach (var id in dynamicIds)
            {
                var sceneInfo = m_StaticSceneInfo.GetRowById<StaticSceneInfoRow>(id);
                m_SectionSceneInfos.Add(sceneInfo.AssetPathId, sceneInfo);
            }

            m_HoldStaticSceneIds.Clear();
            m_HoldStaticSceneIds.AddRange(holdIds);

            m_InitStaticSceneIds.Clear();
            m_InitStaticSceneIds.AddRange(initIds);
        }

        public void SetGameSectionChange(Tuple<int, int> value)
        {
            m_SectionChange = value;
        }



        public void EnterSceneAsset(int assetPathId)
        {
            int count;

            Log.Debug($"EnterSceneAsset:{assetPathId} {m_EnterAssetCountDict.Count}");
            if (!m_EnterAssetCountDict.TryGetValue(assetPathId, out count))
            {
                m_EnterAssetCountDict.Add(assetPathId, 1);
            }
            else
            {
                m_EnterAssetCountDict[assetPathId] = count + 1;
            }

            StaticSceneInfoRow staticSceneInfo;
            if (m_SectionSceneInfos.TryGetValue(assetPathId, out staticSceneInfo))
            {
                if (staticSceneInfo.Id == m_SectionChange.Item1)
                {
                    Event.Fire(this, GameSectionChangeEventArgs.Create(m_SectionChange.Item2));
                }
            }

        }

        public void ExitSceneAsset(int assetPathId)
        {
            int count;
            Log.Debug($"ExitSceneAsset:{assetPathId},{m_EnterAssetCountDict.Count}");
            if (m_EnterAssetCountDict.TryGetValue(assetPathId, out count))
            {
                count -= 1;
                if (count <= 0)
                {
                    m_EnterAssetCountDict.Remove(assetPathId);
                }
                else
                {
                    m_EnterAssetCountDict[assetPathId] = count;
                }
            }
        }

        public bool IsLoadedScene(List<int> ids)
        {
            bool allInited = true;
            foreach (var sceneId in ids)
            {
                var info = m_StaticSceneInfo.GetRowById<StaticSceneInfoRow>(sceneId);
                if (!m_LoadedSceneAssetDict.ContainsKey(info.AssetPathId))
                {
                    allInited = false;
                    break;
                }
            }
            return allInited;
        }

        public void ShowStaticScene(int staticSceneId)
        {
            if (Loader == null)
            {
                Loader = EntityLoader.Create(this);
            }

            //通过路径ID去判断是否被加载。用来在不同的章节下用了不用的静态场景ID,但是使用不同的加载Ids
            var sceneInfo = m_StaticSceneInfo.GetRowById<StaticSceneInfoRow>(staticSceneId);
            var AssetPathId = sceneInfo.AssetPathId;
            if (m_LoadedSceneAssetDict.ContainsKey(AssetPathId))
            {
                return;
            }

            if (!IsInGameSectionConfig(staticSceneId))
            {
                return;
            }

            Log.Info($"ShowStaticScene:{staticSceneId}");

            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if (m_LoadingAssetIds.Contains(AssetPathId))
            {
                return;
            }
            else
            {
                EntityStaticSceneData data = EntityStaticSceneData.Create(sceneInfo.CreateEntityInfo(m_EntityGroupRow), this);
                m_LoadingAssetIds.Add(AssetPathId);
                Loader.ShowEntity(EnumEntity.StaticScene,
                    (o) =>
                    {
                        Log.Info($"ShowStaticScene Loaded:{staticSceneId}-{sceneInfo.Name}");
                        if (m_LoadingAssetIds.Contains(AssetPathId))
                        {
                            m_LoadingAssetIds.Remove(AssetPathId);
                        }
                        m_LoadedSceneAssetDict.Add(AssetPathId, o.Logic as EntityStaticScene);

                        if (!m_SectionInited)
                        {
                            bool allInited = IsLoadedScene(m_HoldStaticSceneIds);
                            Log.Info($"Hold Loaded:{allInited}");
                            if (allInited)
                            {
                                allInited = IsLoadedScene(m_InitStaticSceneIds);
                                Log.Info($"Init Loaded:{allInited}");
                            }

                            if (allInited)
                            {
                                OnInitSceneLoaded?.Invoke();
                                Log.Info($"ON Loaded");
                                m_SectionInited = true;
                            }
                        }


                    },
                    data.EntityInfo,
                    data);
            }
        }

        public void UpdateNeedLoadDict()
        {
            NeedLoadDict.Clear();

            //章节服务会设置常驻的场景ID。用来打开该场景下通用的设置。
            foreach (var keepSceneId in m_HoldStaticSceneIds)
            {
                StaticSceneInfoRow sceneInfo = m_StaticSceneInfo.GetRowById<StaticSceneInfoRow>(keepSceneId);
                if (!NeedLoadDict.ContainsKey(sceneInfo.Id))
                {
                    NeedLoadDict.Add(sceneInfo.Id, sceneInfo.AssetPathId);
                }
            }

            if (m_EnterAssetCountDict.Count == 0)
            {
                foreach (var staticSceneId in m_InitStaticSceneIds)
                {
                    var loadSceneInfo = m_StaticSceneInfo.GetRowById<StaticSceneInfoRow>(staticSceneId);
                    NeedLoadDict.Add(staticSceneId, loadSceneInfo.AssetPathId);
                }
                return;
            }

            // 玩家进入对应的场景后。对应场景有相应的场景加载要求。
            foreach (var enterAssetId in m_EnterAssetCountDict.Keys)
            {
                StaticSceneInfoRow sceneInfo;
                if (m_SectionSceneInfos.TryGetValue(enterAssetId, out sceneInfo))
                {
                    foreach (var id in sceneInfo.LoadSceneIds)
                    {
                        if (!NeedLoadDict.ContainsKey(id))
                        {
                            var loadSceneInfo = m_StaticSceneInfo.GetRowById<StaticSceneInfoRow>(id);
                            NeedLoadDict.Add(id, loadSceneInfo.AssetPathId);
                        }
                    }
                    if (!NeedLoadDict.ContainsKey(sceneInfo.Id))
                    {
                        NeedLoadDict.Add(sceneInfo.Id, sceneInfo.AssetPathId);
                    }
                }
            }
        }

        public void UpdateAutoLoad()
        {
            foreach (var loadId in NeedLoadDict.Keys)
            {
                ShowStaticScene(loadId);
            }
        }

        public void UpdateAutoRelease()
        {
            List<int> removeScene = new List<int>();
            foreach (var item in m_LoadedSceneAssetDict)
            {
                if (!NeedLoadDict.ContainsValue(item.Key) && !m_EnterAssetCountDict.ContainsKey(item.Key))
                {
                    if (Loader.GetEntity(item.Value.Id))
                    {
                        Loader.HideEntity(item.Value.Id);
                        Log.Info($"HideEntity:{item.Key}");
                    }
                    removeScene.Add(item.Key);
                }
            }

            foreach (var id in removeScene)
            {
                m_LoadedSceneAssetDict.Remove(id);
            }

        }

        public void DumpStr()
        {
            Log.Info("load :{}");
        }

        protected override void DoUpdate()
        {
            UpdateNeedLoadDict();
            UpdateAutoLoad();
            UpdateAutoRelease();
        }

    }
}