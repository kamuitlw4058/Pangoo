
using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.Service;

namespace Pangoo
{
    [Serializable]
    public class EntityStaticSceneData : EntityData
    {
        // public EntityInfo Info;
        
        public EntityInfo EntityInfo;

        public int AssetPathId{
            get{
                return EntityInfo.AssetPathId;
            }
        }

        public StaticSceneService Service;
        public EntityStaticSceneData() : base()
        {
        }

        public static EntityStaticSceneData Create(EntityInfo Info, StaticSceneService service, object userData = null)
        {
            EntityStaticSceneData entityData = ReferencePool.Acquire<EntityStaticSceneData>();
            entityData.EntityInfo = Info;
            entityData.Service = service;
            entityData.UserData = userData;
            return entityData;
        }

        public override void Clear()
        {
            base.Clear();
            Service = null;
            EntityInfo = null;
        }
    }
}
