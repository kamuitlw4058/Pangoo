
using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.Core.Services;

namespace Pangoo
{
    [Serializable]
    public class EntityDynamicObjectData : EntityData
    {
        // public EntityInfo Info;

        public EntityInfo EntityInfo;

        public DynamicObjectInfoRow InfoRow;

        public string Uuid
        {
            get
            {
                return InfoRow.Uuid;
            }
        }


        public string AssetPathUuid
        {
            get
            {
                return EntityInfo.AssetPathUuid;
            }
        }

        public DynamicObjectService Service;
        public EntityDynamicObjectData() : base()
        {
        }

        public static EntityDynamicObjectData Create(EntityInfo Info, DynamicObjectService service, DynamicObjectInfoRow infoRow, object userData = null)
        {
            EntityDynamicObjectData entityData = ReferencePool.Acquire<EntityDynamicObjectData>();
            entityData.EntityInfo = Info;
            entityData.Service = service;
            entityData.UserData = userData;
            entityData.InfoRow = infoRow;
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
