using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using MetaTable.Editor;
using MetaTable;
using Pangoo.MetaTable;


namespace Pangoo.Editor
{
    public class MetaTableEditor : OdinMenuEditorWindow
    {
        [MenuItem("Pangoo/MetaTable", false, 10)]
        private static void OpenWindow()
        {
            var window = GetWindow<MetaTableEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1600, 700);
            window.titleContent = new GUIContent("MetaTable");
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

        void InitOverviews<TOverview, TRowDetailWrapper, TTableRowWrapper, TNewRowWrapper, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
            where TOverview : MetaTableOverview
            where TRowDetailWrapper : MetaTableDetailRowWrapper<TOverview, TRow>, new()
            where TTableRowWrapper : MetaTableRowWrapper<TOverview, TNewRowWrapper, TRow>, new()
            where TNewRowWrapper : MetaTableNewRowWrapper<TOverview, TRow>, new()
            where TRow : MetaTableUnityRow, new()
        {
            var overviews = AssetDatabaseUtility.FindAsset<TOverview>().ToList();
            var overviewEditor = new MetaTableOverviewWrapper<TOverview, TRowDetailWrapper, TTableRowWrapper, TNewRowWrapper, TRow>();
            overviewEditor.Overviews = overviews;
            overviewEditor.MenuWindow = this;
            // overviewEditor.MenuKey = menuMainKey;
            overviewEditor.MenuDisplayName = menuDisplayName;
            overviewEditor.Tree = tree;
            overviewEditor.InitWrappers();
            tree.Add(menuDisplayName, overviewEditor);

        }

        void InitOverviewWrapper<T, TOverview, TRowDetailWrapper, TTableRowWrapper, TNewRowWrapper, TRow>(OdinMenuTree tree, string menuMainKey, string menuDisplayName)
    where T : MetaTableOverviewWrapper<TOverview, TRowDetailWrapper, TTableRowWrapper, TNewRowWrapper, TRow>, new()
    where TOverview : MetaTableOverview
    where TRowDetailWrapper : MetaTableDetailRowWrapper<TOverview, TRow>, new()
    where TTableRowWrapper : MetaTableRowWrapper<TOverview, TNewRowWrapper, TRow>, new()
    where TNewRowWrapper : MetaTableNewRowWrapper<TOverview, TRow>, new()
    where TRow : MetaTableUnityRow, new()
        {
            var overviews = AssetDatabaseUtility.FindAsset<TOverview>().ToList();
            var overviewEditor = new T();
            overviewEditor.Overviews = overviews;
            overviewEditor.MenuWindow = this;
            // overviewEditor.MenuKey = menuMainKey;
            overviewEditor.MenuDisplayName = menuDisplayName;
            overviewEditor.Tree = tree;
            overviewEditor.InitWrappers();
            tree.Add(menuDisplayName, overviewEditor);

        }


        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.AutoScrollOnSelectionChanged = false;

            var config = AssetDatabaseUtility.FindAssetFirst<GameMainConfig>();
            if (config != null)
            {
                tree.Add("游戏配置", config);
            }


            InitOverviewWrapper<AssetGroupOverviewWrapper, AssetGroupOverview, AssetGroupDetailRowWrapper, AssetGroupRowWrapper, AssetGroupNewRowWrapper, UnityAssetGroupRow>(tree, null, "资源组");

            InitOverviewWrapper<AssetPathOverviewWrapper, Pangoo.MetaTable.AssetPathOverview, Pangoo.MetaTable.AssetPathDetailRowWrapper, Pangoo.MetaTable.AssetPathRowWrapper, Pangoo.MetaTable.AssetPathNewRowWrapper, UnityAssetPathRow>(tree, null, "资源路径");
            InitOverviewWrapper<CharacterOverviewWrapper, Pangoo.MetaTable.CharacterOverview, Pangoo.MetaTable.CharacterDetailRowWrapper, Pangoo.MetaTable.CharacterRowWrapper, Pangoo.MetaTable.CharacterNewRowWrapper, UnityCharacterRow>(tree, null, "角色");
            InitOverviewWrapper<GameSectionOverviewWrapper, Pangoo.MetaTable.GameSectionOverview, Pangoo.MetaTable.GameSectionDetailRowWrapper, Pangoo.MetaTable.GameSectionRowWrapper, Pangoo.MetaTable.GameSectionNewRowWrapper, UnityGameSectionRow>(tree, null, "游戏段落");

