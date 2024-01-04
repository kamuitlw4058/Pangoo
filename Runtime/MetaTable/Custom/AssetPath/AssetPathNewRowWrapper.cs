#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using MetaTable;
using UnityEditor;
using Pangoo.Common;
using Sirenix.OdinInspector.Editor;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class AssetPathNewRowWrapper : MetaTableNewRowWrapper<AssetPathOverview, UnityAssetPathRow>
    {



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

                if (UnityRow.Row.AssetType == null || UnityRow.Row.AssetType == string.Empty)
                {
                    UnityRow.Row.AssetType = ConstExcelTable.DynamicObjectAssetTypeName;
                    UnityRow.Row.Id = 0;
                }

                return UnityRow.Row?.AssetType;
            }
            set
            {

                UnityRow.Row.AssetType = value;
                UnityRow.Row.Id = 0;
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

        [ShowInInspector]
        public string AssetPath
        {
            get
            {
                return UnityRow.Row.AssetPath;
            }
        }

        public void UpdateAssetPath()
        {
            UnityRow.Row.AssetPath = $"{m_AssetName}.{m_FileType}";
        }

        public override void OnNameChanged()
        {
            OnAssetNameChangeChanged();
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

                return AssetUtility.GetAssetPath(Overview.Config.PackageDir, UnityRow.Row.AssetType, UnityRow.Row.AssetPath);
            }
        }

        public IEnumerable OnAssetTypeDropdown()
        {
            var ret = new ValueDropdownList<string>();
            ret.Add(ConstExcelTable.DynamicObjectAssetTypeName);
            ret.Add(ConstExcelTable.StaticSceneAssetTypeName);
            ret.Add(ConstExcelTable.CharacterAssetTypeName);
            return ret;
        }

        public enum AssetNameChangeType
        {
            [LabelText("跟随名字")]
            WithName,
            [LabelText("跟随模型名")]
            WithModelName,

            [LabelText("手动控制")]
            Manual
        }

        [LabelText("资源名改变策略")]
        [OnValueChanged("OnAssetNameChangeChanged")]
        [TabGroup("新建预制体")]
        [PropertyOrder(9)]

        public AssetNameChangeType AssetNameChange;

        void OnAssetNameChangeChanged()
        {
            switch (AssetNameChange)
            {
                case AssetNameChangeType.WithName:
                    AssetName = GetPrefixByAssetType(AssetType) + Name.ToPinyin();
                    break;
                case AssetNameChangeType.WithModelName:
                    if (ModelPrefab != null)
                    {
                        AssetName = GetPrefixByAssetType(AssetType) + ModelPrefab.name;
                    }
                    break;
            }
        }



        string m_AssetName;

        [ShowInInspector]
        [PropertyOrder(9)]
        [TabGroup("新建预制体")]
        [LabelText("资源名")]
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

        [ShowInInspector]
        [ValueDropdown("OnAssetGroupDropdown")]
        public string AssetGroup
        {
            get
            {
                return AssetGroupOverview.GetAssetGroupUuidByAssetGroup(UnityRow.Row.AssetGroup);
            }
            set
            {
                var newGroup = AssetGroupOverview.GetAssetGroupByAssetGroupUuid(value);
                UnityRow.Row.AssetGroup = newGroup;
            }
        }

        IEnumerable OnAssetGroupDropdown()
        {
            return AssetGroupOverview.GetUuidDropdown();
        }



        [LabelText("模型预制体")]
        [AssetsOnly]
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

            OnAssetNameChangeChanged();
        }


        public static AssetPathNewRowWrapper Create(AssetPathOverview overview, string assetType = "", string name = "", string fileType = "prefab", Action<string> afterCreateAsset = null, string assetGroup = null)
        {
            var wrapper = new AssetPathNewRowWrapper();
            wrapper.Overview = overview;
            wrapper.UnityRow = ScriptableObject.CreateInstance<UnityAssetPathRow>();
            wrapper.UnityRow.Row.AssetPackageDir = overview.Config.PackageDir;
            wrapper.UnityRow.Row.AssetType = assetType;
            wrapper.UnityRow.Row.Id = 0;
            wrapper.UnityRow.Row.Uuid = UuidUtility.GetNewUuid();
            wrapper.FileType = fileType;
            wrapper.Name = name;
            wrapper.AfterCreate = afterCreateAsset;
            wrapper.AssetName = GetPrefixByAssetType(assetType) + name.ToPinyin();
            wrapper.ShowCreateButton = false;
            wrapper.AssetGroup = AssetGroupOverview.GetAssetGroupUuidByAssetGroup(assetGroup);
            return wrapper;
        }

        public void ConfirmCreateNew()
        {
            if (!Directory.Exists(UnityRow.ToDirPath()))
            {
                Directory.CreateDirectory(UnityRow.ToDirPath());
            }

            AssetDatabase.CreateAsset(UnityRow, Overview.RowPath(UnityRow.Uuid));
            Overview.Rows.Add(UnityRow);
            EditorUtility.SetDirty(Overview);

            var go = new GameObject(Name);
            if (ModelPrefab != null)
            {
                var prefab_go = PrefabUtility.InstantiatePrefab(ModelPrefab) as GameObject;
                prefab_go.transform.SetParent(go.transform);
                prefab_go.ResetTransfrom(false);
                prefab_go.name = ConstExcelTable.SubModelName;
            }

            PrefabUtility.SaveAsPrefabAsset(go, UnityRow.ToPrefabPath());
            GameObject.DestroyImmediate(go);
            AssetDatabase.SaveAssets();

            if (AfterCreate != null)
            {
                AfterCreate(UnityRow.Uuid);
            }

            if (OpenWindow != null)
            {
                OpenWindow.Close();
            }


        }

        public void ConfirmCreateRef()
        {

            AssetName = RefAssetName;
            AssetDatabase.CreateAsset(UnityRow, Overview.RowPath(UnityRow.Uuid));
            Overview.Rows.Add(UnityRow);
            EditorUtility.SetDirty(Overview);
            AssetDatabase.SaveAssets();

            if (AfterCreate != null)
            {
                AfterCreate(UnityRow.Uuid);
            }

            if (OpenWindow != null)
            {
                OpenWindow.Close();
            }

        }
        public bool CheckParams(bool assetNameType = true)
        {
            if (Name.IsNullOrWhiteSpace() || (assetNameType && AssetName.IsNullOrWhiteSpace()) || (!assetNameType && RefAssetName.IsNullOrWhiteSpace()))
            {
                EditorUtility.DisplayDialog("错误", " Name, 命名空间,ArtPrefab  必须填写", "确定");
                // GUIUtility.ExitGUI();
                return false;
            }

            if (assetNameType)
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

            ConfirmCreateNew();
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

            ConfirmCreateRef();
        }

    }
}
#endif

