#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class StaticSceneDetailRowWrapper : MetaTableDetailRowWrapper<StaticSceneOverview, UnityStaticSceneRow>
    {

        [LabelText("资源Uuid")]
        [ValueDropdown("AssetPathUuidValueDropdown")]
        [PropertyOrder(0)]
        [ShowInInspector]
        // [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
        public string AssetPathUuid
        {
            get
            {
                return UnityRow.Row.AssetPathUuid;
            }
            set
            {
                UnityRow.Row.AssetPathUuid = value;
                Save();
            }

        }

        [LabelText("资源ID")]
        [ValueDropdown("AssetPathIdValueDropdown")]
        [PropertyOrder(0)]
        [ShowInInspector]
        // [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
        public int AssetPathId
        {
            get
            {
                return UnityRow.Row?.AssetPathId ?? 0;
            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.AssetPathId = value;
                    Save();
                }
            }

        }

        public IEnumerable AssetPathIdValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "Scene" });
        }

        public IEnumerable AssetPathUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathUuids(assetTypes: new List<string> { "Scene" });
        }

        GameObject m_Prefab;


        [ShowInInspector]
        [LabelText("资源预制体")]
        [ReadOnly]
        public GameObject Prefab
        {
            get
            {
                if (m_Prefab == null)
                {
                    m_Prefab = GameSupportEditorUtility.GetPrefabByAssetPathUuid(UnityRow.Row.AssetPathUuid);
                }
                return m_Prefab;
            }
            set
            {

            }
        }




        [LabelText("加载场景Ids")]
        [ValueDropdown("StaticSceneIdValueDropdown", IsUniqueList = true)]

        // [OnValueChanged("OnDynamicSceneIdsChanged")]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        public int[] LoadSceneIds
        {
            get
            {
                return UnityRow.Row?.LoadSceneIds?.ToSplitArr<int>() ?? new int[0];
            }
            set
            {
                UnityRow.Row.LoadSceneIds = value.ToList().ToListString();
                Save();
            }
        }

        [ShowInInspector]
        [LabelText("加载场景Uuid")]
        [ListDrawerSettings(Expanded = true)]
        [ValueDropdown("StaticSceneUuidValueDropdown", IsUniqueList = true)]

        public string[] LoadSceneUuids
        {
            get
            {
                return UnityRow.Row.LoadSceneUuids?.ToSplitArr<string>() ?? new string[0];
            }
            set
            {
                UnityRow.Row.LoadSceneUuids = value.ToList().ToListString();
                Save();
            }
        }



        public IEnumerable StaticSceneIdValueDropdown()
        {
            return GameSupportEditorUtility.GetStaticSceneIds(excludeIds: new List<int> { UnityRow.Row.Id });
        }

        public IEnumerable StaticSceneUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetStaticSceneUuids(excludeUuid: new List<string> { UnityRow.Uuid });
        }


        [Button("升级到Uuid")]
        public void UpgradeToUuuid()
        {
            var row = GameSupportEditorUtility.GetAssetPathById(AssetPathId);
            if (row == null)
            {
                Debug.Log($"AssetPathId:{AssetPathId} no found:{Name}");
                return;
            }
            AssetPathUuid = row.Uuid;

            List<string> loadUuids = new List<string>();

            foreach (var id in LoadSceneIds)
            {
                var loadRow = GameSupportEditorUtility.GetStaticSceneById(id);
                loadUuids.Add(loadRow.Uuid);
            }

            LoadSceneUuids = loadUuids.ToArray();

        }



    }
}
#endif

