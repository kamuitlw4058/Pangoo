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


        void InitOverviews<TOverview, TRowDetailWrapper, TTableRowWrapper, TRow>(OdinMenuTree tree, string menuMainKey)
            where TOverview : ExcelTableOverview
            where TRowDetailWrapper : ExcelTableRowDetailWrapper<TOverview, TRow>, new()
            where TTableRowWrapper : ExcelTableTableRowWrapper<TOverview, TRow>, new()
            where TRow : ExcelNamedRowBase
        {
            var overviews = AssetDatabaseUtility.FindAsset<TOverview>().ToList();
            var overviewEditor = new OverviewEditorBase<TOverview, TRowDetailWrapper, TTableRowWrapper, TRow>();
            overviewEditor.Overviews = overviews;
            overviewEditor.Window = this;
            overviewEditor.MenuKey = menuMainKey;
            overviewEditor.InitWrappers();
            tree.Add(menuMainKey, overviewEditor);
            Debug.Log($"{menuMainKey} Wrapper Count:{overviewEditor.Wrappers.Count}");
            foreach (var wrapper in overviewEditor.Wrappers)
            {

                var customMenuItem = new OdinMenuItem(tree, GameEditorUtility.GetMenuItemKey(menuMainKey, wrapper.Id, wrapper.Name)
                , wrapper.DetailWrapper);
                tree.AddMenuItemAtPath(menuMainKey, customMenuItem);
            }
        }

        void InitBaseOverviews<TOverview, TRow>(OdinMenuTree tree, string menuMainKey)
                    where TOverview : ExcelTableOverview
                    where TRow : ExcelNamedRowBase
        {
            InitOverviews<TOverview, ExcelTableRowDetailWrapper<TOverview, TRow>, ExcelTableTableRowWrapper<TOverview, TRow>, TRow>(tree, menuMainKey);
        }



        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.AutoScrollOnSelectionChanged = false;
            // 
            // var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>().ToList();
            // tree.Add("Volume编辑", new VolumeOverviewEditor(overviews, this));

            // foreach (var overview in overviews)
            // {
            //     foreach (var volumeRow in overview.Data.Rows)
            //     {
            //         var volume_dir_path = PathUtility.Join(overview.Config.PackageDir, "StreamRes/Volume");
            //         var volume_file_path = PathUtility.Join(volume_dir_path, $"{volumeRow.Name}.asset");
            //         VolumeProfile volume_so = AssetDatabaseUtility.LoadAssetAtPath<VolumeProfile>(volume_file_path);
            //         var customMenuItem = new OdinMenuItem(tree,
            //         $"Volume-{volumeRow.Id}-{volumeRow.Name} ", volume_so);
            //         tree.AddMenuItemAtPath("Volume编辑", customMenuItem);
            //     }
            // }

            InitOverviews<AssetPathTableOverview, AssetPathDetailWrapper, ExcelTableTableRowWrapper<AssetPathTableOverview, AssetPathTable.AssetPathRow>, AssetPathTable.AssetPathRow>(tree, "AssetPath");
            InitOverviews<StaticSceneTableOverview, StaticSceneDetailWrapper, ExcelTableTableRowWrapper<StaticSceneTableOverview, StaticSceneTable.StaticSceneRow>, StaticSceneTable.StaticSceneRow>(tree, "StaticScene");
            InitBaseOverviews<DynamicObjectTableOverview, DynamicObjectTable.DynamicObjectRow>(tree, "DynamicObject");
            return tree;
        }
    }

}