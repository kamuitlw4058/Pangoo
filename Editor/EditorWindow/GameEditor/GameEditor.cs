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

        void InitOverviews<TOverview, TRowDetailWrapper, TTableRowWrapper, TNewRowWrapper, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
            where TOverview : ExcelTableOverview
            where TRowDetailWrapper : ExcelTableRowDetailWrapper<TOverview, TRow>, new()
            where TTableRowWrapper : ExcelTableTableRowWrapper<TOverview, TRow>, new()
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


        void InitCommonOverviews<TOverview, TRowDetailWrapper, TTableRowWrapper, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
            where TOverview : ExcelTableOverview
            where TRowDetailWrapper : ExcelTableRowDetailWrapper<TOverview, TRow>, new()
            where TTableRowWrapper : ExcelTableTableRowWrapper<TOverview, TRow>, new()
             where TRow : ExcelNamedRowBase, new()
        {
            InitOverviews<TOverview, TRowDetailWrapper, TTableRowWrapper, ExcelTableRowNewWrapper<TOverview, TRow>, TRow>(tree, menuMainKey, menuDisplayName);
        }

        void InitBaseOverviews<TOverview, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
                    where TOverview : ExcelTableOverview
                    where TRow : ExcelNamedRowBase, new()
        {
            InitCommonOverviews<TOverview, ExcelTableRowDetailWrapper<TOverview, TRow>, ExcelTableTableRowWrapper<TOverview, TRow>, TRow>(tree, menuMainKey, menuDisplayName);
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
            InitOverviews<AssetPathTableOverview, AssetPathDetailWrapper, ExcelTableTableRowWrapper<AssetPathTableOverview, AssetPathTable.AssetPathRow>, AssetPathNewWrapper, AssetPathTable.AssetPathRow>(tree, "AssetPath", "资产路径");
            // InitCommonOverviews<AssetPathTableOverview, AssetPathDetailWrapper, ExcelTableTableRowWrapper<AssetPathTableOverview, AssetPathTable.AssetPathRow>, AssetPathTable.AssetPathRow>(tree, "AssetPath");
            InitCommonOverviews<StaticSceneTableOverview, StaticSceneDetailWrapper, ExcelTableTableRowWrapper<StaticSceneTableOverview, StaticSceneTable.StaticSceneRow>, StaticSceneTable.StaticSceneRow>(tree, "StaticScene", "静态场景");
            InitCommonOverviews<DynamicObjectTableOverview, DynamicObjectDetailWrapper, ExcelTableTableRowWrapper<DynamicObjectTableOverview, DynamicObjectTable.DynamicObjectRow>, DynamicObjectTable.DynamicObjectRow>(tree, "DynamicObject", "动态物体");
            InitCommonOverviews<GameSectionTableOverview, GameSectionDetailWrapper, ExcelTableTableRowWrapper<GameSectionTableOverview, GameSectionTable.GameSectionRow>, GameSectionTable.GameSectionRow>(tree, "GameSection", "游戏段落");

            InitCommonOverviews<TriggerEventTableOverview, TriggerDetailWrapper, ExcelTableTableRowWrapper<TriggerEventTableOverview, TriggerEventTable.TriggerEventRow>, TriggerEventTable.TriggerEventRow>(tree, "Trigger", "触发器");
            InitCommonOverviews<InstructionTableOverview, InstructionDetailWrapper, ExcelTableTableRowWrapper<InstructionTableOverview, InstructionTable.InstructionRow>, InstructionTable.InstructionRow>(tree, "Instruction", "指令");

            InitCommonOverviews<ConditionTableOverview, ConditionDetailWrapper, ExcelTableTableRowWrapper<ConditionTableOverview, ConditionTable.ConditionRow>, ConditionTable.ConditionRow>(tree, "Condition", "条件");

            InitCommonOverviews<CharacterTableOverview, CharacterDetailWrapper, ExcelTableTableRowWrapper<CharacterTableOverview, CharacterTable.CharacterRow>, CharacterTable.CharacterRow>(tree, "Character", "角色");
            InitCommonOverviews<HotspotTableOverview, HotspotDetailWrapper, ExcelTableTableRowWrapper<HotspotTableOverview, HotspotTable.HotspotRow>, HotspotTable.HotspotRow>(tree, "Hotspot", "热点区域");

            InitCommonOverviews<VariablesTableOverview, VariablesDetailWrapper, ExcelTableTableRowWrapper<VariablesTableOverview, VariablesTable.VariablesRow>, VariablesTable.VariablesRow>(tree, "Variables", "变量");



            return tree;
        }
    }

}