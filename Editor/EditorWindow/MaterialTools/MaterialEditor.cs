
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
    public class MaterialEditor : OdinMenuEditorWindow
    {

        [MenuItem("Pangoo/工具/Material", false, 6)]
        public static void ShowWindow()
        {
            var window = GetWindow<MaterialEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 700);
            window.titleContent = new GUIContent("Material编辑");
            window.MenuWidth = 180;


            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.AutoScrollOnSelectionChanged = false;
            var materialListTools = new MaterialListTools();
            tree.Add("Material列表", materialListTools);


            var TextureBuild = new BuildTextureTools();
            tree.Add("Texture生成", TextureBuild);
            return tree;
        }


    }
}