            InitOverviewWrapper<DynamicObjectOverviewWrapper, Pangoo.MetaTable.DynamicObjectOverview, Pangoo.MetaTable.DynamicObjectDetailRowWrapper, Pangoo.MetaTable.DynamicObjectRowWrapper, Pangoo.MetaTable.DynamicObjectNewRowWrapper, UnityDynamicObjectRow>(tree, null, "动态物体");
            InitOverviewWrapper<HotspotOverviewWrapper, Pangoo.MetaTable.HotspotOverview, Pangoo.MetaTable.HotspotDetailRowWrapper, Pangoo.MetaTable.HotspotRowWrapper, Pangoo.MetaTable.HotspotNewRowWrapper, UnityHotspotRow>(tree, null, "交互UI");

            InitOverviewWrapper<TriggerEventOverviewWrapper, Pangoo.MetaTable.TriggerEventOverview, Pangoo.MetaTable.TriggerEventDetailRowWrapper, Pangoo.MetaTable.TriggerEventRowWrapper, Pangoo.MetaTable.TriggerEventNewRowWrapper, UnityTriggerEventRow>(tree, null, "触发器");

            InitOverviewWrapper<ConditionOverviewWrapper, Pangoo.MetaTable.ConditionOverview, Pangoo.MetaTable.ConditionDetailRowWrapper, Pangoo.MetaTable.ConditionRowWrapper, Pangoo.MetaTable.ConditionNewRowWrapper, UnityConditionRow>(tree, null, "条件");
            InitOverviewWrapper<StaticSceneOverviewWrapper, Pangoo.MetaTable.StaticSceneOverview, Pangoo.MetaTable.StaticSceneDetailRowWrapper, Pangoo.MetaTable.StaticSceneRowWrapper, Pangoo.MetaTable.StaticSceneNewRowWrapper, UnityStaticSceneRow>(tree, null, "静态场景");
            InitOverviewWrapper<InstructionOverviewWrapper, Pangoo.MetaTable.InstructionOverview, Pangoo.MetaTable.InstructionDetailRowWrapper, Pangoo.MetaTable.InstructionRowWrapper, Pangoo.MetaTable.InstructionNewRowWrapper, UnityInstructionRow>(tree, null, "指令");
            InitOverviewWrapper<VariablesOverviewWrapper, Pangoo.MetaTable.VariablesOverview, Pangoo.MetaTable.VariablesDetailRowWrapper, Pangoo.MetaTable.VariablesRowWrapper, Pangoo.MetaTable.VariablesNewRowWrapper, UnityVariablesRow>(tree, null, "变量");
            InitOverviewWrapper<SoundOverviewWrapper, Pangoo.MetaTable.SoundOverview, Pangoo.MetaTable.SoundDetailRowWrapper, Pangoo.MetaTable.SoundRowWrapper, Pangoo.MetaTable.SoundNewRowWrapper, UnitySoundRow>(tree, null, "音频");
            InitOverviewWrapper<SimpleUIOverviewWrapper, Pangoo.MetaTable.SimpleUIOverview, Pangoo.MetaTable.SimpleUIDetailRowWrapper, Pangoo.MetaTable.SimpleUIRowWrapper, Pangoo.MetaTable.SimpleUINewRowWrapper, UnitySimpleUIRow>(tree, null, "UI");
            InitOverviewWrapper<DynamicObjectPreviewOverviewWrapper, Pangoo.MetaTable.DynamicObjectPreviewOverview, Pangoo.MetaTable.DynamicObjectPreviewDetailRowWrapper, Pangoo.MetaTable.DynamicObjectPreviewRowWrapper, Pangoo.MetaTable.DynamicObjectPreviewNewRowWrapper, UnityDynamicObjectPreviewRow>(tree, null, "预览");
            InitOverviewWrapper<ActorsLinesOverviewWrapper, Pangoo.MetaTable.ActorsLinesOverview, Pangoo.MetaTable.ActorsLinesDetailRowWrapper, Pangoo.MetaTable.ActorsLinesRowWrapper, Pangoo.MetaTable.ActorsLinesNewRowWrapper, UnityActorsLinesRow>(tree, null, "台词");
            InitOverviewWrapper<DialogueOverviewWrapper, Pangoo.MetaTable.DialogueOverview, Pangoo.MetaTable.DialogueDetailRowWrapper, Pangoo.MetaTable.DialogueRowWrapper, Pangoo.MetaTable.DialogueNewRowWrapper, UnityDialogueRow>(tree, null, "对话");

            return tree;
        }
    }

}