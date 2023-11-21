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
        public string AssetGroup
        {
            get
            {
                return Row.AssetGroup;
            }
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
        public string FullPath
        {
            get
            {
                return Row.ToFullPath();
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