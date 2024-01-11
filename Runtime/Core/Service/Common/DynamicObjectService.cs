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
    public class DynamicObjectService : MainSubService
    {
        public override string ServiceName => "DynamicObjectService";
        public override int Priority => 6;

        public ExcelTableService TableService
        {
            get
            {
                return ExcelTableSrv;
            }
        }



        IEntityGroupRow m_EntityGroupRow;

        EntityLoader Loader = null;

        DynamicObjectInfo m_DynamicObjectInfo;

        [ShowInInspector]
        [Searchable]
        Dictionary<string, EntityDynamicObject> m_LoadedAssetDict = new Dictionary<string, EntityDynamicObject>();
        List<string> m_LoadingAssetUuids = new List<string>();

        [ShowInInspector]
        List<string> m_GameSectionDynamicObjectUuids = new List<string>();

        public void SetGameScetion(List<string> dynamicUuids)
        {
            DynamicObjectInited = false;
            m_GameSectionDynamicObjectUuids.Clear();
            m_GameSectionDynamicObjectUuids.AddRange(dynamicUuids);

        }
        public bool DynamicObjectInited = false;

        public Action OnGameSectionDynamicObjectLoaded;






        protected override void DoStart()
        {


            m_EntityGroupRow = EntityGroupRowExtension.CreateDynamicObjectGroup();
            m_DynamicObjectInfo = GameInfoSrv.GetGameInfo<DynamicObjectInfo>();
            Debug.Log($"DoStart DynamicObjectService :{m_EntityGroupRow} m_EntityGroupRow:{m_EntityGroupRow.Name}");

        }

        public List<string> GetLoadedUuids()
        {
            return m_LoadedAssetDict.Keys.ToList();
        }

        public EntityDynamicObject GetLoadedEntity(string uuid)
        {
            if (m_LoadedAssetDict.TryGetValue(uuid, out EntityDynamicObject var))
            {
                return var;
            }
            return null;
        }

        public void HideAllLoaded()
        {
            var ids = m_LoadedAssetDict.Keys.ToList();
            foreach (var id in ids)
            {
                Loader.HideEntity(m_LoadedAssetDict[id].Id);
                m_LoadedAssetDict.Remove(id);
            }
        }

        public void HideEntity(string uuid)
        {
            if (m_LoadedAssetDict.TryGetValue(uuid, out EntityDynamicObject dynamicObjectEntity))
            {
                if (dynamicObjectEntity.Id != 0)
                {
                    Loader.HideEntity(dynamicObjectEntity.Id);
                }
                m_LoadedAssetDict.Remove(uuid);
            }
        }

        public class SubDynamicObjectEntry
        {
            public EntityBase ParentEntity;
            public string SubUuid;

            public bool IsParentCharacter;
            public string Path;
        }

        public Dictionary<string, SubDynamicObjectEntry> SubDynamicObjectDict = new Dictionary<string, SubDynamicObjectEntry>();

        void AddSubDynamicObjectDict(SubDynamicObjectEntry entry)
        {
            if (entry == null)
            {
                LogError($"AddSubDynamicObjectDict Some null  :{entry},{entry?.SubUuid}");
                return;
            }

            if (!SubDynamicObjectDict.ContainsKey(entry.SubUuid))
            {
                SubDynamicObjectDict.Add(entry.SubUuid, entry);
            }

        }


        public void ShowSubDynamicObject(string dynamicObjectUuid, string path, EntityBase parentEntity, bool IsParentCharacter, Action<EntityDynamicObject> onShowSuccess)
        {
            if (Loader == null)
            {
                Loader = EntityLoader.Create(this);
            }

            if (parentEntity == null)
            {
                LogError($"ShowSubDynamicObject Failed: parentEntity is null.parentEntity");

                return;
            }



            var entry = new SubDynamicObjectEntry()
            {
                ParentEntity = parentEntity,
                SubUuid = dynamicObjectUuid,
                Path = path,
                IsParentCharacter = IsParentCharacter,
            };

            if (m_LoadedAssetDict.TryGetValue(dynamicObjectUuid, out EntityDynamicObject entity))
            {
                Loader.AttachEntity(entity.Entity, parentEntity.Entity.Id, path);
                AddSubDynamicObjectDict(entry);
                return;
            }




            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if (m_LoadingAssetUuids.Contains(dynamicObjectUuid))
            {

                return;
            }
            else
            {
                AddSubDynamicObjectDict(entry);
                Log($"ShowSubDynamicObject:{dynamicObjectUuid}");
                var info = m_DynamicObjectInfo.GetRowByUuid<DynamicObjectInfoRow>(dynamicObjectUuid);
                EntityDynamicObjectData data = EntityDynamicObjectData.Create(info.CreateEntityInfo(m_EntityGroupRow), this, info);
                m_LoadingAssetUuids.Add(dynamicObjectUuid);
                Loader.ShowEntity(EnumEntity.DynamicObject,
                    (o) =>
                    {
                        if (m_LoadingAssetUuids.Contains(dynamicObjectUuid))
                        {
                            m_LoadingAssetUuids.Remove(dynamicObjectUuid);
                        }
                        var showedEntity = o.Logic as EntityDynamicObject;
                        m_LoadedAssetDict.Add(dynamicObjectUuid, showedEntity);
                        AddSubDynamicObjectDict(entry);
                        Loader.AttachEntity(showedEntity.Entity, parentEntity.Entity.Id, path);
                        showedEntity.UpdateDefaultTransform();
                        onShowSuccess?.Invoke(o.Logic as EntityDynamicObject);
                    },
                    data.EntityInfo,
                    data);
            }


        }

        public void ShowSubDynamicObject(string dynamicObjectUuid, int parentEntityId, string path, Action<EntityDynamicObject> onShowSuccess)
        {
            if (Loader == null)
            {
                Loader = EntityLoader.Create(this);
            }

            //通过路径ID去判断是否被加载。用来在不同的章节下用了不用的静态场景ID,但是使用不同的加载Ids
            var info = m_DynamicObjectInfo.GetRowByUuid<DynamicObjectInfoRow>(dynamicObjectUuid);

            EntityDynamicObject entity;

            if (m_LoadedAssetDict.TryGetValue(dynamicObjectUuid, out entity))
            {
                Loader.AttachEntity(entity.Entity, parentEntityId, path);
                return;
            }

            Log($"ShowDynamicObject:{dynamicObjectUuid}");

            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if (m_LoadingAssetUuids.Contains(dynamicObjectUuid))
            {

                return;
            }
            else
            {
                EntityDynamicObjectData data = EntityDynamicObjectData.Create(info.CreateEntityInfo(m_EntityGroupRow), this, info);
                m_LoadingAssetUuids.Add(dynamicObjectUuid);
                Loader.ShowEntity(EnumEntity.DynamicObject,
                    (o) =>
                    {
                        if (m_LoadingAssetUuids.Contains(dynamicObjectUuid))
                        {
                            m_LoadingAssetUuids.Remove(dynamicObjectUuid);
                        }
                        var showedEntity = o.Logic as EntityDynamicObject;
                        m_LoadedAssetDict.Add(dynamicObjectUuid, showedEntity);
                        Loader.AttachEntity(showedEntity.Entity, parentEntityId, path);
                        showedEntity.UpdateDefaultTransform();
                        onShowSuccess?.Invoke(o.Logic as EntityDynamicObject);

                    },
                    data.EntityInfo,
                    data);
            }
        }


        [Button("Show")]
        public void ShowDynamicObject(string uuid, Action<string> callback = null)
        {
            if (Loader == null)
            {
                Loader = EntityLoader.Create(this);
            }

            if (m_LoadedAssetDict.ContainsKey(uuid))
            {
                callback?.Invoke(uuid);
                return;
            }

            if (m_LoadingAssetUuids.Contains(uuid))
            {
                return;
            }
            else
            {
                var info = m_DynamicObjectInfo.GetRowByUuid<DynamicObjectInfoRow>(uuid);
                Log($"Show GameSection DynamicObject:{info.Name}[{uuid.ToShortUuid()}]");

                EntityDynamicObjectData data = EntityDynamicObjectData.Create(info.CreateEntityInfo(m_EntityGroupRow), this, info);
                m_LoadingAssetUuids.Add(uuid);
                Loader.ShowEntity(EnumEntity.DynamicObject,
                    (o) =>
                    {
                        if (m_LoadingAssetUuids.Contains(uuid))
                        {
                            m_LoadingAssetUuids.Remove(uuid);
                        }
                        Log($"GameSection DynamicObject Loaed:{info.Name}[{uuid.ToShortUuid()}]");
                        m_LoadedAssetDict.Add(uuid, o.Logic as EntityDynamicObject);
                        callback?.Invoke(uuid);
                    },
                    data.EntityInfo,
                    data);
            }
        }



        // 需要加载的场景列表。 Key: StaticSceneId Value:AssetPathId
        [ShowInInspector]
        Dictionary<string, SubDynamicObjectEntry> NeedLoadDict = new Dictionary<string, SubDynamicObjectEntry>();


        public void UpdateNeedLoadDict()
        {
            NeedLoadDict.Clear();

            //章节服务会设置常驻的场景ID。用来打开该场景下通用的设置。
            foreach (var gameSectionDynamicObjectUuid in m_GameSectionDynamicObjectUuids)
            {
                var info = m_DynamicObjectInfo.GetRowByUuid<DynamicObjectInfoRow>(gameSectionDynamicObjectUuid);
                if (!NeedLoadDict.ContainsKey(info.Uuid))
                {
                    NeedLoadDict.Add(info.Uuid, null);
                }

            }

            foreach (var kv in SubDynamicObjectDict)
            {

                if (!NeedLoadDict.ContainsKey(kv.Key) && kv.Value.ParentEntity != null)
                {
                    NeedLoadDict.Add(kv.Key, kv.Value);
                }
            }
        }

        public void UpdateAutoLoad()
        {
            foreach (var kv in NeedLoadDict)
            {
                if (kv.Value == null)
                {
                    ShowDynamicObject(kv.Key);
                }
            }
        }

        public void UpdateAutoRelease()
        {
            List<string> removeDynamicObject = new List<string>();
            foreach (var item in m_LoadedAssetDict)
            {
                if (!NeedLoadDict.ContainsKey(item.Key))
                {
                    removeDynamicObject.Add(item.Key);
                }
            }

            foreach (var removeUuid in removeDynamicObject)
            {
                Log($"Hide[{removeUuid.ToShortUuid()}]");
                HideEntity(removeUuid);
                m_LoadedAssetDict.Remove(removeUuid);
            }

        }

        bool IsAllGameSectionDynamicObjectLoaded()
        {

            for (int i = 0; i < m_GameSectionDynamicObjectUuids.Count; i++)
            {
                if (!m_LoadedAssetDict.ContainsKey(m_GameSectionDynamicObjectUuids[i]))
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
            if (!DynamicObjectInited)
            {
                if (IsAllGameSectionDynamicObjectLoaded())
                {
                    DynamicObjectInited = true;
                    Log($"GameSection DynamicObject All Loaded!");
                    OnGameSectionDynamicObjectLoaded?.Invoke();

                }
            }
        }

    }
}