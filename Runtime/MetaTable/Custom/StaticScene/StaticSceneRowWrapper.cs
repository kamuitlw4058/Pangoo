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
    public partial class StaticSceneRowWrapper : MetaTableRowWrapper<StaticSceneOverview, StaticSceneNewRowWrapper, UnityStaticSceneRow>
    {
        GameObject m_AssetPrefab;
        [ShowInInspector]
        [LabelText("资源预制体")]
        public GameObject AssetPrefab
        {
            get
            {
                if (m_AssetPrefab == null)
                {
                    m_AssetPrefab = GameSupportEditorUtility.GetPrefabByAssetPathUuid(UnityRow.Row.AssetPathUuid);
                }
                return m_AssetPrefab;
            }
        }

    }
}
#endif

