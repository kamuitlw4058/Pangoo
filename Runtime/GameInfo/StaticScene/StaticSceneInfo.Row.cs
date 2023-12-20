using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;
using Pangoo.MetaTable;

namespace Pangoo
{
    public class StaticSceneInfoRow : BaseInfoRow
    {
        public StaticSceneTable.StaticSceneRow m_StaticSceneRow;
        public AssetPathTable.AssetPathRow m_AssetPathRow;

        public EntityInfo m_EntityInfo;

        public StaticSceneInfoRow(StaticSceneTable.StaticSceneRow scene, AssetPathTable.AssetPathRow assetPathRow)
        {
            this.m_StaticSceneRow = scene;
            this.m_AssetPathRow = assetPathRow;
        }


        public EntityGroupTable.EntityGroupRow EntityGroupRow;


        List<int> m_LoadSceneIds = null;

        public int Id
        {
            get
            {
                return m_StaticSceneRow.Id;
            }
        }

        public string Name
        {
            get
            {
                return m_StaticSceneRow.Name;
            }
        }

        public EntityInfo CreateEntityInfo(IEntityGroupRow entityGroupRow)
        {
            return EntityInfo.Create(m_AssetPathRow, entityGroupRow);
        }

        public List<int> LoadSceneIds
        {
            get
            {
                if (m_LoadSceneIds == null)
                {
                    m_LoadSceneIds = m_StaticSceneRow.LoadSceneIds.Split("|").Select(row => int.Parse(row)).ToList();
                }
                return m_LoadSceneIds;
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



    }
}