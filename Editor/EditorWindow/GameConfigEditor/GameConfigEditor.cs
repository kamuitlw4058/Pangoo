using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;


namespace Pangoo.Editor
{
    public class GameConfigEditor : OdinMenuEditorWindow
    {
        [MenuItem("Pangoo/游戏配置", false, 9)]
        private static void OpenWindow()
        {
            var window = GetWindow<GameConfigEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1600, 700);
            window.titleContent = new GUIContent("游戏配置");
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



        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.AutoScrollOnSelectionChanged = false;

            var config = AssetDatabaseUtility.FindAssetFirst<GameMainConfig>();
            if (config != null)
            {
                tree.Add("游戏主配置", config);
            }


            return tree;
        }
    }

}