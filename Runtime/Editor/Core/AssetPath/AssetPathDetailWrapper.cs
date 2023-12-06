#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Pangoo.Editor
{
    public class AssetPathDetailWrapper : ExcelTableRowDetailWrapper<AssetPathTableOverview, AssetPathTable.AssetPathRow>
    {

        [ShowInInspector]
        public string PackageDir
        {
            get
            {
                return Row.AssetPackageDir;
            }
        }


        [ShowInInspector]
        public string AssetType
        {
            get
            {
                return Row.AssetType;
            }
        }

        [ShowInInspector]
        [ValueDropdown("OnAssetGroupDropdown")]
        public int AssetGroup
        {
            get
            {
                return GameSupportEditorUtility.GetAssetGroupIdByAssetGroup(Row.AssetGroup);
            }
            set
            {
                var oldAssetGroupId = GameSupportEditorUtility.GetAssetGroupIdByAssetGroup(Row.AssetGroup);
                if (value != oldAssetGroupId && value != oldAssetGroupId)
                {

                    var oldGroup = GameSupportEditorUtility.GetAssetGroupByAssetGroupId(oldAssetGroupId);
                    var newGroup = GameSupportEditorUtility.GetAssetGroupByAssetGroupId(value);


                    var oldPath = AssetUtility.GetAssetPath(Row.AssetPackageDir, Row.AssetType, Row.AssetPath, oldGroup);
                    var groupPath = AssetUtility.GetAssetPathDir(Row.AssetPackageDir, Row.AssetType, newGroup);
                    if (!AssetDatabase.IsValidFolder(groupPath))
                    {
                        var baseAssetPath = AssetUtility.GetAssetPathDir(Row.AssetPackageDir, Row.AssetType);
                        AssetDatabase.CreateFolder(baseAssetPath, newGroup);
                    }
                    var newPath = AssetUtility.GetAssetPath(Row.AssetPackageDir, Row.AssetType, Row.AssetPath, newGroup);
                    MovePrefab(oldPath, newPath);
                    Row.AssetGroup = newGroup;
                    Save();
                }
            }
        }

        public void MovePrefab(string src, string dest)
        {
            Debug.Log($"Src:{src} Dest:{dest}");

            AssetDatabase.MoveAsset(src, dest);
        }
        IEnumerable OnAssetGroupDropdown()
        {
            return GameSupportEditorUtility.GetAssetGroupIdDropdown();
        }

        [ShowInInspector]
        public string AssetPath
        {
            get
            {
                return Row.AssetPath;
            }
        }


        [ShowInInspector]
        public string PrefabPath
        {
            get
            {
                return Row.ToPrefabPath();
            }
        }




        [ShowInInspector]
        [LabelText("资源预制体")]
        public GameObject AssetPrefab
        {
            get
            {
                return GameSupportEditorUtility.GetPrefabByAssetPathId(Row.Id);
            }
        }





    }
}
#endif