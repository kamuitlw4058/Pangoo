
using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.Core.Services;

namespace Pangoo
{
    [Serializable]
    public class EntityCharacterData : EntityData
    {
        // public EntityInfo Info;

        public EntityInfo EntityInfo;

        public CharacterInfoRow InfoRow;


        public int AssetPathId
        {
            get
            {
                return EntityInfo.AssetPathId;
            }
        }

        public CharacterService Service;
        public EntityCharacterData() : base()
        {
        }

        public static EntityCharacterData Create(EntityInfo Info, CharacterService service, CharacterInfoRow infoRow, Vector3 Position, Vector3 Rotation, object userData = null)
        {
            EntityCharacterData entityData = ReferencePool.Acquire<EntityCharacterData>();
            entityData.EntityInfo = Info;
            entityData.Service = service;
            entityData.UserData = userData;
            entityData.InfoRow = infoRow;
            entityData.Position = Position;
            entityData.Rotation = Quaternion.Euler(Rotation);
            return entityData;
        }

        public override void Clear()
        {
            base.Clear();
            Service = null;
            EntityInfo = null;
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
