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
    public partial class DynamicObjectService : MainSubService
    {
        public override string ServiceName => "DynamicObjectService";
        public override int Priority => 6;


        IEntityGroupRow m_EntityGroupRow;

        EntityLoader Loader = null;

        DynamicObjectInfo m_DynamicObjectInfo;



        [ShowInInspector]
        List<string> m_GameSectionDynamicObjectUuids;

        // 需要加载的场景列表。 Key: StaticSceneId Value:AssetPathId
        [ShowInInspector]
        Dictionary<string, SubDynamicObjectEntry> NeedLoadDict;
        public Dictionary<string, SubDynamicObjectEntry> SubDynamicObjectDict;



        protected override void DoAwake()
        {
            base.DoAwake();
            DoAwakeLoadedDict();
            m_GameSectionDynamicObjectUuids = new List<string>();
            NeedLoadDict = new Dictionary<string, SubDynamicObjectEntry>();
            SubDynamicObjectDict = new Dictionary<string, SubDynamicObjectEntry>();
            Loader = EntityLoader.Create(this);
        }

        public void SetGameScetion(List<string> dynamicUuids)
        {
            m_GameSectionDynamicObjectUuids.Clear();

            if (dynamicUuids.IsNullOrEmpty()) return;

            m_GameSectionDynamicObjectUuids.AddRange(dynamicUuids);
        }


        protected override void DoStart()
        {


            m_EntityGroupRow = EntityGroupRowExtension.CreateDynamicObjectGroup();
            m_DynamicObjectInfo = GameInfoSrv.GetGameInfo<DynamicObjectInfo>();
            Debug.Log($"DoStart DynamicObjectService :{m_EntityGroupRow} m_EntityGroupRow:{m_EntityGroupRow.Name}");

        }


        public void HideAllLoaded()
        {
            foreach (var entity in AllLoadedEntity)
            {
                Loader.HideEntity(entity.Id);
                RemoveFromLoadedDict(entity.DynamicObjectUuid);
            }
        }

        public void HideEntity(string uuid)
        {
            var entity = GetLoadedEntity(uuid);
            if (entity != null && entity.Id != 0)
            {
                Loader.HideEntity(entity.Id);
                RemoveFromLoadedDict(entity.DynamicObjectUuid);
            }
        }

        public class SubDynamicObjectEntry
        {
            public EntityBase ParentEntity;
            public string SubUuid;

            public bool IsParentCharacter;
            public string Path;
        }



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
                Add2LoadingList(dynamicObjectUuid);
                Loader.ShowEntity(EnumEntity.DynamicObject,
                    (o) =>
                    {

                        Log($"GameSection DynamicObject Loaed:{info.Name}[{dynamicObjectUuid.ToShortUuid()}]");
                        var showedEntity = o.Logic as EntityDynamicObject;
                        RemoveFromLoadingList(dynamicObjectUuid);
                        AddLoadedDict(ConstString.SubDynamicObjectModule, o.Logic as EntityDynamicObject);
                        Loader.AttachEntity(showedEntity.Entity, parentEntity.Entity.Id, path);
                        showedEntity.UpdateDefaultTransform();
                        onShowSuccess?.Invoke(o.Logic as EntityDynamicObject);
                    },
                    data.EntityInfo,
                    data);
            }

        }





        public void ShowModuleDynamicObject(string moduleName, string dynamicObjectUuid, Action<EntityDynamicObject> finishCallback = null)
        {
            if (IsEntityDynamicObjectLoaded(dynamicObjectUuid))
            {
                return;
            }

            if (ContainInLoadingList(dynamicObjectUuid))
            {
                // LogWarning("Try Load Loading Asset:" + dynamicObjectUuid);
                return;
            }

            var info = m_DynamicObjectInfo.GetRowByUuid<DynamicObjectInfoRow>(dynamicObjectUuid);
            Log($"Show Module:{moduleName} DynamicObject:{info.Name}[{dynamicObjectUuid.ToShortUuid()}]");

            EntityDynamicObjectData data = EntityDynamicObjectData.Create(info.CreateEntityInfo(m_EntityGroupRow), this, info);
            Add2LoadingList(dynamicObjectUuid);
            var serialId = Loader.ShowEntity(EnumEntity.DynamicObject,
                (o) =>
                {
                    Log($"GameSection DynamicObject Loaed:{info.Name}[{dynamicObjectUuid.ToShortUuid()}]");
                    RemoveFromLoadingList(dynamicObjectUuid);
                    AddLoadedDict(moduleName, o.Logic as EntityDynamicObject);
                    finishCallback?.Invoke(o.Logic as EntityDynamicObject);
                },
                data.EntityInfo,
                data);
        }




        [Button("Show")]
        public void ShowGameSectionDynamicObject(string uuid, Action<EntityDynamicObject> callback = null)
        {

            ShowModuleDynamicObject(ConstString.GameSectionModule, uuid, callback);
        }




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
                    ShowGameSectionDynamicObject(kv.Key);
                }
            }
        }

        public void UpdateAutoRelease()
        {
            List<string> removeDynamicObject = new List<string>();
            var gameSectionLoadedDict = GetModuleLoaded(ConstString.GameSectionModule);
            if (gameSectionLoadedDict != null)
            {
                foreach (var item in gameSectionLoadedDict)
                {
                    if (!NeedLoadDict.ContainsKey(item.Key))
                    {
                        removeDynamicObject.Add(item.Key);
                    }
                }
            }

            var subDynamicObjectLoadedDict = GetModuleLoaded(ConstString.SubDynamicObjectModule);
            if (subDynamicObjectLoadedDict != null)
            {
                foreach (var kv in subDynamicObjectLoadedDict)
                {
                    if (kv.Value == null)
                    {
                        if (!NeedLoadDict.ContainsKey(kv.Key))
                        {
                            removeDynamicObject.Add(kv.Key);
                        }
                    }

                }
            }

            if (removeDynamicObject.Count == 0)
            {
                return;
            }

            foreach (var removeUuid in removeDynamicObject)
            {
                Log($"Hide[{removeUuid.ToShortUuid()}]");
                HideEntity(removeUuid);
                RemoveFromLoadedDict(removeUuid);
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