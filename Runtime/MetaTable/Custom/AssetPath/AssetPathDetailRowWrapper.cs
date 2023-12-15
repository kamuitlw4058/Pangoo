#if UNITY_EDITOR

using System;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using MetaTable;
using UnityEditor;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class AssetPathDetailRowWrapper : MetaTableDetailRowWrapper<AssetPathOverview, UnityAssetPathRow>
    {
        [ShowInInspector]
        public string PackageDir
        {
            get
            {
                return UnityRow.Row.AssetPackageDir;
            }
        }


        [ShowInInspector]
        public string AssetType
        {
            get
            {
                return UnityRow.Row.AssetType;
            }
        }

        [ShowInInspector]
        [ValueDropdown("OnAssetGroupUuidDropdown")]
        public string AssetGroup
        {
            get
            {
                return GameSupportEditorUtility.GetAssetGroupUuidByAssetGroup(UnityRow.Row.AssetGroup);
            }
            set
            {
                var oldAssetGroupUuid = GameSupportEditorUtility.GetAssetGroupUuidByAssetGroup(UnityRow.Row.AssetGroup);
                if (value != oldAssetGroupUuid)
                {

                    var oldGroup = GameSupportEditorUtility.GetAssetGroupByAssetGroupUuid(oldAssetGroupUuid);
                    var newGroup = GameSupportEditorUtility.GetAssetGroupByAssetGroupUuid(value);

                    var oldPath = AssetUtility.GetAssetPath(UnityRow.Row.AssetPackageDir, UnityRow.Row.AssetType, UnityRow.Row.AssetPath, oldGroup);
                    var groupPath = AssetUtility.GetAssetPathDir(UnityRow.Row.AssetPackageDir, UnityRow.Row.AssetType, newGroup);
                    if (!AssetDatabase.IsValidFolder(groupPath))
                    {
                        var baseAssetPath = AssetUtility.GetAssetPathDir(UnityRow.Row.AssetPackageDir, UnityRow.Row.AssetType);
                        AssetDatabase.CreateFolder(baseAssetPath, newGroup);
                    }
                    var newPath = AssetUtility.GetAssetPath(UnityRow.Row.AssetPackageDir, UnityRow.Row.AssetType, UnityRow.Row.AssetPath, newGroup);
                    MovePrefab(oldPath, newPath);
                    UnityRow.Row.AssetGroup = newGroup;
                    Save();
                }
            }
        }

        public void MovePrefab(string src, string dest)
        {
            Debug.Log($"Src:{src} Dest:{dest}");

            AssetDatabase.MoveAsset(src, dest);
        }
        IEnumerable OnAssetGroupUuidDropdown()
        {
            return GameSupportEditorUtility.GetAssetGroupUuidDropdown();
        }

        [ShowInInspector]
        public string AssetPath
        {
            get
            {
                return UnityRow.Row.AssetPath;
            }
        }


        [ShowInInspector]
        public string PrefabPath
        {
            get
            {
                return UnityRow.Row.ToPrefabPath();
            }
        }



        [ShowInInspector]
        [LabelText("资源预制体")]
        public GameObject AssetPrefab
        {
            get
            {
                return GameSupportEditorUtility.GetPrefabByAssetPathUuid(UnityRow.Uuid);
            }
        }


    }
}
#endif

