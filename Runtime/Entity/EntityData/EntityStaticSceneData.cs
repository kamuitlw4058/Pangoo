
using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.Core.Services;

namespace Pangoo
{
    [Serializable]
    public class EntityStaticSceneData : EntityData
    {
        // public EntityInfo Info;
        public StaticSceneInfoRow sceneInfo;

        public EntityInfo EntityInfo;

        public string AssetPathUuid
        {
            get
            {
                return EntityInfo.AssetPathUuid;
            }
        }

        public string Uuid
        {
            get
            {
                return sceneInfo.Uuid;
            }
        }

        public string Name
        {
            get
            {
                return sceneInfo.Name;
            }
        }

        public StaticSceneService Service;
        public EntityStaticSceneData() : base()
        {
        }

        public static EntityStaticSceneData Create(StaticSceneInfoRow staticSceneInfo, EntityInfo Info, StaticSceneService service, object userData = null)
        {
            EntityStaticSceneData entityData = ReferencePool.Acquire<EntityStaticSceneData>();
            entityData.EntityInfo = Info;
            entityData.Service = service;
            entityData.UserData = userData;
            entityData.sceneInfo = staticSceneInfo;
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
