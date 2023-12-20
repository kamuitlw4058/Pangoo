using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;
using UnityEngine;
using Pangoo.MetaTable;


namespace Pangoo
{
    public class DynamicObjectInfoRow : BaseInfoRow
    {
        public DynamicObjectTable.DynamicObjectRow m_DynamicObjectRow;
        public AssetPathTable.AssetPathRow m_AssetPathRow;

        public EntityInfo m_EntityInfo;

        public DynamicObjectInfoRow(DynamicObjectTable.DynamicObjectRow scene, AssetPathTable.AssetPathRow assetPathRow)
        {
            this.m_DynamicObjectRow = scene;
            this.m_AssetPathRow = assetPathRow;
        }




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

        public EntityInfo CreateEntityInfo(IEntityGroupRow entityGroupRow)
        {
            return EntityInfo.Create(m_AssetPathRow, entityGroupRow);
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


        public Vector3 Scale
        {
            get
            {
                return m_DynamicObjectRow.Scale;
            }
        }




    }
}