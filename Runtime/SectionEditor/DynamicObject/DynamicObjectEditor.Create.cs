
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


        private static OdinEditorWindow m_CreateAssetPathWindow;

        [Button("创建新的动态物体", ButtonSizes.Large)]
        public void BuildDynamicObject()
        {
            m_CreateDynamicObjectWindow = OdinEditorWindow.InspectObject(new DynamicObjectCreateWindow(this));
        }

        public void ConfirmCreate(int id, int assetPathId, string name, string name_cn)
        {

            PackageConfig pakcageConfig = GameSupportEditorUtility.GetPakcageConfigByOverviewRowId<GameSectionTableOverview>(Section);
            GameSectionTableOverview gameSectionTableOverview = AssetDatabaseUtility.FindAssetFirst<GameSectionTableOverview>(pakcageConfig.PackageDir);
            DynamicObjectTableOverview overview = AssetDatabaseUtility.FindAssetFirst<DynamicObjectTableOverview>(pakcageConfig.PackageDir);
            Debug.Log($"overview:{overview}, overview.{overview.Config.PackageDir}");

            var gameSectionRow = gameSectionTableOverview.Data.GetRowById(Section);


            var row = new DynamicObjectTable.DynamicObjectRow();
            row.Id = id;
            row.Name = name;
            row.NameCn = name_cn;
            row.AssetPathId = assetPathId;
            overview.Data.Rows.Add(row);
            EditorUtility.SetDirty(overview);


            gameSectionRow.AddDynamicObjectId(id);
            EditorUtility.SetDirty(gameSectionTableOverview);

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


            [LabelText("资源ID")]
            [ValueDropdown("AssetPathIdValueDropdown")]
            [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
            public int AssetPathId;


            public IEnumerable AssetPathIdValueDropdown()
            {
                return GameSupportEditorUtility.GetAssetPathIds(ids: new List<int> { AssetPathId }, assetTypes: new List<string> { "DynamicObject" });
            }

            public void ShowCreateAssetPath()
            {
                PackageConfig config = GameSupportEditorUtility.GetPakcageConfigByOverviewRowId<GameSectionTableOverview>(m_Editor.Section);
                var window = new AssetPathWrapper(config, Id, ConstExcelTable.DynamicObjectAssetTypeName, Name, ConstExcelTable.PrefabType, AfterCreateAsset);
                m_CreateAssetPathWindow = OdinEditorWindow.InspectObject(window);
            }

            public void AfterCreateAsset(int id)
            {
                AssetPathId = id;
                m_CreateAssetPathWindow.Close();
            }


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

                m_Editor.ConfirmCreate(Id, AssetPathId, Name, NameCn);
            }
        }
    }
}
#endif