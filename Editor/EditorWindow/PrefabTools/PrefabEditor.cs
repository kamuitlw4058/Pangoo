using System;
using System.IO;
using System.Linq;
using Pangoo;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Pangoo.Editor
{
    public class PrefabEditor : OdinMenuEditorWindow
    {

        [MenuItem("Pangoo/预制体编辑", false, 6)]
        public static void ShowWindow()
        {
            var window = GetWindow<PrefabEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 700);
            window.titleContent = new GUIContent("预制体编辑");
            window.MenuWidth = 180;

            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.AutoScrollOnSelectionChanged = false;
            var tool = new PrefabTool();
            tree.Add("替换工具", tool);

            // var datas = AssetDatabaseUtility.FindAsset<ExcelTableOverview>();
            // foreach (var data in datas)
            // {
            //     var item = new OdinMenuItem(tree, data.GetName(), data);
            //     tree.AddMenuItemAtPath("ExcelTable", item);
            // }

            return tree;
        }


    }
}
