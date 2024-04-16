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
        IDynamicObjectRow m_DynamicObjectRow;

        public IDynamicObjectRow DynamicObjectRow => m_DynamicObjectRow;

        public IAssetPathRow m_AssetPathRow;

        public IAssetPathRow AssetPathRow => m_AssetPathRow;


        public EntityInfo m_EntityInfo;

        public DynamicObjectInfoRow(IDynamicObjectRow row, IAssetPathRow assetPathRow)
        {
            this.m_DynamicObjectRow = row;
            this.m_AssetPathRow = assetPathRow;
        }


        public string Uuid
        {
            get
            {
                return m_DynamicObjectRow.Uuid;
            }
        }

        public string UuidShort
        {
            get
            {
                return m_DynamicObjectRow.UuidShort;
            }
        }

        public string Name
        {
            get
            {
                return m_DynamicObjectRow.Name;
            }
        }


        public string AssetPathUuid
        {
            get
            {
                return m_AssetPathRow.Uuid;
            }
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