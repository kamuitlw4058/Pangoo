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
using System.Linq;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class StaticSceneRowWrapper : MetaTableRowWrapper<StaticSceneOverview, StaticSceneNewRowWrapper, UnityStaticSceneRow>
    {

        [ShowInInspector]
        [TableTitleGroup("资源Uuid")]
        [HideLabel]
        public string AssetPathUuid
        {
            get
            {
                return UnityRow.Row.AssetPathUuid;
            }
        }



        GameObject m_AssetPrefab;
        [ShowInInspector]
        [TableTitleGroup("资源预制体")]
        [HideLabel]
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


        [ShowInInspector]
        [TableTitleGroup("加载场景数量")]
        [HideLabel]
        public int LoadSceneUuids
        {
            get
            {
                return UnityRow.Row.LoadSceneUuids.ToSplitArr<string>().Count();
            }
        }



    }
}
#endif

