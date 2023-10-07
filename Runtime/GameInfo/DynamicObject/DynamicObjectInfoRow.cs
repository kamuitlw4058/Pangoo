using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;
using UnityEngine;


namespace Pangoo
{
    public class DynamicObjectInfoRow : BaseInfoRow
    {
        public DynamicObjectTable.DynamicObjectRow m_DynamicObjectRow;
        public AssetPathTable.AssetPathRow m_AssetPathRow;
        public EntityGroupTable.EntityGroupRow m_EntityGroupRow;

        public EntityInfo m_EntityInfo;

        public DynamicObjectInfoRow(DynamicObjectTable.DynamicObjectRow scene, AssetPathTable.AssetPathRow assetPathRow, EntityGroupTable.EntityGroupRow entityGroupRow)
        {
            this.m_DynamicObjectRow = scene;
            this.m_AssetPathRow = assetPathRow;
            this.m_EntityGroupRow = entityGroupRow;
        }


        public EntityGroupTable.EntityGroupRow EntityGroupRow;


        public int Id
        {
            get
            {
                return m_DynamicObjectRow.Id;
            }
        }

        public string Name
        {
            get
            {
                return m_DynamicObjectRow.Name;
            }
        }

        public EntityInfo CreateEntityInfo(EntityGroupTable.EntityGroupRow entityGroupRow)
        {
            return EntityInfo.Create(m_AssetPathRow, entityGroupRow);
        }

        public EntityInfo EntityInfo
        {
            get
            {
                if (m_EntityInfo == null)
                {
                    m_EntityInfo = EntityInfo.Create(m_AssetPathRow, m_EntityGroupRow);
                }

                return m_EntityInfo;
            }

        }


        public int AssetPathId
        {
            get
            {
                return m_AssetPathRow.Id;
            }
        }

        public override void Remove()
        {
            ReferencePool.Release(m_EntityInfo);
        }

        public Vector3 Position
        {
            get
            {
                return m_DynamicObjectRow.Position;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return Quaternion.Euler(m_DynamicObjectRow.Rotation);
            }
        }



    }
}