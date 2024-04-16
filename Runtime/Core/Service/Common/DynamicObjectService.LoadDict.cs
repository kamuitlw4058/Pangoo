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
    public partial class DynamicObjectService
    {
        [ShowInInspector]
        public List<string> m_LoadingAssetUuids;


        [ShowInInspector]
        [Searchable]
        [FoldoutGroup("加载列表")]
        [LabelText("加载列表")]
        public Dictionary<string, EntityDynamicObject> m_LoadedAssetDict;

        [ShowInInspector]
        [Searchable]
        [LabelText("加载模块列表")]
        [FoldoutGroup("加载列表")]
        public Dictionary<string, Dictionary<string, EntityDynamicObject>> m_LoadedModuleAssetDict;

        public void DoAwakeLoadedDict()
        {
            m_LoadedAssetDict = new Dictionary<string, EntityDynamicObject>();
            m_LoadedModuleAssetDict = new Dictionary<string, Dictionary<string, EntityDynamicObject>>();
            m_LoadingAssetUuids = new List<string>();
        }

        public bool IsEntityDynamicObjectLoaded(string dynamicObjectUuid)
        {
            if (m_LoadedAssetDict.TryGetValue(dynamicObjectUuid, out EntityDynamicObject dat))
            {
                return true;
            }

            return false;
        }

        public bool ContainInLoadingList(string dynamicObjectUuid)
        {
            if (m_LoadingAssetUuids.Contains(dynamicObjectUuid))
            {
                return true;
            }

            return false;
        }

        public void Add2LoadingList(string dynamicObjectUuid)
        {
            m_LoadingAssetUuids.Add(dynamicObjectUuid);
        }

        public void RemoveFromLoadingList(string dynamicObjectUuid)
        {
            if (m_LoadingAssetUuids.Contains(dynamicObjectUuid))
            {
                m_LoadingAssetUuids.Remove(dynamicObjectUuid);
            }
        }

        public Dictionary<string, EntityDynamicObject> GetModuleLoaded(string moduleName)
        {
            if (m_LoadedModuleAssetDict.TryGetValue(moduleName, out Dictionary<string, EntityDynamicObject> dict))
            {
                return dict;
            }
            return null;
        }


        public void AddLoadedDict(string moduleName, EntityDynamicObject entity)
        {
            if (entity == null) return;

            if (!m_LoadedAssetDict.ContainsKey(entity.DynamicObjectUuid))
            {
                m_LoadedAssetDict.Add(entity.DynamicObjectUuid, entity);
            }

            if (m_LoadedModuleAssetDict.TryGetValue(moduleName, out Dictionary<string, EntityDynamicObject> dict))
            {
                dict.Add(entity.DoData.InfoUuid, entity);
            }
            else
            {
                var newDict = new Dictionary<string, EntityDynamicObject>
                {
                    { entity.DynamicObjectUuid, entity }
                };
                m_LoadedModuleAssetDict.Add(moduleName, newDict);
            }

        }

        public bool CheckGameSectionLoaded
        {
            get
            {
                if (m_GameSectionDynamicObjectUuids.Count == 0)
                {
                    return true;
                }

                var moduleDict = GetModuleLoaded(ConstString.GameSectionModule);
                if (moduleDict == null)
                {
                    return false;
                }


                for (int i = 0; i < m_GameSectionDynamicObjectUuids.Count; i++)
                {
                    if (!moduleDict.ContainsKey(m_GameSectionDynamicObjectUuids[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public void RemoveFromLoadedDict(string dynamicObjectUuid)
        {
            if (m_LoadedAssetDict.ContainsKey(dynamicObjectUuid))
            {
                m_LoadedAssetDict.Remove(dynamicObjectUuid);
            }

            for (int i = 0; i < m_LoadedModuleAssetDict.Count; i++)
            {
                foreach (Dictionary<string, EntityDynamicObject> dict in m_LoadedModuleAssetDict.Values)
                {
                    if (dict.TryGetValue(dynamicObjectUuid, out EntityDynamicObject entity))
                    {
                        dict.Remove(dynamicObjectUuid);
                    }
                }

            }
        }

        public List<EntityDynamicObject> AllLoadedEntity
        {
            get
            {
                return m_LoadedAssetDict.Values.ToList();
            }
        }

        public EntityDynamicObject GetLoadedEntity(string uuid)
        {
            if (m_LoadedAssetDict.TryGetValue(uuid, out EntityDynamicObject dynamicObjectEntity))
            {
                return dynamicObjectEntity;
            }

            return null;
        }


    }
}