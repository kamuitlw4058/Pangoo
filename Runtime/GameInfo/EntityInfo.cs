
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using Pangoo.MetaTable;

namespace Pangoo
{
    [Serializable]
    public sealed class EntityInfo : IReference
    {

        public IAssetPathRow AssetPathRow;
        public IEntityGroupRow EntityGroupRow;


        public string AssetPathUuid
        {
            get
            {
                return AssetPathRow.Uuid;
            }
        }

        public string AssetName
        {
            get
            {
                return AssetPathRow.Name;
            }
        }



        public string AssetPath
        {
            get
            {
                return AssetPathRow.ToPrefabPath();
            }
        }

        public string GroupName
        {

            get
            {
                return EntityGroupRow?.Name ?? string.Empty;
            }
        }

        public int GroupInstanceCapacity
        {
            get
            {
                return EntityGroupRow?.InstanceCapacity ?? 1000;
            }
        }

        public double GroupInstanceAutoReleaseInterval
        {
            get
            {
                return EntityGroupRow?.InstanceAutoReleaseInterval ?? 0;
            }
        }

        public double GroupInstanceExpireTime
        {
            get
            {
                return EntityGroupRow?.InstanceExpireTime ?? 0;
            }
        }

        public int GroupInstancePriority
        {
            get
            {
                return EntityGroupRow?.InstancePriority ?? 0;
            }
        }

        public static EntityInfo Create(IAssetPathRow assetPath, IEntityGroupRow entityGroup)
        {
            var info = ReferencePool.Acquire<EntityInfo>();
            info.AssetPathRow = assetPath;
            info.EntityGroupRow = entityGroup;
            return info;
        }

        public void Clear()
        {
            AssetPathRow = null;
            EntityGroupRow = null;
        }

    }
}
