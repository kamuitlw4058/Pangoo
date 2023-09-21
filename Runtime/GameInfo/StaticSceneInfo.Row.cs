using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;


namespace Pangoo
{
    public partial class StaticSceneInfo
    {

        public class StaticSceneInfoRow : IInfoRow
        {
            public StaticSceneTable.StaticSceneRow m_StaticSceneRow;
            public AssetPathTable.AssetPathRow m_AssetPathRow;
            public EntityGroupTable.EntityGroupRow m_EntityGroupRow;

            public EntityInfo m_EntityInfo;

            public StaticSceneInfoRow(StaticSceneTable.StaticSceneRow scene, AssetPathTable.AssetPathRow assetPathRow, EntityGroupTable.EntityGroupRow entityGroupRow)
            {
                this.m_StaticSceneRow = scene;
                this.m_AssetPathRow = assetPathRow;
                this.m_EntityGroupRow = entityGroupRow;
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

            public void Remove()
            {
                ReferencePool.Release(m_EntityInfo);
            }

        }





    }
}