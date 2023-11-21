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
            window.MenuWidth = 200;
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
            where TOverview : ExcelTableOverview
            where TRowDetailWrapper : ExcelTableRowDetailWrapper<TOverview, TRow>, new()
            where TTableRowWrapper : ExcelTableTableRowWrapper<TOverview, TNewRowWrapper, TRow>, new()
            where TNewRowWrapper : ExcelTableRowNewWrapper<TOverview, TRow>, new()
            where TRow : ExcelNamedRowBase, new()
        {
            var overviews = AssetDatabaseUtility.FindAsset<TOverview>().ToList();
            var overviewEditor = new OverviewEditorBase<TOverview, TRowDetailWrapper, TTableRowWrapper, TNewRowWrapper, TRow>();
            overviewEditor.Overviews = overviews;
            overviewEditor.Window = this;
            overviewEditor.MenuKey = menuMainKey;
            overviewEditor.MenuDisplayName = menuDisplayName;
            overviewEditor.Tree = tree;
            overviewEditor.InitWrappers();
            tree.Add(menuDisplayName, overviewEditor);
            // // Debug.Log($"{menuMainKey} Wrapper Count:{overviewEditor.Wrappers.Count}");
            // foreach (var wrapper in overviewEditor.Wrappers)
            // {

            //     var customMenuItem = new OdinMenuItem(tree, GameEditorUtility.GetMenuItemKey(menuMainKey, wrapper.Id, wrapper.Name)
            //     , wrapper.DetailWrapper);
            //     tree.AddMenuItemAtPath(menuMainKey, customMenuItem);
            // }
        }
        void InitNewOverviews<TOverview, TRowDetailWrapper, TNewRowWrapper, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
            where TOverview : ExcelTableOverview
            where TRowDetailWrapper : ExcelTableRowDetailWrapper<TOverview, TRow>, new()
            where TNewRowWrapper : ExcelTableRowNewWrapper<TOverview, TRow>, new()
             where TRow : ExcelNamedRowBase, new()
        {
            InitOverviews<TOverview, TRowDetailWrapper, ExcelTableTableRowWrapper<TOverview, TNewRowWrapper, TRow>, TNewRowWrapper, TRow>(tree, menuMainKey, menuDisplayName);
        }


        void InitCommonOverviews<TOverview, TRowDetailWrapper, TTableRowWrapper, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
            where TOverview : ExcelTableOverview
            where TRowDetailWrapper : ExcelTableRowDetailWrapper<TOverview, TRow>, new()
            where TTableRowWrapper : ExcelTableTableRowWrapper<TOverview, ExcelTableRowNewWrapper<TOverview, TRow>, TRow>, new()
             where TRow : ExcelNamedRowBase, new()
        {
            InitOverviews<TOverview, TRowDetailWrapper, TTableRowWrapper, ExcelTableRowNewWrapper<TOverview, TRow>, TRow>(tree, menuMainKey, menuDisplayName);
        }

        void InitDetailOverviews<TOverview, TRowDetailWrapper, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
            where TOverview : ExcelTableOverview
            where TRowDetailWrapper : ExcelTableRowDetailWrapper<TOverview, TRow>, new()
            where TRow : ExcelNamedRowBase, new()
        {
            InitOverviews<TOverview, TRowDetailWrapper, ExcelTableTableRowWrapper<TOverview, ExcelTableRowNewWrapper<TOverview, TRow>, TRow>, ExcelTableRowNewWrapper<TOverview, TRow>, TRow>(tree, menuMainKey, menuDisplayName);
        }

        void InitDetailRowOverviews<TOverview, TRowDetailWrapper, TTableRowWrapper, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
            where TOverview : ExcelTableOverview
            where TRowDetailWrapper : ExcelTableRowDetailWrapper<TOverview, TRow>, new()
            where TTableRowWrapper : ExcelTableTableRowWrapper<TOverview, ExcelTableRowNewWrapper<TOverview, TRow>, TRow>, new()
            where TRow : ExcelNamedRowBase, new()
        {
            InitOverviews<TOverview, TRowDetailWrapper, TTableRowWrapper, ExcelTableRowNewWrapper<TOverview, TRow>, TRow>(tree, menuMainKey, menuDisplayName);
        }

        void InitBaseOverviews<TOverview, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
                    where TOverview : ExcelTableOverview
                    where TRow : ExcelNamedRowBase, new()
        {
            InitCommonOverviews<TOverview, ExcelTableRowDetailWrapper<TOverview, TRow>, ExcelTableTableRowWrapper<TOverview, ExcelTableRowNewWrapper<TOverview, TRow>, TRow>, TRow>(tree, menuMainKey, menuDisplayName);
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
            InitNewOverviews<AssetPathTableOverview, AssetPathDetailWrapper, AssetPathNewWrapper, AssetPathTable.AssetPathRow>(tree, "AssetPath", "资产路径");
            InitDetailOverviews<AssetGroupTableOverview, AssetGroupDetailWrapper, AssetGroupTable.AssetGroupRow>(tree, "AssetGroup", "资产组");

            // InitCommonOverviews<AssetPathTableOverview, AssetPathDetailWrapper, ExcelTableTableRowWrapper<AssetPathTableOverview, AssetPathTable.AssetPathRow>, AssetPathTable.AssetPathRow>(tree, "AssetPath");
            InitDetailOverviews<StaticSceneTableOverview, StaticSceneDetailWrapper, StaticSceneTable.StaticSceneRow>(tree, "StaticScene", "静态场景");
            InitDetailOverviews<DynamicObjectTableOverview, DynamicObjectDetailWrapper, DynamicObjectTable.DynamicObjectRow>(tree, "DynamicObject", "动态物体");
            InitDetailOverviews<GameSectionTableOverview, GameSectionDetailWrapper, GameSectionTable.GameSectionRow>(tree, "GameSection", "游戏段落");

            InitDetailOverviews<TriggerEventTableOverview, TriggerDetailWrapper, TriggerEventTable.TriggerEventRow>(tree, "Trigger", "触发器");
            InitDetailOverviews<InstructionTableOverview, InstructionDetailWrapper, InstructionTable.InstructionRow>(tree, "Instruction", "指令");

            InitDetailOverviews<ConditionTableOverview, ConditionDetailWrapper, ConditionTable.ConditionRow>(tree, "Condition", "条件");

            InitDetailOverviews<CharacterTableOverview, CharacterDetailWrapper, CharacterTable.CharacterRow>(tree, "Character", "角色");
            InitDetailOverviews<HotspotTableOverview, HotspotDetailWrapper, HotspotTable.HotspotRow>(tree, "Hotspot", "热点区域");

            InitDetailOverviews<VariablesTableOverview, VariablesDetailWrapper, VariablesTable.VariablesRow>(tree, "Variables", "变量");

            InitDetailRowOverviews<SoundTableOverview, SoundDetailWrapper, SoundRowWrapper, SoundTable.SoundRow>(tree, "Sound", "音频");
            InitDetailOverviews<SimpleUITableOverview, SimpleUIDetailWrapper, SimpleUITable.SimpleUIRow>(tree, "UI", "UI");



            return tree;
        }
    }

}