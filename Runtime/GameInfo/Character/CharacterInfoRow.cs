
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using GameFramework.Entity;
using Pangoo.MetaTable;

namespace Pangoo
{

    public class CharacterInfoRow : BaseInfoRow
    {
        public ICharacterRow m_CharacterRow;
        public IAssetPathRow m_AssetPathRow;

        public EntityInfo m_EntityInfo;

        public CharacterInfoRow(ICharacterRow character, IAssetPathRow assetPathRow)
        {
            this.m_CharacterRow = character;
            this.m_AssetPathRow = assetPathRow;
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
        
        public float Height
        {
            get
            {
                return m_CharacterRow.Height;
            }
        }
    }
}
