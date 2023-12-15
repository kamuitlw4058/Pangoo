#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class AssetPathRowWrapper : MetaTableRowWrapper<AssetPathOverview, AssetPathNewRowWrapper, UnityAssetPathRow>
    {
        [ShowInInspector]
        public int Id
        {
            get { return UnityRow.Row.Id; }
        }

        [ShowInInspector]
        public string AssetType
        {
            get
            {
                return UnityRow.Row.AssetType;
            }
        }

        GameObject m_AssetPrefab;


        [ShowInInspector]
        public GameObject AssetPrefab
        {
            get
            {
                if (m_AssetPrefab == null)
                {
                    m_AssetPrefab = GameSupportEditorUtility.GetPrefabByAssetPathUuid(UnityRow.Uuid);
                }
                return m_AssetPrefab;
            }
        }
    }
}
#endif

