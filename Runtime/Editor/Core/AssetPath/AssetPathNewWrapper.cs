#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;
using Sirenix.Utilities;




using UnityEditor;
using Sirenix.OdinInspector.Editor;
using System.IO;


namespace Pangoo
{
    public class AssetPathNewWrapper : ExcelTableRowNewWrapper<AssetPathTableOverview, AssetPathTable.AssetPathRow>
    {

        public int GetAssetTypeBaseId(AssetPathTableOverview overview, string assetType)
        {
            if (overview == null || assetType.IsNullOrWhiteSpace())
            {
                return 0;
            }

            switch (assetType)
            {
                case ConstExcelTable.DynamicObjectAssetTypeName:
                    return overview.Config.AssetPathBaseId + overview.Config.DynmaicObjectBaseId;
                case ConstExcelTable.StaticSceneAssetTypeName:
                    return overview.Config.AssetPathBaseId + overview.Config.StaticSceneBaseId;
                case ConstExcelTable.CharacterAssetTypeName:
                    return overview.Config.AssetPathBaseId + overview.Config.CharacterBaseId;
                case ConstExcelTable.UIAssetTypeName:
                    return overview.Config.AssetPathBaseId + overview.Config.UIBaseId;
            }



            return 0;
        }

        public static string GetPrefixByAssetType(string assetType)
        {
            switch (assetType)
            {
                case ConstExcelTable.DynamicObjectAssetTypeName:
                    return "DO_";
                case ConstExcelTable.StaticSceneAssetTypeName:
                    return "SS_";
                case ConstExcelTable.CharacterAssetTypeName:
                    return "CA_";
                case ConstExcelTable.UIAssetTypeName:
                    return "UI_";
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
                        Row.Id = GetAssetTypeBaseId(Overview, Row.AssetType);
                    }
                }

                return Row?.AssetType;
            }
            set
            {
                if (Row != null)
                {
                    Row.AssetType = value;
                    Row.Id = GetAssetTypeBaseId(Overview, value);
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




        [ShowInInspector]
        [TabGroup("新建预制体")]
        [PropertyOrder(11)]
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
        [PropertyOrder(9)]
        [TabGroup("新建预制体")]
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
        // [AssetSelector(ExpandAllMenuItems = false, Filter = "p: prefab")]
        [OnValueChanged("OnModelPrefabChanged")]
        [PropertyOrder(10)]
        [TabGroup("新建预制体")]
        public GameObject ModelPrefab;


        [TabGroup("创建资产引用")]
        [LabelText("引用预制体")]
        [ValueDropdown("OnRefAssetValueDropdown")]
        [PropertyOrder(11)]
        public string RefAssetName;

        IEnumerable OnRefAssetValueDropdown()
        {
            var ret = new ValueDropdownList<string>();
            var assetDir = AssetUtility.GetAssetPathDir(Overview.Config.PackageDir, AssetType);
            var assets = AssetDatabaseUtility.FindAsset<GameObject>(assetDir);
            foreach (var asset in assets)
            {
                ret.Add(asset.name);
            }
            return ret;
        }


        void OnModelPrefabChanged()
        {
            if (AssetName.IsNullOrWhiteSpace())
            {
                AssetName = GetPrefixByAssetType(AssetType) + ModelPrefab.name;
            }

        }

        public static AssetPathNewWrapper Create(AssetPathTableOverview overview, int id = 0, string assetType = "", string name = "", string fileType = "prefab", Action<int> afterCreateAsset = null)
        {
            var wrapper = new AssetPathNewWrapper();
            wrapper.Overview = overview;
            wrapper.Row = new AssetPathTable.AssetPathRow();
            wrapper.AssetType = assetType;
            wrapper.Id = wrapper.GetAssetTypeBaseId(overview, assetType) + id;
            wrapper.FileType = fileType;
            wrapper.Name = name;
            wrapper.AfterCreate = afterCreateAsset;
            wrapper.AssetName = GetPrefixByAssetType(assetType) + name.ToPinyin();
            wrapper.ShowCreateButton = false;
            return wrapper;
        }


        public void ConfirmCreateNew(int id, string name, string assetType, string assetName, GameObject prefab, string fileType)
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

            if (prefab != null)
            {
                var prefab_go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                prefab_go.transform.SetParent(go.transform);
                prefab_go.ResetTransfrom(false);
                prefab_go.name = ConstExcelTable.SubModelName;
            }

            if (!Directory.Exists(assetPathRow.ToDirPath()))
            {
                Directory.CreateDirectory(assetPathRow.ToDirPath());
            }


            PrefabUtility.SaveAsPrefabAsset(go, assetPathRow.ToPrefabPath());
            GameObject.DestroyImmediate(go);


            AssetDatabase.SaveAssets();
            if (AfterCreate != null)
            {
                AfterCreate(id);
            }

            if (Window != null)
            {
                Window.Close();
            }


        }

        public void ConfirmCreateRef(int id, string name, string assetType, string assetName, string fileType)
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
            AssetDatabase.SaveAssets();
            if (AfterCreate != null)
            {
                AfterCreate(id);
            }

            if (Window != null)
            {
                Window.Close();
            }


        }
        public bool CheckParams(bool checkAssetName = true)
        {
            if (Id == 0 || Name.IsNullOrWhiteSpace() || (checkAssetName && AssetName.IsNullOrWhitespace()) || (!checkAssetName && RefAssetName.IsNullOrWhiteSpace()))
            {
                EditorUtility.DisplayDialog("错误", "Id, Name, 命名空间,ArtPrefab  必须填写", "确定");
                // GUIUtility.ExitGUI();
                return false;
            }

            if (checkAssetName)
            {
                if (StringUtility.ContainsChinese(AssetName))
                {
                    EditorUtility.DisplayDialog("错误", "Name不能包含中文", "确定");
                    // GUIUtility.ExitGUI();
                    return false;
                }

                if (StringUtility.IsOnlyDigit(AssetName))
                {
                    EditorUtility.DisplayDialog("错误", "Name不能全是数字", "确定");
                    return false;
                }

                if (char.IsDigit(AssetName[0]))
                {
                    EditorUtility.DisplayDialog("错误", "Name开头不能是数字", "确定");
                    return false;
                }
            }



            if (CheckExistsId())
            {
                EditorUtility.DisplayDialog("错误", "Id已经存在", "确定");
                return false;
            }

            if (CheckExistsName())
            {
                EditorUtility.DisplayDialog("错误", "Name已经存在", "确定");
                return false;
            }

            return true;

        }



        [TabGroup("新建预制体")]
        [Button("新建预制体资产")]
        [PropertyOrder(12)]
        public void CreateNew()
        {
            if (!CheckParams())
            {
                return;
            }

            ConfirmCreateNew(Id, Name, AssetType, AssetName, ModelPrefab, FileType);
        }


        [TabGroup("创建资产引用")]
        [Button("引用预制体资产")]
        [PropertyOrder(12)]
        public void CreateRef()
        {
            if (!CheckParams(false))
            {
                return;
            }

            ConfirmCreateRef(Id, Name, AssetType, RefAssetName, FileType);
        }





    }
}
#endif