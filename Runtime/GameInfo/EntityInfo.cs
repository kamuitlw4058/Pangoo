
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using Pangoo.MetaTable;
using MetaTable;
using Pangoo.Core.Common;


namespace Pangoo
{
    [Serializable]
    public abstract class EntityInfo : IReference
    {
        public abstract void InitAsset(IEntityGroupRow group);


        public void InitAsset(IEntityGroupRow group, IAssetPathRow asset)
        {
            m_EntityGroupRow = group;
            m_AssetPathRow = asset;
        }


        IAssetPathRow m_AssetPathRow;


        IEntityGroupRow m_EntityGroupRow;

        [ShowInInspector]
        public IAssetPathRow AssetPathRow
        {
            get
            {
                if (m_AssetPathRow == null)
                {
                    Debug.LogWarning($"AssetPath Is Null");
                    var mainConfig = PangooEntry.GameConfig.GetGameMainConfig();
                    if (mainConfig == null || (mainConfig != null && mainConfig.DefaultAssetPathUuid.IsNullOrWhiteSpace()))
                    {
                        return null;
                    }

                    var table = PangooEntry.MetaTable.GetMetaTable<AssetPathTable>();
                    m_AssetPathRow = table?.GetRowByUuid(mainConfig.DefaultAssetPathUuid);
                }

                return m_AssetPathRow;
            }
        }

        [ShowInInspector]
        public IEntityGroupRow EntityGroupRow
        {
            get
            {
                return m_EntityGroupRow;
            }
        }

        [ShowInInspector]
        public abstract EnumEntity EntityType { get; }


        [ShowInInspector]
        public abstract EntityLoadType LoadType { get; }

        [ShowInInspector]
        public string Uuid
        {
            get
            {
                switch (LoadType)
                {
                    case EntityLoadType.Info:
                        return InfoUuid;
                    case EntityLoadType.AssetPath:
                        return AssetPathUuid;
                    case EntityLoadType.Instance:
                        return InstanceUuid;
                }
                return null;
            }
        }

        [ShowInInspector]
        public abstract string InfoUuid { get; }

        private string m_InstanceUuid;

        [ShowInInspector]
        public virtual string InstanceUuid
        {
            get
            {
                if (m_InstanceUuid == null)
                {
                    m_InstanceUuid = UuidUtility.GetNewUuid();
                }
                return m_InstanceUuid;
            }
        }

        [ShowInInspector]
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


        public virtual void Clear()
        {
            Debug.Log($"Run Clear");
            m_AssetPathRow = null;
            m_EntityGroupRow = null;
        }

        public bool Equals(EntityInfo other)
        {
            if (Uuid.Equals(other.Uuid))
            {
                return true;
            }

            return false;
        }
    }
}
