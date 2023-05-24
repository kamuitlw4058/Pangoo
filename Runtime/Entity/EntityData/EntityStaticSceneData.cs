
using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    [Serializable]
    public class EntityStaticSceneData : EntityData
    {
        public EntityInfo Info;

        public List<int> LoadIds;

        public StaticSceneManager Manager;
        public EntityStaticSceneData() : base()
        {
        }

        public static EntityStaticSceneData Create(EntityInfo Info, List<int> LoadIds,StaticSceneManager manager, object userData = null)
        {
            EntityStaticSceneData entityData = ReferencePool.Acquire<EntityStaticSceneData>();
            entityData.LoadIds = LoadIds;
            entityData.Info = Info;
            entityData.Manager = manager;
            entityData.UserData = userData;
            return entityData;
        }

        // public static EntityStaticSceneData Create(EnemyData enemyData, LevelPath levelPath, Vector3 position, Quaternion rotation, object userData = null)
        // {
        //     EntityDataEnemy entityData = ReferencePool.Acquire<EntityStaticSceneData>();
        //     entityData.EnemyData = enemyData;
        //     entityData.LevelPath = levelPath;
        //     entityData.Position = position;
        //     entityData.Rotation = rotation;
        //     return entityData;
        // }

        public override void Clear()
        {
            base.Clear();
            // EnemyData = null;
        }
    }
}
