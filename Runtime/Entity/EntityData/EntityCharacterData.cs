
using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.Core.Services;
using Pangoo.MetaTable;
using Pangoo.Core.Common;

namespace Pangoo
{
    [Serializable]
    public class EntityCharacterData : EntityData
    {
        public override EntityLoadType LoadType => EntityLoadType.Info;

        public override EnumEntity EntityType => EnumEntity.Character;

        public CharacterInfoRow InfoRow;

        public override string InfoUuid => InfoRow.Uuid;

        public float Height { get; set; } = ConstFloat.InvaildCameraHeight;

        public float ColliderHeight { get; set; } = 0;

        public bool IsInteractive { get; set; } = true;



        public CharacterService Service;
        public EntityCharacterData()
        {
        }

        public override void InitAsset(IEntityGroupRow group)
        {
            InitAsset(group, InfoRow.AssetPathRow);
        }

        public static EntityCharacterData Create(CharacterService service, CharacterInfoRow infoRow, IEntityGroupRow group, Vector3 Position, Vector3 Rotation, object userData = null)
        {
            EntityCharacterData entityData = ReferencePool.Acquire<EntityCharacterData>();
            entityData.Service = service;
            entityData.UserData = userData;
            entityData.InfoRow = infoRow;
            entityData.InitAsset(group);
            entityData.Position = Position;
            entityData.Rotation = Quaternion.Euler(Rotation);
            return entityData;
        }

        public override void Clear()
        {
            base.Clear();
            Service = null;
        }

        public bool IsPlayer
        {
            get
            {
                return InfoRow.IsPlayer;
            }
        }

        public bool CameraOnly
        {
            get
            {
                return InfoRow.CameraOnly;
            }
        }
    }
}
