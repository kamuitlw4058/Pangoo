
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
    public class DataTableEditor : OdinMenuEditorWindow
    {

        // [MenuItem("Pangoo/ExcelTable编辑", false, 6)]
        // public static void ShowWindow()
        // {
        //     var window = GetWindow<DataTableEditor>();
        //     window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 700);
        //     window.titleContent = new GUIContent("Pangoo编辑");
        //     window.MenuWidth = 180;



        //     window.Show();
        // }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.AutoScrollOnSelectionChanged = false;
            var dataTableTools = new DataTableTools();
            tree.Add("ExcelTable", dataTableTools);


            var datas = AssetDatabaseUtility.FindAsset<ExcelTableOverview>();
            foreach (var data in datas)
            {
                var item = new OdinMenuItem(tree, $"{data.Namespace}.{data.GetName()}", data);
                tree.AddMenuItemAtPath("ExcelTable", item);
            }

            return tree;
        }


    }
}

