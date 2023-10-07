#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;
using Sirenix.Utilities;




using UnityEditor;
using Sirenix.OdinInspector.Editor;


namespace Pangoo
{
    public class AssetPathNewWrapper : ExcelTableRowNewWrapper<AssetPathTableOverview, AssetPathTable.AssetPathRow>
    {

        public int GetAssetTypeBaseId(string assetType)
        {
            switch (assetType)
            {
                case ConstExcelTable.DynamicObjectAssetTypeName:
                    return ConstExcelTable.DynamicObjectAssetPathIdBase;
                case ConstExcelTable.StaticSceneAssetTypeName:
                    return ConstExcelTable.StaticSceneAssetPathIdBase;
            }

            return 0;
        }

        public string GetPrefixByAssetType(string assetType)
        {
            switch (assetType)
            {
                case ConstExcelTable.DynamicObjectAssetTypeName:
                    return "DO_";
                case ConstExcelTable.StaticSceneAssetTypeName:
                    return "SS_";
            }

            return string.Empty;

        }

        [ShowInInspector]
        [ValueDropdown("OnAssetTypeDropdown")]
        public string AssetType
        {
            get
            {
                if (Row != null)
                {
                    if (Row.AssetType == null || Row.AssetType == string.Empty)
                    {
                        Row.AssetType = ConstExcelTable.DynamicObjectAssetTypeName;
                        Row.Id = GetAssetTypeBaseId(Row.AssetType);
                    }
                }

                return Row?.AssetType;
            }
            set
            {
                if (Row != null)
                {
                    Row.AssetType = value;
                    Row.Id = GetAssetTypeBaseId(value);
                }
            }
        }
        string m_FileType;

        [ValueDropdown("OnFileTypeDropdown")]
        [ShowInInspector]
        public string FileType
        {
            get
            {
                if (m_FileType.IsNullOrWhiteSpace())
                {
                    m_FileType = "prefab";
                }

                return m_FileType;
            }
            set
            {
                m_FileType = value;
                UpdateAssetPath();
            }
        }

        public void UpdateAssetPath()
        {
            Row.AssetPath = $"{m_AssetName}.{m_FileType}";
        }


        IEnumerable OnFileTypeDropdown()
        {
            var ret = new ValueDropdownList<string>();
            ret.Add("prefab");
            return ret;
        }

        // [ShowInInspector]
        // public string PackageDir
        // {
        //     get
        //     {
        //         if (Row == null && Overview == null)
        //         {
        //             return string.Empty;
        //         }
        //         if (Row.AssetPackageDir.IsNullOrWhitespace())
        //         {
        //             Row.AssetPackageDir = Overview.Config.PackageDir;
        //         }

        //         return Row.AssetPackageDir;
        //     }
        // }



        [ShowInInspector]
        public string FullPath
        {
            get
            {
                if (Row == null)
                {
                    return string.Empty;
                }

                return AssetUtility.GetAssetPath(Overview.Config.PackageDir, Row.AssetType, Row.AssetPath);
            }
        }

        public IEnumerable OnAssetTypeDropdown()
        {
            var ret = new ValueDropdownList<string>();
            ret.Add(ConstExcelTable.DynamicObjectAssetTypeName);
            ret.Add(ConstExcelTable.StaticSceneAssetTypeName);
            return ret;
        }
        string m_AssetName;

        [ShowInInspector]
        public string AssetName
        {
            get
            {
                return m_AssetName;
            }
            set
            {
                m_AssetName = value;
                UpdateAssetPath();
            }
        }



        [LabelText("模型预制体")]
        [AssetsOnly]
        [AssetSelector(ExpandAllMenuItems = false)]
        [OnValueChanged("OnModelPrefabChanged")]
        public GameObject ModelPrefab;

        void OnModelPrefabChanged()
        {
            AssetName = GetPrefixByAssetType(AssetType) + ModelPrefab.name;
        }

        public static AssetPathNewWrapper Create(AssetPathTableOverview overview, int id = 0, string assetType = "", string name = "", string fileType = ".prefab", Action<int> afterCreateAsset = null)
        {
            var wrapper = new AssetPathNewWrapper();
            wrapper.Overview = overview;
            wrapper.Row = new AssetPathTable.AssetPathRow();
            wrapper.AssetType = assetType;
            wrapper.Id = wrapper.GetAssetTypeBaseId(assetType) + id;
            wrapper.FileType = fileType;
            wrapper.Name = name;
            wrapper.AfterCreate = afterCreateAsset;
            return wrapper;
        }


        public void ConfirmCreate(int id, string name, string assetType, string assetName, GameObject prefab, string fileType)
        {


            var fileName = $"{assetName}";
            var fullFileName = $"{fileName}.{fileType}";


            var assetPathRow = new AssetPathTable.AssetPathRow();
            assetPathRow.Id = id;
            assetPathRow.AssetPackageDir = Overview.Config.PackageDir;
            assetPathRow.AssetPath = fullFileName;
            assetPathRow.AssetType = assetType;
            assetPathRow.Name = name;
            Overview.Data.Rows.Add(assetPathRow);
            EditorUtility.SetDirty(Overview);



            var go = new GameObject(fileName);

            var prefab_go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            prefab_go.transform.parent = go.transform;
            prefab_go.ResetTransfrom(false);
            prefab_go.name = ConstExcelTable.SubModelName;

            // string prefabPath = "Assets/Prefabs/MyPrefab.prefab";
            PrefabUtility.SaveAsPrefabAsset(go, assetPathRow.ToPrefabPath());
            GameObject.DestroyImmediate(go);


            AssetDatabase.SaveAssets();
            if (AfterCreate != null)
            {
                AfterCreate(id);
            }


        }

        public override void Create()
        {
            if (Id == 0 || Name.IsNullOrWhiteSpace() || AssetName.IsNullOrWhitespace())
            {
                EditorUtility.DisplayDialog("错误", "Id, Name, 命名空间,ArtPrefab  必须填写", "确定");
                // GUIUtility.ExitGUI();
                return;
            }


            if (StringUtility.ContainsChinese(AssetName))
            {
                EditorUtility.DisplayDialog("错误", "Name不能包含中文", "确定");
                // GUIUtility.ExitGUI();
                return;
            }

            if (StringUtility.IsOnlyDigit(AssetName))
            {
                EditorUtility.DisplayDialog("错误", "Name不能全是数字", "确定");
                return;
            }

            if (char.IsDigit(AssetName[0]))
            {
                EditorUtility.DisplayDialog("错误", "Name开头不能是数字", "确定");
                return;
            }

            if (CheckExistsId())
            {
                EditorUtility.DisplayDialog("错误", "Id已经存在", "确定");
                return;
            }

            if (CheckExistsName())
            {
                EditorUtility.DisplayDialog("错误", "Name已经存在", "确定");
                return;
            }

            ConfirmCreate(Id, Name, AssetType, AssetName, ModelPrefab, FileType);
        }

    }
}
#endif