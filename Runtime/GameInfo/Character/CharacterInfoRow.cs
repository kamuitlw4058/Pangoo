
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

        public ICharacterRow CharacterRow => m_CharacterRow;

        public IAssetPathRow m_AssetPathRow;
        public IAssetPathRow AssetPathRow => m_AssetPathRow;



        public CharacterInfoRow(ICharacterRow character, IAssetPathRow assetPathRow)
        {
            this.m_CharacterRow = character;
            this.m_AssetPathRow = assetPathRow;
        }

        public string Uuid
        {
            get
            {
                return m_CharacterRow.Uuid;
            }
        }


        public string Name
        {
            get
            {
                return m_CharacterRow.Name;
            }
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

        public float CameraHeight
        {
            get
            {
                return m_CharacterRow.CameraHeight;
            }
        }

        public float ColliderHeight
        {
            get
            {
                return m_CharacterRow.ColliderHeight;
            }
        }
    }
}
