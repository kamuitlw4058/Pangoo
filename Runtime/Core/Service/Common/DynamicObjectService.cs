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
        Dictionary<int, EntityDynamicObject> m_LoadedAssetDict = new Dictionary<int, EntityDynamicObject>();
        List<int> m_LoadingAssetIds = new List<int>();


        protected override void DoStart()
        {


            m_EntityGroupRow = EntityGroupRowExtension.CreateDynamicObjectGroup();
            m_DynamicObjectInfo = GameInfoSrv.GetGameInfo<DynamicObjectInfo>();
            Debug.Log($"DoStart DynamicObjectService :{m_EntityGroupRow} m_EntityGroupRow:{m_EntityGroupRow.Name}");
        }

        public EntityDynamicObject GetLoadedEntity(int id)
        {
            if (m_LoadedAssetDict.TryGetValue(id, out EntityDynamicObject var))
            {
                return var;
            }
            return null;
        }

        public int[] GetIds()
        {
            return m_LoadedAssetDict.Keys.ToArray();
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

        public void HideLoaded(int id)
        {
            Loader.HideEntity(m_LoadedAssetDict[id].Id);
            m_LoadedAssetDict.Remove(id);
        }

        public void ShowSubDynamicObject(int dynamicObjectId, int parentEntityId, string path, Action<EntityDynamicObject> onShowSuccess)
        {
            if (Loader == null)
            {
                Loader = EntityLoader.Create(this);
            }

            //通过路径ID去判断是否被加载。用来在不同的章节下用了不用的静态场景ID,但是使用不同的加载Ids
            var info = m_DynamicObjectInfo.GetRowById<DynamicObjectInfoRow>(dynamicObjectId);

            EntityDynamicObject entity;

            if (m_LoadedAssetDict.TryGetValue(dynamicObjectId, out entity))
            {
                Loader.AttachEntity(entity.Entity, parentEntityId, path);
                return;
            }

            Log($"ShowDynamicObject:{dynamicObjectId}");

            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if (m_LoadingAssetIds.Contains(dynamicObjectId))
            {

                return;
            }
            else
            {
                EntityDynamicObjectData data = EntityDynamicObjectData.Create(info.CreateEntityInfo(m_EntityGroupRow), this, info);
                m_LoadingAssetIds.Add(dynamicObjectId);
                Loader.ShowEntity(EnumEntity.DynamicObject,
                    (o) =>
                    {
                        if (m_LoadingAssetIds.Contains(dynamicObjectId))
                        {
                            m_LoadingAssetIds.Remove(dynamicObjectId);
                        }
                        var showedEntity = o.Logic as EntityDynamicObject;
                        m_LoadedAssetDict.Add(dynamicObjectId, showedEntity);
                        Loader.AttachEntity(showedEntity.Entity, parentEntityId, path);
                        showedEntity.UpdateDefaultTransform();
                        onShowSuccess?.Invoke(o.Logic as EntityDynamicObject);

                    },
                    data.EntityInfo,
                    data);
            }
        }


        [Button("Show")]
        public void ShowDynamicObject(int id, Action<int> callback = null)
        {
            if (Loader == null)
            {
                Loader = EntityLoader.Create(this);
            }

            //通过路径ID去判断是否被加载。用来在不同的章节下用了不用的静态场景ID,但是使用不同的加载Ids
            var info = m_DynamicObjectInfo.GetRowById<DynamicObjectInfoRow>(id);
            var AssetPathId = info.AssetPathId;
            if (m_LoadedAssetDict.ContainsKey(id))
            {
                return;
            }


            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if (m_LoadingAssetIds.Contains(id))
            {
                return;
            }
            else
            {
                Log($"ShowDynamicObject:{id}");
                EntityDynamicObjectData data = EntityDynamicObjectData.Create(info.CreateEntityInfo(m_EntityGroupRow), this, info);
                m_LoadingAssetIds.Add(id);
                Loader.ShowEntity(EnumEntity.DynamicObject,
                    (o) =>
                    {
                        if (m_LoadingAssetIds.Contains(id))
                        {
                            m_LoadingAssetIds.Remove(id);
                        }
                        m_LoadedAssetDict.Add(id, o.Logic as EntityDynamicObject);
                        callback?.Invoke(id);
                    },
                    data.EntityInfo,
                    data);
            }
        }

        [Button("Hide")]
        public void Hide(int id)
        {
            var entity = GetLoadedEntity(id);
            if (entity != null)
            {
                Loader.HideEntity(entity.Id);
            }
            m_LoadedAssetDict.Remove(id);
        }


    }
}