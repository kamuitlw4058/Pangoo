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
    [Serializable]
    public class StaticSceneService : MainSubService
    {
        public override string ServiceName => "StaticSceneService";
        public override int Priority => 5;

        IEntityGroupRow m_EntityGroupRow;

        // StaticSceneTable m_StaticSceneTable;

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


        [ShowInInspector]

        Dictionary<string, StaticSceneInfoRow> m_SectionSceneInfos;

        [ShowInInspector]
        List<string> m_DynamicStaticSceneUuids;

        [ShowInInspector]
        List<string> m_HoldStaticSceneUuids;


        [ShowInInspector]
        List<string> m_InitStaticSceneUuids;


        // 需要加载的场景列表。 Key: StaticSceneId Value:AssetPathId
        [ShowInInspector]
        Dictionary<string, string> NeedLoadDict;





        public Tuple<string, string> m_SectionChange;


        public bool SectionInited = false;

        public Action OnInitSceneLoaded;



        protected override void DoAwake()
        {
            base.DoAwake();
            NeedLoadDict = new Dictionary<string, string>();
            m_InitStaticSceneUuids = new List<string>();
            m_HoldStaticSceneUuids = new List<string>();
            m_DynamicStaticSceneUuids = new List<string>();
            m_SectionSceneInfos = new Dictionary<string, StaticSceneInfoRow>();
            m_LoadingAssetUuids = new List<string>();
            m_LoadedSceneAssetDict = new Dictionary<string, EntityStaticScene>();
            m_EnterAssetCountDict = new Dictionary<string, int>();


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


        public bool IsInGameSectionConfig(string uuid)
        {
            if (m_InitStaticSceneUuids.Contains(uuid))
            {
                return true;
            }

            if (m_HoldStaticSceneUuids.Contains(uuid))
            {
                return true;
            }

            if (m_DynamicStaticSceneUuids.Contains(uuid))
            {
                return true;
            }

            return false;
        }


        public void SetGameScetion(List<string> dynamicUuids, List<string> holdUuids, List<string> initUuids)
        {
            SectionInited = false;
            m_DynamicStaticSceneUuids.Clear();
            m_DynamicStaticSceneUuids.AddRange(dynamicUuids);

            m_SectionSceneInfos.Clear();
            foreach (var uuid in dynamicUuids)
            {
                var sceneInfo = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(uuid);
                // Debug.Log($"uuid:{uuid}, sceneInfo:{sceneInfo}");
                m_SectionSceneInfos.Add(sceneInfo.AssetPathUuid, sceneInfo);
            }

            foreach (var uuid in holdUuids)
            {
                var sceneInfo = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(uuid);
                if (!m_SectionSceneInfos.ContainsKey(sceneInfo.AssetPathUuid))
                {
                    m_SectionSceneInfos.Add(sceneInfo.AssetPathUuid, sceneInfo);
                }
            }

            foreach (var uuid in initUuids)
            {
                var sceneInfo = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(uuid);
                if (!m_SectionSceneInfos.ContainsKey(sceneInfo.AssetPathUuid))
                {
                    m_SectionSceneInfos.Add(sceneInfo.AssetPathUuid, sceneInfo);
                }
            }

            m_HoldStaticSceneUuids.Clear();
            m_HoldStaticSceneUuids.AddRange(holdUuids);

            m_InitStaticSceneUuids.Clear();
            m_InitStaticSceneUuids.AddRange(initUuids);
        }

        public void SetGameSectionChange(Tuple<string, string> value)
        {
            m_SectionChange = value;
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


        public bool IsLoadedScene(List<string> uuids)
        {
            bool allInited = true;
            foreach (var sceneUuid in uuids)
            {
                var info = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(sceneUuid);
                if (!m_LoadedSceneAssetDict.ContainsKey(info.AssetPathUuid))
                {
                    allInited = false;
                    break;
                }
            }
            return allInited;
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

            if (!IsInGameSectionConfig(staticSceneUuid))
            {
                return;
            }

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
                        m_LoadedSceneAssetDict.Add(AssetPathUuid, o.Logic as EntityStaticScene);
                    },
                    data.EntityInfo,
                    data);
            }
        }
        public StaticSceneInfoRow GetInfoRowByAssetUuid(string assetUuid)
        {
            StaticSceneInfoRow sceneInfo;
            if (assetUuid.IsNullOrWhiteSpace()) return null;

            if (m_SectionSceneInfos.TryGetValue(assetUuid, out sceneInfo))
            {
                return sceneInfo;
            }

            return null;
        }

        public void UpdateNeedLoadDict()
        {
            NeedLoadDict.Clear();

            //章节服务会设置常驻的场景ID。用来打开该场景下通用的设置。
            foreach (var keepSceneUuid in m_HoldStaticSceneUuids)
            {
                StaticSceneInfoRow sceneInfo = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(keepSceneUuid);
                if (!NeedLoadDict.ContainsKey(sceneInfo.Uuid))
                {
                    NeedLoadDict.Add(sceneInfo.Uuid, sceneInfo.AssetPathUuid);
                }
            }

            if (m_EnterAssetCountDict.Count == 0)
            {
                foreach (var staticSceneUuid in m_InitStaticSceneUuids)
                {
                    var loadSceneInfo = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(staticSceneUuid);
                    // Debug.Log($"loadSceneInfo:{loadSceneInfo} staticSceneUuid:{staticSceneUuid}");
                    NeedLoadDict.Add(staticSceneUuid, loadSceneInfo.AssetPathUuid);
                }
                return;
            }

            if (!SectionInited)
            {
                foreach (var sceneUuid in m_InitStaticSceneUuids)
                {
                    StaticSceneInfoRow sceneInfo = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(sceneUuid);
                    if (!NeedLoadDict.ContainsKey(sceneInfo.Uuid))
                    {
                        NeedLoadDict.Add(sceneInfo.Uuid, sceneInfo.AssetPathUuid);
                    }
                }
            }

            // 玩家进入对应的场景后。对应场景有相应的场景加载要求。
            foreach (var enterAssetUuid in m_EnterAssetCountDict.Keys)
            {
                StaticSceneInfoRow sceneInfo = GetInfoRowByAssetUuid(enterAssetUuid);
                if (sceneInfo != null)
                {
                    foreach (var loadSceneUuid in sceneInfo.LoadSceneUuids)
                    {
                        if (!NeedLoadDict.ContainsKey(loadSceneUuid))
                        {
                            var loadSceneInfo = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(loadSceneUuid);
                            NeedLoadDict.Add(loadSceneUuid, loadSceneInfo.AssetPathUuid);
                        }
                    }
                    if (!NeedLoadDict.ContainsKey(sceneInfo.Uuid))
                    {
                        NeedLoadDict.Add(sceneInfo.Uuid, sceneInfo.AssetPathUuid);
                    }
                }
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
                        Log($"HideEntity:{item.Key}");
                    }
                    removeScene.Add(item.Key);
                }
            }

            foreach (var removeUuid in removeScene)
            {
                m_LoadedSceneAssetDict.Remove(removeUuid);
            }

        }

        public void DumpStr()
        {
            Log("load :{}");
        }

        bool IsAllGameSectionSceneLoaded()
        {

            for (int i = 0; i < NeedLoadDict.Count; i++)
            {
                var key = NeedLoadDict.Values.ToList()[i];
                if (!m_LoadedSceneAssetDict.ContainsKey(key))
                {
                    return false;
                }
            }

            return true;

        }

        protected override void DoUpdate()
        {
            UpdateNeedLoadDict();
            UpdateAutoLoad();
            UpdateAutoRelease();

            if (!SectionInited)
            {
                if (IsAllGameSectionSceneLoaded())
                {
                    SectionInited = true;
                    Log($"Section All Loaded!");
                    OnInitSceneLoaded?.Invoke();
                }
            }
        }

    }
}