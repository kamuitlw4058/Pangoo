using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;

#if UNITY_EDITOR

using UnityEditor;
using Sirenix.OdinInspector.Editor;
#endif

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
                    }
                }

                return Row?.AssetType;
            }
            set
            {
                if (Row != null)
                {
                    Row.AssetType = value;
                }
            }
        }

        public IEnumerable OnAssetTypeDropdown()
        {
            var ret = new ValueDropdownList<string>();
            ret.Add(ConstExcelTable.DynamicObjectAssetTypeName);
            ret.Add(ConstExcelTable.StaticSceneAssetTypeName);
            return ret;
        }

        public string NameCn;



        [LabelText("模型预制体")]
        [AssetsOnly]
        [AssetSelector(ExpandAllMenuItems = false)]
        [OnValueChanged("OnModelPrefabChanged")]
        public GameObject ModelPrefab;

        void OnModelPrefabChanged()
        {
            Name = GetPrefixByAssetType(AssetType) + ModelPrefab.name;
        }

        public string FileType
        {
            get; set;
        }

        public static AssetPathNewWrapper Create(AssetPathTableOverview overview, int id = 0, string assetType = "", string name = "", string fileType = "", Action<int> afterCreateAsset = null)
        {
            var wrapper = new AssetPathNewWrapper();
            wrapper.Overview = overview;
            wrapper.Row = new AssetPathTable.AssetPathRow();
            wrapper.Id = wrapper.GetAssetTypeBaseId(assetType) + id;
            wrapper.AssetType = assetType;
            wrapper.FileType = fileType;
            wrapper.Name = name;
            wrapper.AfterCreate = afterCreateAsset;
            return wrapper;
        }


        public void ConfirmCreate(int id, string name, string assetType, string name_cn, GameObject prefab, string fileType)
        {


            var fileName = $"{name}";
            var fullFileName = $"{fileName}.{fileType}";


            var assetPathRow = new AssetPathTable.AssetPathRow();
            assetPathRow.Id = id;
            assetPathRow.AssetPackageDir = Overview.Config.PackageDir;
            assetPathRow.AssetPath = fullFileName;
            assetPathRow.AssetType = assetType;
            assetPathRow.Name = name_cn;
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
            if (Id == 0 || Name.IsNullOrWhiteSpace())
            {
                EditorUtility.DisplayDialog("错误", "Id, Name, 命名空间,ArtPrefab  必须填写", "确定");
                // GUIUtility.ExitGUI();
                return;
            }


            if (StringUtility.ContainsChinese(Name))
            {
                EditorUtility.DisplayDialog("错误", "Name不能包含中文", "确定");
                // GUIUtility.ExitGUI();
                return;
            }

            if (StringUtility.IsOnlyDigit(Name))
            {
                EditorUtility.DisplayDialog("错误", "Name不能全是数字", "确定");
                return;
            }

            if (char.IsDigit(Name[0]))
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

            ConfirmCreate(Id, Name, AssetType, NameCn, ModelPrefab, FileType);
        }

    }
}