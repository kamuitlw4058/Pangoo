using Pangoo;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.IO;
using System.Text.RegularExpressions;


namespace Pangoo.Editor
{
    public class HierarchyEditor : OdinMenuEditorWindow
    {

        [MenuItem("Pangoo/场景工具", false, 10)]
        private static void OpenWindow()
        {
            var window = GetWindow<HierarchyEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1100, 700);
            window.titleContent = new GUIContent("场景工具");
            window.MenuWidth = 280;
        }



        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.AutoScrollOnSelectionChanged = false;

            tree.Add("引用查找", new ReferenceEditor(this));
            tree.Add("Id查找", new HierarchyIdEditor());

            return tree;
        }

    }
}