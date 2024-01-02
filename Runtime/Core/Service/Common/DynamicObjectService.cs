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

namespace Pangoo.Core.Services
{
    public class DynamicObjectService : MainSubService
    {
        public override string ServiceName => "DynamicObject";
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
        Dictionary<string, EntityDynamicObject> m_LoadedAssetDict = new Dictionary<string, EntityDynamicObject>();
        List<string> m_LoadingAssetIds = new List<string>();


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
                Loader.HideEntity(dynamicObjectEntity.Id);
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
            if (m_LoadingAssetIds.Contains(dynamicObjectUuid))
            {

                return;
            }
            else
            {
                EntityDynamicObjectData data = EntityDynamicObjectData.Create(info.CreateEntityInfo(m_EntityGroupRow), this, info);
                m_LoadingAssetIds.Add(dynamicObjectUuid);
                Loader.ShowEntity(EnumEntity.DynamicObject,
                    (o) =>
                    {
                        if (m_LoadingAssetIds.Contains(dynamicObjectUuid))
                        {
                            m_LoadingAssetIds.Remove(dynamicObjectUuid);
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

            //通过路径ID去判断是否被加载。用来在不同的章节下用了不用的静态场景ID,但是使用不同的加载Ids
            var info = m_DynamicObjectInfo.GetRowByUuid<DynamicObjectInfoRow>(uuid);
            var AssetPathId = info.AssetPathUuid;
            if (m_LoadedAssetDict.ContainsKey(uuid))
            {
                return;
            }

            Log($"ShowDynamicObject:{uuid}");

            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if (m_LoadingAssetIds.Contains(uuid))
            {
                return;
            }
            else
            {
                EntityDynamicObjectData data = EntityDynamicObjectData.Create(info.CreateEntityInfo(m_EntityGroupRow), this, info);
                m_LoadingAssetIds.Add(uuid);
                Loader.ShowEntity(EnumEntity.DynamicObject,
                    (o) =>
                    {
                        if (m_LoadingAssetIds.Contains(uuid))
                        {
                            m_LoadingAssetIds.Remove(uuid);
                        }
                        m_LoadedAssetDict.Add(uuid, o.Logic as EntityDynamicObject);
                        callback?.Invoke(uuid);
                    },
                    data.EntityInfo,
                    data);
            }
        }

        [Button("Hide")]
        public void Hide(string uuid)
        {
            var entity = GetLoadedEntity(uuid);
            if (entity != null)
            {
                Loader.HideEntity(entity.Id);
            }
            m_LoadedAssetDict.Remove(uuid);
        }


    }
}