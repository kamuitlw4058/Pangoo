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
    public class DynamicObjectManagerService : BaseService
    {
        public override int Priority => 6;

        ExcelTableService m_ExcelTableService;


        public ExcelTableService TableService
        {
            get
            {
                return m_ExcelTableService;
            }
        }


        EntityGroupTable m_EntityGroupTable;

        EntityGroupTable.EntityGroupRow m_EntityGroupRow;


        DynamicObjectTable m_DynamicObjectTable;


        EntityLoader Loader = null;

        GameInfoService m_GameInfoService;


        DynamicObjectInfo m_DynamicObjectInfo;

        [ShowInInspector]
        Dictionary<int, EntityDynamicObject> m_LoadedAssetDict = new Dictionary<int, EntityDynamicObject>();
        List<int> m_LoadingAssetIds = new List<int>();

        protected override void DoAwake()
        {
            base.DoAwake();

            m_ExcelTableService = Parent.GetService<ExcelTableService>();
            m_GameInfoService = Parent.GetService<GameInfoService>();
        }


        protected override void DoStart()
        {

            m_DynamicObjectTable = m_ExcelTableService.GetExcelTable<DynamicObjectTable>();
            m_EntityGroupTable = m_ExcelTableService.GetExcelTable<EntityGroupTable>();

            m_EntityGroupRow = EntityGroupRowExtension.CreateDynamicObjectGroup();

            m_DynamicObjectInfo = m_GameInfoService.GetGameInfo<DynamicObjectInfo>();
            Debug.Log($"DoStart DynamicObjectService :{m_EntityGroupRow} m_EntityGroupRow:{m_EntityGroupRow.Name}");
        }


        public void ShowDynamicObject(int id)
        {
            if (Loader == null)
            {
                Loader = EntityLoader.Create(this);
            }

            //通过路径ID去判断是否被加载。用来在不同的章节下用了不用的静态场景ID,但是使用不同的加载Ids
            var info = m_DynamicObjectInfo.GetRowById<DynamicObjectInfoRow>(id);
            var AssetPathId = info.AssetPathId;
            if (m_LoadedAssetDict.ContainsKey(AssetPathId))
            {
                return;
            }

            Log.Info($"ShowDynamicObject:{id}");

            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if (m_LoadingAssetIds.Contains(AssetPathId))
            {
                return;
            }
            else
            {
                EntityDynamicObjectData data = EntityDynamicObjectData.Create(info.CreateEntityInfo(m_EntityGroupRow), this, info);
                m_LoadingAssetIds.Add(AssetPathId);
                Loader.ShowEntity(EnumEntity.DynamicObject,
                    (o) =>
                    {
                        if (m_LoadingAssetIds.Contains(AssetPathId))
                        {
                            m_LoadingAssetIds.Remove(AssetPathId);
                        }
                        m_LoadedAssetDict.Add(AssetPathId, o.Logic as EntityDynamicObject);
                    },
                    data.EntityInfo,
                    data);
            }
        }


        public void Hide(int id)
        {

        }


    }
}