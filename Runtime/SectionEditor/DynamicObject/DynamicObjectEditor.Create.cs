
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo;

namespace Pangoo.Editor
{
    public partial class DynamicObjectEditor : MonoBehaviour
    {
        public const string SubModelName = "Model";
        private OdinEditorWindow m_CreateDynamicObjectWindow;

        [Button("创建新的动态物体", ButtonSizes.Large)]
        public void BuildDynamicObject()
        {
            m_CreateDynamicObjectWindow = OdinEditorWindow.InspectObject(new DynamicObjectCreateWindow(this));
        }

        public void ConfirmCreate(int id, string name, string name_cn, GameObject prefab)
        {

            PackageConfig pakcageConfig = GameSupportEditorUtility.GetPakcageConfigByOverviewRowId<GameSectionTableOverview>(Section);
            GameSectionTableOverview gameSectionTableOverview = AssetDatabaseUtility.FindAssetFirst<GameSectionTableOverview>(pakcageConfig.PackageDir);
            DynamicObjectTableOverview overview = AssetDatabaseUtility.FindAssetFirst<DynamicObjectTableOverview>(pakcageConfig.PackageDir);
            Debug.Log($"overview:{overview}, overview.{overview.Config.PackageDir}");
            AssetPathTableOverview assetPathTableOverview = AssetDatabaseUtility.FindAssetFirst<AssetPathTableOverview>(pakcageConfig.PackageDir);
            // assetPackageTableOverview.GetAssetPackageIdByConfig

            var gameSectionRow = gameSectionTableOverview.Data.GetRowById(Section);



            var prefab_name = $"{name}";
            var prefab_file_name = $"{prefab_name}.prefab";

            var assetPathId = ConstExcelTable.DynamicObjectAssetPathIdBase + id;

            var assetPathRow = new AssetPathTable.AssetPathRow();
            assetPathRow.Id = assetPathId;
            assetPathRow.AssetPackageDir = pakcageConfig.PackageDir;
            assetPathRow.AssetPath = prefab_file_name;
            assetPathRow.AssetType = ConstExcelTable.DynamicObjectAssetTypeName;
            assetPathRow.Name = name;
            assetPathTableOverview.Data.Rows.Add(assetPathRow);
            EditorUtility.SetDirty(assetPathTableOverview);



            var row = new DynamicObjectTable.DynamicObjectRow();
            row.Id = id;
            row.Name = name;
            row.NameCn = name_cn;
            row.AssetPathId = assetPathId;
            overview.Data.Rows.Add(row);
            EditorUtility.SetDirty(overview);



            var go = new GameObject(prefab_name);
            go.transform.parent = transform;
            var helper = go.AddComponent<DynamicObjectEditorHelper>();
            go.ResetTransfrom();

            helper.DynamicObjectId = id;
            helper.Model = prefab;

            var prefab_go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            prefab_go.transform.parent = go.transform;
            prefab_go.ResetTransfrom(false);
            prefab_go.name = SubModelName;

            gameSectionRow.AddDynamicObjectId(id);
            EditorUtility.SetDirty(gameSectionTableOverview);

            // Debug.Log($"assetPathRow:{assetPathRow.ToPrefabPath()}");


            // string prefabPath = "Assets/Prefabs/MyPrefab.prefab";
            PrefabUtility.SaveAsPrefabAsset(go, assetPathRow.ToPrefabPath());

            GameObject.DestroyImmediate(go);
            AssetDatabase.SaveAssets();
            OnSectionChange();
        }


        public class DynamicObjectCreateWindow
        {

            public int Id = 0;

            [LabelText("名字")]
            public string Name = "";

            [LabelText("中文名")]
            public string NameCn = "";


            [LabelText("模型预制体")]
            [AssetsOnly]
            [AssetSelector]
            public GameObject ArtPrefab;


            DynamicObjectEditor m_Editor;

            public DynamicObjectCreateWindow(DynamicObjectEditor editor)
            {
                m_Editor = editor;
            }


            public DynamicObjectCreateWindow()
            {
            }

            [Button("新建", ButtonSizes.Large)]
            public void Create()
            {

                if (Id == 0 || Name.IsNullOrWhiteSpace() || ArtPrefab == null)
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

                if (!GameSupportEditorUtility.ExistsExcelTableOverviewId<DynamicObjectTableOverview>(Id))
                {
                    EditorUtility.DisplayDialog("错误", "Id已经存在", "确定");
                    return;
                }

                if (!GameSupportEditorUtility.ExistsExcelTableOverviewName<DynamicObjectTableOverview>(Name))
                {
                    EditorUtility.DisplayDialog("错误", "Name已经存在", "确定");
                    return;
                }

                m_Editor.ConfirmCreate(Id, Name, NameCn, ArtPrefab);
            }
        }
    }
}
#endif