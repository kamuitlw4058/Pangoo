using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Pangoo.Editor
{
    public class GameEditor : OdinMenuEditorWindow
    {
        [MenuItem("Pangoo/资源编辑器", false, 10)]
        private static void OpenWindow()
        {
            var window = GetWindow<GameEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1100, 700);
            window.titleContent = new GUIContent("资源编辑器");
            window.MenuWidth = 280;
        }

        public string GetMenuItemKey(string model, int id, string name)
        {
            return $"AssetPath-{id}-{name}";

        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.AutoScrollOnSelectionChanged = false;
            // 
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>().ToList();
            tree.Add("Volume编辑", new VolumeOverviewEditor(overviews, this));

            foreach (var overview in overviews)
            {
                foreach (var volumeRow in overview.Data.Rows)
                {
                    var volume_dir_path = PathUtility.Join(overview.Config.PackageDir, "StreamRes/Volume");
                    var volume_file_path = PathUtility.Join(volume_dir_path, $"{volumeRow.Name}.asset");
                    VolumeProfile volume_so = AssetDatabaseUtility.LoadAssetAtPath<VolumeProfile>(volume_file_path);
                    var customMenuItem = new OdinMenuItem(tree,
                    $"Volume-{volumeRow.Id}-{volumeRow.Name} ", volume_so);
                    tree.AddMenuItemAtPath("Volume编辑", customMenuItem);
                }
            }

            var assetPathOverviews = AssetDatabaseUtility.FindAsset<AssetPathTableOverview>().ToList();
            var overviewEditor = new OverviewEditorBase<AssetPathTableOverview, ExcelTableRowTableWrapper<AssetPathTableOverview, AssetPathTable.AssetPathRow>, AssetPathTable.AssetPathRow>();
            overviewEditor.Overviews = assetPathOverviews;
            overviewEditor.Window = this;
            overviewEditor.Model = "AssetPath";
            overviewEditor.InitWrappers();
            tree.Add("AssetPath编辑", overviewEditor);
            Debug.Log($"AssetPath Wrapper Count:{overviewEditor.Wrappers.Count}");
            foreach (var wrapper in overviewEditor.Wrappers)
            {

                var customMenuItem = new OdinMenuItem(tree, GetMenuItemKey("AssetPath", wrapper.Id, wrapper.Name)
                , wrapper);
                tree.AddMenuItemAtPath("AssetPath编辑", customMenuItem);
            }


            return tree;
        }
    }

}