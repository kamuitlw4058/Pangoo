
using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.Core.Services;
using Pangoo.MetaTable;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;

namespace Pangoo
{
    [Serializable]
    public class EntityDynamicObjectData : EntityData
    {
        // public EntityInfo Info;
        public override EntityLoadType LoadType => EntityLoadType.Info;

        public override EnumEntity EntityType => EnumEntity.DynamicObject;

        [ShowInInspector]
        public DynamicObjectInfoRow InfoRow;

        [ShowInInspector]
        public override string InfoUuid
        {
            get
            {
                if (InfoRow == null)
                {
                    Debug.LogError($"DynamicObject Info: is null");
                }

                return InfoRow.Uuid;
            }
        }


        public DynamicObjectService Service;
        public EntityDynamicObjectData()
        {
        }

        public override void InitAsset(IEntityGroupRow group)
        {
            InitAsset(group, InfoRow.AssetPathRow);
        }

        public static EntityDynamicObjectData Create(DynamicObjectService service, DynamicObjectInfoRow infoRow, IEntityGroupRow group, object userData = null)
        {
            EntityDynamicObjectData entityData = ReferencePool.Acquire<EntityDynamicObjectData>();
            entityData.Service = service;
            entityData.UserData = userData;
            entityData.InfoRow = infoRow;
            entityData.InitAsset(group);
            return entityData;
        }

        public override void Clear()
        {
            base.Clear();
            Service = null;
        }
    }
}
