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
    public class ModelEditor : OdinMenuEditorWindow
    {

        // [MenuItem("Pangoo/模型编辑", false, 6)]
        public static void ShowWindow()
        {
            var window = GetWindow<ModelEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 700);
            window.titleContent = new GUIContent("模型编辑");
            window.MenuWidth = 180;

            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.AutoScrollOnSelectionChanged = false;
            var exporter = AssetDatabaseUtility.FindAssetFirst<ModelExporter>();
            if (exporter == null)
            {
                exporter = ScriptableObject.CreateInstance<ModelExporter>();
                AssetDatabase.CreateAsset(exporter, "Assets/test.asset");
            }

            var tool = exporter;
            tree.Add("模型导出工具", tool);

            return tree;
        }


    }
}

