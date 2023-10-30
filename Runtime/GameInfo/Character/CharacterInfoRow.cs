
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;

namespace Pangoo
{

    public class CharacterInfoRow : BaseInfoRow
    {
        public CharacterTable.CharacterRow m_CharacterRow;
        public AssetPathTable.AssetPathRow m_AssetPathRow;
        public EntityGroupTable.EntityGroupRow m_EntityGroupRow;

        public EntityInfo m_EntityInfo;

        public CharacterInfoRow(CharacterTable.CharacterRow character, AssetPathTable.AssetPathRow assetPathRow, EntityGroupTable.EntityGroupRow entityGroupRow)
        {
            this.m_CharacterRow = character;
            this.m_AssetPathRow = assetPathRow;
            this.m_EntityGroupRow = entityGroupRow;
        }
        public int Id
        {
            get
            {
                return m_CharacterRow.Id;
            }
        }

        public string Name
        {
            get
            {
                return m_CharacterRow.Name;
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

        public bool IsPlayer
        {
            get
            {
                return m_CharacterRow.IsPlayer;
            }
        }

        public bool CameraOnly
        {
            get
            {
                return m_CharacterRow.CameraOnly;
            }
        }



    }
}
