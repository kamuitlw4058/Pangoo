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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace Pangoo.Core.Services
{

    public abstract class EntityLoaderService<TData> : MainSubService where TData : EntityData
    {
        public override string ServiceName => "EntityLoaderService";
        public override int Priority => 5;

        protected virtual IEntityGroupRow EntityGroupRow { get; }

        EntityLoader Loader = null;

        [ShowInInspector]
        [HideIf("@this.m_LoadingEntityList == null || this.m_LoadingEntityList.Count == 0 ")]
        List<string> m_LoadingEntityList;

        public List<string> LoadingEntityList
        {
            get
            {
                return m_LoadingEntityList;
            }
        }

        [ShowInInspector]
        [HideIf("@this.m_RemoveEntityList == null || this.m_RemoveEntityList.Count == 0 ")]
        List<string> m_RemoveEntityList;


        [ShowInInspector]
        Dictionary<string, EntityLoaderData> m_LoadedEntityDict = new Dictionary<string, EntityLoaderData>();

        public Dictionary<string, EntityLoaderData> LoadedEntityDict
        {
            get
            {
                return m_LoadedEntityDict;
            }
        }



        [ShowInInspector]
        Dictionary<string, List<EntityLoaderData>> m_LoadedInfoEntityDict = new Dictionary<string, List<EntityLoaderData>>();

        [ShowInInspector]
        Dictionary<string, List<EntityLoaderData>> m_LoadedAssetPathEntityDict = new Dictionary<string, List<EntityLoaderData>>();

        [ShowInInspector]
        protected Dictionary<string, TData> EntityDataDict = new Dictionary<string, TData>();


        public class EntityLoaderData : IReference
        {
            public TData EntityData;

            public EntityBase Entity;

            public void Clear()
            {
                EntityData = null;
                Entity = null;

            }

            public static EntityLoaderData Create(TData data, EntityBase entity)
            {
                EntityLoaderData entityLoaderData = ReferencePool.Acquire<EntityLoaderData>();
                entityLoaderData.EntityData = data;
                entityLoaderData.Entity = entity;
                return entityLoaderData;
            }
        }



        protected override void DoAwake()
        {
            base.DoAwake();

            m_LoadingEntityList = new List<string>();
            m_RemoveEntityList = new List<string>();
            Loader = EntityLoader.Create(this);

        }

        protected override void DoStart()
        {

        }

        public abstract TData GetEntityData(string uuid);

        public bool IsEntityLoadedData(string uuid)
        {
            var entityData = GetEntityData(uuid);
            return GetEntityLoadedData(entityData) != null;
        }

        public string ShowEntity(string infoUuid, EntityBase parentEntity = null, string path = null, string refName = null, Action<EntityBase> onSucess = null, Action<TData> onFailed = null)
        {
            var data = GetEntityData(infoUuid);
            var InstanceUuid = data.InstanceUuid;
            ShowEntity(data, (o) =>
            {
                Log($"On Show Entity Sucess!:Info Uuid{infoUuid},Instance Uuid:{InstanceUuid}");
                onSucess?.Invoke(o);
            }, (o) =>
            {
                Log($"On Show Entity Failed!:Info Uuid{infoUuid},Instance Uuid:{InstanceUuid}");
                onFailed?.Invoke(data);
            }, overrideData: true, refName: refName, parentEntity: parentEntity, path: path
            );
            return InstanceUuid;
        }

        public void HideEntity(string uuid, string refName = null)
        {
            var data = GetEntityData(uuid);
            if (data != null)
            {
                HideEntity(data, refName);
            }
        }


        public void ShowEntity(TData entityData, Action<EntityBase> onSucess, Action<TData> onFailed, bool overrideData = false, string refName = null, EntityBase parentEntity = null, string path = null)
        {
            var uuid = entityData.Uuid;
            if (uuid != null)
            {
                var applyRefName = refName ?? ServiceName;
                entityData.AddRef(applyRefName);

                if (m_LoadingEntityList.Contains(uuid))
                {
                    return;
                }

                if (m_LoadedEntityDict.TryGetValue(uuid, out EntityLoaderData loadedData))
                {
                    if (overrideData)
                    {
                        string oldUuid = loadedData.EntityData.Uuid;
                        string oldInfoUuid = loadedData.EntityData.InfoUuid;

                        bool infoChanged = !oldInfoUuid.Equals(entityData.InfoUuid);

                        loadedData.EntityData = entityData;

                        if (infoChanged)
                        {
                            m_LoadedInfoEntityDict.RemoveFromDictList(oldInfoUuid, loadedData);
                            m_LoadedInfoEntityDict.AddToDictList(loadedData.EntityData.InfoUuid, loadedData);
                        }
                        loadedData.Entity.OnUpdateEntityData(entityData);
                    }

                    return;
                }

                m_LoadingEntityList.Add(uuid);
                Loader.ShowEntity(entityData.EntityType, (o) =>
                {
                    if (m_LoadingEntityList.Contains(uuid))
                    {
                        m_LoadingEntityList.Remove(uuid);
                    }

                    var entity = o.Logic as EntityBase;
                    if (m_RemoveEntityList.Contains(uuid))
                    {
                        var entityId = entity?.Entity?.Id;
                        if (entityId != null)
                        {
                            Loader.HideEntity(entityId.Value);
                        }
                        return;
                    }

                    EntityLoaderData entityLoaderData = EntityLoaderData.Create(entityData, entity);
                    if (entityLoaderData != null)
                    {
                        m_LoadedEntityDict.Add(uuid, entityLoaderData);
                        m_LoadedInfoEntityDict.AddToDictList(entityLoaderData.EntityData.InfoUuid, entityLoaderData);
                        m_LoadedAssetPathEntityDict.AddToDictList(entityLoaderData.EntityData.AssetPathUuid, entityLoaderData);
                        if (parentEntity != null)
                        {
                            Loader.AttachEntity(entity.Entity, parentEntity.Entity.Id, path);
                        }
                        onSucess?.Invoke(entity);
                    }
                    else
                    {
                        onFailed?.Invoke(entityData);
                    }

                }, entityData, entityData);
            }
        }

        public void HideEntity(TData entityData, string refName = null)
        {
            var uuid = entityData.Uuid;
            if (uuid != null)
            {
                var applyRefName = refName ?? ServiceName;
                entityData.RemoveRef(applyRefName);

                if (entityData.RefCount != 0)
                {
                    return;
                }

                if (m_LoadingEntityList.Contains(uuid))
                {
                    m_RemoveEntityList.Add(uuid);
                    return;
                }

                if (m_LoadedEntityDict.TryGetValue(uuid, out EntityLoaderData data))
                {
                    m_LoadedEntityDict.Remove(uuid);
                    m_LoadedInfoEntityDict.RemoveFromDictList(data.EntityData.InfoUuid, data);
                    m_LoadedAssetPathEntityDict.RemoveFromDictList(data.EntityData.AssetPathUuid, data);

                    var entityId = data?.Entity?.Id;
                    if (entityId != null)
                    {
                        Loader.HideEntity(entityId.Value);
                    }

                    ReferencePool.Release(data);

                }

            }
        }

        public EntityLoaderData GetEntityLoadedData(EntityData entityData)
        {
            if (entityData == null) return null;

            var uuid = entityData.Uuid;
            if (uuid != null)
            {
                if (m_LoadedEntityDict.TryGetValue(uuid, out EntityLoaderData loaderData))
                {
                    return loaderData;
                }
            }
            return null;
        }


        public EntityBase GetLoadedEntity(string uuid)
        {
            var entityData = GetEntityData(uuid);
            if (entityData != null)
            {
                return GetLoadedEntity(entityData);
            }
            return null;
        }



        public EntityBase GetLoadedEntity(EntityData entityData)
        {
            if (entityData == null) return null;

            var uuid = entityData.Uuid;
            if (uuid != null)
            {
                if (m_LoadedEntityDict.TryGetValue(uuid, out EntityLoaderData loaderData))
                {
                    return loaderData.Entity;
                }
            }
            return null;
        }


        protected override void DoUpdate()
        {

        }

    }
}