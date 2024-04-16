
using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.Core.Services;
using Pangoo.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;

using Pangoo.MetaTable;
using Pangoo.Core.Common;

namespace Pangoo
{
    [Serializable]
    public class EntityStaticSceneData : EntityData
    {

        public override EntityLoadType LoadType => EntityLoadType.AssetPath;

        [ShowInInspector]
        public override EnumEntity EntityType => EnumEntity.StaticScene;

        StaticSceneInfoRow m_sceneInfo;

        [ShowInInspector]
        public StaticSceneInfoRow sceneInfo
        {
            get
            {
                return m_sceneInfo;
            }
            set
            {
                if (m_sceneInfo != value)
                {
                    m_sceneInfo = value;
                }
            }
        }


        public override string InfoUuid
        {
            get
            {
                return sceneInfo.Uuid;
            }
        }
        public string UuidShort
        {
            get
            {
                return Uuid.ToShortUuid();
            }
        }

        [ShowInInspector]
        public string Name
        {
            get
            {
                return sceneInfo.Name;
            }
        }

        public override Vector3 Position
        {
            get
            {
                return sceneInfo.m_StaticSceneRow.Position;
            }
        }

        public override Quaternion Rotation
        {
            get
            {
                return Quaternion.Euler(sceneInfo.m_StaticSceneRow.Rotation);
            }
        }



        public StaticSceneService Service;
        public EntityStaticSceneData()
        {
        }

        public override void InitAsset(IEntityGroupRow group)
        {
            InitAsset(group, sceneInfo.AssetPathRow);
        }

        public static EntityStaticSceneData Create(StaticSceneInfoRow staticSceneInfo, StaticSceneService service, IEntityGroupRow group, object userData = null)
        {
            EntityStaticSceneData entityData = ReferencePool.Acquire<EntityStaticSceneData>();
            entityData.Service = service;
            entityData.UserData = userData;
            entityData.sceneInfo = staticSceneInfo;
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
