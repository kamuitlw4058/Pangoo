
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo;
using System;

namespace Pangoo.Editor
{
    [Serializable]
    public class AssetPathWrapper
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
        PackageConfig Config;

        Action<int> AfterCreateAsset;

        public AssetPathWrapper(PackageConfig config, int id = 0, string assetType = "", string name = "", string fileType = "", Action<int> afterCreateAsset = null)
        {
            Config = config;
            Id = GetAssetTypeBaseId(assetType) + id;
            AssetType = assetType;
            FileType = fileType;
            Name = name;
            AfterCreateAsset = afterCreateAsset;
        }


        [LabelText("资源ID")]
        [InfoBox("Id 已经存在", InfoMessageType.Error, "CheckExistsId")]
        public int Id = 0;


        [EnableIf("@this.AssetType == \"\" || this.AssetType == null")]
        public string AssetType;

        [EnableIf("@this.FileType == \"\" || this.FileType == null")]
        public string FileType;


        [InfoBox("已经有对应的名字", InfoMessageType.Error, "CheckExistsName")]
        public string Name;


        public string NameCn;



        [LabelText("模型预制体")]
        [AssetsOnly]
        [AssetSelector(ExpandAllMenuItems = false)]
        [OnValueChanged("OnModelPrefabChanged")]
        public GameObject ModelPrefab;

        void OnValueChanged()
        {
            if (Id == 0) return;

        }

        void OnModelPrefabChanged()
        {
            Name = GetPrefixByAssetType(AssetType) + ModelPrefab.name;
        }

        bool CheckExistsId()
        {
            return GameSupportEditorUtility.ExistsExcelTableOverviewId<AssetPathTableOverview>(Id);
        }

        bool CheckExistsName()
        {
            return GameSupportEditorUtility.ExistsAssetPath(Config.PackageDir, Name, AssetType, FileType);
        }

        public void ConfirmCreate(int id, string name, string assetType, string name_cn, GameObject prefab, string fileType)
        {

            AssetPathTableOverview assetPathTableOverview = AssetDatabaseUtility.FindAssetFirst<AssetPathTableOverview>(Config.PackageDir);

            var fileName = $"{name}";
            var fullFileName = $"{fileName}.{fileType}";


            var assetPathRow = new AssetPathTable.AssetPathRow();
            assetPathRow.Id = id;
            assetPathRow.AssetPackageDir = Config.PackageDir;
            assetPathRow.AssetPath = fullFileName;
            assetPathRow.AssetType = assetType;
            assetPathRow.Name = name_cn;
            assetPathTableOverview.Data.Rows.Add(assetPathRow);
            EditorUtility.SetDirty(assetPathTableOverview);




            var go = new GameObject(fileName);

            var prefab_go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            prefab_go.transform.parent = go.transform;
            prefab_go.ResetTransfrom(false);
            prefab_go.name = ConstExcelTable.SubModelName;

            // string prefabPath = "Assets/Prefabs/MyPrefab.prefab";
            PrefabUtility.SaveAsPrefabAsset(go, assetPathRow.ToPrefabPath());
            GameObject.DestroyImmediate(go);


            AssetDatabase.SaveAssets();
            if (AfterCreateAsset != null)
            {
                AfterCreateAsset(id);
            }
        }


        [Button("新建", ButtonSizes.Large)]
        public void Create()
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
#endif