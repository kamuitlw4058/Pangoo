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

namespace Pangoo.Core.Services
{

    public partial class StaticSceneService : EntityLoaderService<EntityStaticSceneData>
    {

        public override string ServiceName => "NewStaticSceneService";

        StaticSceneInfo m_StaticSceneInfo;

        IEntityGroupRow m_EntityGroupRow;


        protected override void DoAwake()
        {
            base.DoAwake();
            m_EnterAssetCountDict = new Dictionary<string, int>();
            Event.Subscribe(EnterStaticSceneEventArgs.EventId, OnEnterStaticSceneEvent);
            Event.Subscribe(ExitStaticSceneEventArgs.EventId, OnExitStaticSceneEvent);
        }


        protected override void DoStart()
        {
            m_StaticSceneInfo = GameInfoSrv.GetGameInfo<StaticSceneInfo>();
            m_EntityGroupRow = EntityGroupRowExtension.CreateStaticSceneGroup();
        }

        public override EntityStaticSceneData GetEntityData(string uuid)
        {
            if (EntityDataDict.TryGetValue(uuid, out EntityStaticSceneData entityStaticSceneData))
            {
                return entityStaticSceneData;
            }

            var info = m_StaticSceneInfo.GetRowByUuid<StaticSceneInfoRow>(uuid);
            if (info == null)
            {
                return null;
            }
            var data = EntityStaticSceneData.Create(info, this, m_EntityGroupRow, null);
            EntityDataDict.Add(uuid, data);
            return data;
        }

        public List<string> DiffTargetScenes(List<string> targetScenes, List<string> currentScenes)
        {
            if (targetScenes == null)
            {
                return currentScenes;
            }

            if (currentScenes == null)
            {
                return null;
            }

            Dictionary<string, string> TargetDict = new Dictionary<string, string>();

            foreach (var sceneUuid in targetScenes)
            {
                var entityData = GetEntityData(sceneUuid);
                if (entityData != null && !TargetDict.ContainsKey(entityData.AssetPathUuid))
                {
                    TargetDict.Add(entityData.AssetPathUuid, sceneUuid);
                }
            }

            Dictionary<string, string> CurrentDict = new Dictionary<string, string>();
            foreach (var sceneUuid in currentScenes)
            {
                var entityData = GetEntityData(sceneUuid);
                if (entityData != null && !CurrentDict.ContainsKey(entityData.AssetPathUuid))
                {
                    CurrentDict.Add(entityData.AssetPathUuid, sceneUuid);
                }
            }

            var diffKeys = TargetDict.DiffKeys(CurrentDict);
            return CurrentDict.ValuesOfKeyList(diffKeys);
        }


        public void SetSceneModelActive(string uuid, bool val)
        {
            var entityData = GetEntityData(uuid);
            var entityLoaderData = GetEntityLoadedData(entityData);
            var entity = entityLoaderData?.Entity as EntityStaticScene;
            if (entity != null)
            {
                entity.Hide = !val;
            }

        }


    }
}