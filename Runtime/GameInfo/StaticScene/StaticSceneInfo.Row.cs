using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;
using Pangoo.MetaTable;
using Pangoo.Common;

namespace Pangoo
{
    public class StaticSceneInfoRow : BaseInfoRow
    {
        public IStaticSceneRow m_StaticSceneRow;
        public IAssetPathRow m_AssetPathRow;

        public EntityInfo m_EntityInfo;

        public StaticSceneInfoRow(IStaticSceneRow scene, IAssetPathRow assetPathRow)
        {
            this.m_StaticSceneRow = scene;
            this.m_AssetPathRow = assetPathRow;
        }


        public EntityGroupTable.EntityGroupRow EntityGroupRow;


        List<string> m_LoadSceneUuids = null;

        public string Uuid
        {
            get
            {
                return m_StaticSceneRow.Uuid;
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

        public List<string> LoadSceneUuids
        {
            get
            {
                if (m_LoadSceneUuids == null)
                {
                    m_LoadSceneUuids = m_StaticSceneRow.LoadSceneUuids.ToSplitList<string>();
                }
                return m_LoadSceneUuids;
            }
        }

        public string AssetPathUuid
        {
            get
            {
                return m_AssetPathRow.Uuid;
            }
        }

        public override void Remove()
        {
            ReferencePool.Release(m_EntityInfo);
        }



    }
}