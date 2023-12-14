using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using MetaTable.Editor;
using MetaTable;
using Pangoo.MetaTable;


namespace Pangoo.Editor
{
    public class MetaTableEditor : OdinMenuEditorWindow
    {
        [MenuItem("Pangoo/MetaTable", false, 10)]
        private static void OpenWindow()
        {
            var window = GetWindow<MetaTableEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1100, 700);
            window.titleContent = new GUIContent("MetaTable");
            window.MenuWidth = 250;
        }
        protected override void OnBeginDrawEditors()
        {
            if (MenuTree == null)
                return;

            var toolbarHeight = MenuTree.Config.SearchToolbarHeight;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                // GUILayout.Label("提交拉取前务必点击保存全部配置");


                if (SirenixEditorGUI.ToolbarButton(new GUIContent("刷新菜单树")))
                {
                    ForceMenuTreeRebuild();
                }

            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        void InitOverviews<TOverview, TRowDetailWrapper, TTableRowWrapper, TNewRowWrapper, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
            where TOverview : MetaTableOverview
            where TRowDetailWrapper : MetaTableDetailRowWrapper<TOverview, TRow>, new()
            where TTableRowWrapper : MetaTableRowWrapper<TOverview, TNewRowWrapper, TRow>, new()
            where TNewRowWrapper : MetaTableNewRowWrapper<TOverview, TRow>, new()
            where TRow : MetaTableUnityRow, new()
        {
            var overviews = AssetDatabaseUtility.FindAsset<TOverview>().ToList();
            var overviewEditor = new MetaTableOverviewEditor<TOverview, TRowDetailWrapper, TTableRowWrapper, TNewRowWrapper, TRow>();
            overviewEditor.Overviews = overviews;
            overviewEditor.MenuWindow = this;
            // overviewEditor.MenuKey = menuMainKey;
            overviewEditor.MenuDisplayName = menuDisplayName;
            overviewEditor.Tree = tree;
            overviewEditor.InitWrappers();
            tree.Add(menuDisplayName, overviewEditor);

        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.AutoScrollOnSelectionChanged = false;

            InitOverviews<AssetGroupOverview, AssetGroupDetailRowWrapper, AssetGroupRowWrapper, AssetGroupNewRowWrapper, UnityAssetGroupRow>(tree, null, "资源组");

            return tree;
        }
    }

}