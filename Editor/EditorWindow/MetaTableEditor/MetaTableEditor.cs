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
    public class MetaTableEditor : OdinMenuEditorWindow, IMetaTableEditor
    {
        [MenuItem("Pangoo/MetaTable", false, 10)]
        private static void OpenWindow()
        {
            var window = GetWindow<MetaTableEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1600, 700);
            window.titleContent = new GUIContent("MetaTable");
            window.MenuWidth = 250;
        }

        private void OnSelectionChange()
        {
            // Debug.Log($"Selection:{}");
        }

        protected override object GetTarget()
        {
            Debug.Log($"Target:{Selection.activeObject}");
            return base.GetTarget();
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
                if (SirenixEditorGUI.ToolbarButton(SdfIconType.ReplyFill))
                {
                    Debug.Log($"SelectionStack.Count:{SelectionStack.Count}");
                    if (SelectionStack.Count > 0)
                    {
                        SelectionStack.Pop();
                    }

                    if (SelectionStack.Count > 0)
                    {

                        var last = SelectionStack.Peek();
                        Debug.Log($"SelectionStack.Count:{SelectionStack.Count} last:{last}");
                        if (last != m_OdinMenuTree.Selection.SelectedValue && last != null)
                        {
                            TrySelectMenuItemWithObject(last);
                        }
                    }

                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("刷新编辑器缓存")))
                {
                    GameSupportEditorUtility.Refresh();
                    SelectionStack.Clear();
                }

            }
            SirenixEditorGUI.EndHorizontalToolbar();
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
            overviewEditor.Editor = this;
            overviewEditor.InitWrappers();
            tree.Add(menuDisplayName, overviewEditor);

        }

        OdinMenuTree m_OdinMenuTree;

        Stack<object> SelectionStack = new Stack<object>();

        public void OnSeletionChecned(SelectionChangedType changedType)
        {
            switch (changedType)
            {
                case SelectionChangedType.ItemAdded:
                    if (SelectionStack.Count == 0)
                    {
                        SelectionStack.Push(m_OdinMenuTree.Selection.SelectedValue);
                        return;
                    }

                    var target = SelectionStack.Peek();
                    if (target != m_OdinMenuTree.Selection.SelectedValue)
                    {
                        Debug.Log($"Push:{m_OdinMenuTree.Selection.SelectedValue} SelectionStack.Count:{SelectionStack.Count}");
                        SelectionStack.Push(m_OdinMenuTree.Selection.SelectedValue);
                        return;
                    }

                    break;
            }
        }


        protected override OdinMenuTree BuildMenuTree()
        {
            if (m_OdinMenuTree == null)
            {
                DetailWrapperDict.Clear();
                RowWrapperDict.Clear();
                m_OdinMenuTree = new OdinMenuTree(false);

                m_OdinMenuTree.Config.DrawSearchToolbar = true;
                m_OdinMenuTree.Config.AutoScrollOnSelectionChanged = true;
                m_OdinMenuTree.Selection.SelectionChanged += OnSeletionChecned;


                var config = AssetDatabaseUtility.FindAssetFirst<GameMainConfig>();
                if (config != null)
                {
                    m_OdinMenuTree.Add("游戏配置", config);
                }

                InitOverviewWrapper<AssetGroupOverviewWrapper, AssetGroupOverview, AssetGroupDetailRowWrapper, AssetGroupRowWrapper, AssetGroupNewRowWrapper, UnityAssetGroupRow>(m_OdinMenuTree, null, "资源组");

                InitOverviewWrapper<AssetPathOverviewWrapper, Pangoo.MetaTable.AssetPathOverview, Pangoo.MetaTable.AssetPathDetailRowWrapper, Pangoo.MetaTable.AssetPathRowWrapper, Pangoo.MetaTable.AssetPathNewRowWrapper, UnityAssetPathRow>(m_OdinMenuTree, null, "资源路径");
                InitOverviewWrapper<CharacterOverviewWrapper, Pangoo.MetaTable.CharacterOverview, Pangoo.MetaTable.CharacterDetailRowWrapper, Pangoo.MetaTable.CharacterRowWrapper, Pangoo.MetaTable.CharacterNewRowWrapper, UnityCharacterRow>(m_OdinMenuTree, null, "角色");
                InitOverviewWrapper<GameSectionOverviewWrapper, Pangoo.MetaTable.GameSectionOverview, Pangoo.MetaTable.GameSectionDetailRowWrapper, Pangoo.MetaTable.GameSectionRowWrapper, Pangoo.MetaTable.GameSectionNewRowWrapper, UnityGameSectionRow>(m_OdinMenuTree, null, "游戏段落");

                InitOverviewWrapper<DynamicObjectOverviewWrapper, Pangoo.MetaTable.DynamicObjectOverview, Pangoo.MetaTable.DynamicObjectDetailRowWrapper, Pangoo.MetaTable.DynamicObjectRowWrapper, Pangoo.MetaTable.DynamicObjectNewRowWrapper, UnityDynamicObjectRow>(m_OdinMenuTree, null, "动态物体");
                InitOverviewWrapper<HotspotOverviewWrapper, Pangoo.MetaTable.HotspotOverview, Pangoo.MetaTable.HotspotDetailRowWrapper, Pangoo.MetaTable.HotspotRowWrapper, Pangoo.MetaTable.HotspotNewRowWrapper, UnityHotspotRow>(m_OdinMenuTree, null, "交互UI");

                InitOverviewWrapper<TriggerEventOverviewWrapper, Pangoo.MetaTable.TriggerEventOverview, Pangoo.MetaTable.TriggerEventDetailRowWrapper, Pangoo.MetaTable.TriggerEventRowWrapper, Pangoo.MetaTable.TriggerEventNewRowWrapper, UnityTriggerEventRow>(m_OdinMenuTree, null, "触发器");

                InitOverviewWrapper<ConditionOverviewWrapper, Pangoo.MetaTable.ConditionOverview, Pangoo.MetaTable.ConditionDetailRowWrapper, Pangoo.MetaTable.ConditionRowWrapper, Pangoo.MetaTable.ConditionNewRowWrapper, UnityConditionRow>(m_OdinMenuTree, null, "条件");
                InitOverviewWrapper<StaticSceneOverviewWrapper, Pangoo.MetaTable.StaticSceneOverview, Pangoo.MetaTable.StaticSceneDetailRowWrapper, Pangoo.MetaTable.StaticSceneRowWrapper, Pangoo.MetaTable.StaticSceneNewRowWrapper, UnityStaticSceneRow>(m_OdinMenuTree, null, "静态场景");
                InitOverviewWrapper<InstructionOverviewWrapper, Pangoo.MetaTable.InstructionOverview, Pangoo.MetaTable.InstructionDetailRowWrapper, Pangoo.MetaTable.InstructionRowWrapper, Pangoo.MetaTable.InstructionNewRowWrapper, UnityInstructionRow>(m_OdinMenuTree, null, "指令");
                InitOverviewWrapper<VariablesOverviewWrapper, Pangoo.MetaTable.VariablesOverview, Pangoo.MetaTable.VariablesDetailRowWrapper, Pangoo.MetaTable.VariablesRowWrapper, Pangoo.MetaTable.VariablesNewRowWrapper, UnityVariablesRow>(m_OdinMenuTree, null, "变量");
                InitOverviewWrapper<SoundOverviewWrapper, Pangoo.MetaTable.SoundOverview, Pangoo.MetaTable.SoundDetailRowWrapper, Pangoo.MetaTable.SoundRowWrapper, Pangoo.MetaTable.SoundNewRowWrapper, UnitySoundRow>(m_OdinMenuTree, null, "音频");
                InitOverviewWrapper<SimpleUIOverviewWrapper, Pangoo.MetaTable.SimpleUIOverview, Pangoo.MetaTable.SimpleUIDetailRowWrapper, Pangoo.MetaTable.SimpleUIRowWrapper, Pangoo.MetaTable.SimpleUINewRowWrapper, UnitySimpleUIRow>(m_OdinMenuTree, null, "UI");
                InitOverviewWrapper<DynamicObjectPreviewOverviewWrapper, Pangoo.MetaTable.DynamicObjectPreviewOverview, Pangoo.MetaTable.DynamicObjectPreviewDetailRowWrapper, Pangoo.MetaTable.DynamicObjectPreviewRowWrapper, Pangoo.MetaTable.DynamicObjectPreviewNewRowWrapper, UnityDynamicObjectPreviewRow>(m_OdinMenuTree, null, "预览");
                InitOverviewWrapper<ActorsLinesOverviewWrapper, Pangoo.MetaTable.ActorsLinesOverview, Pangoo.MetaTable.ActorsLinesDetailRowWrapper, Pangoo.MetaTable.ActorsLinesRowWrapper, Pangoo.MetaTable.ActorsLinesNewRowWrapper, UnityActorsLinesRow>(m_OdinMenuTree, null, "台词");
                InitOverviewWrapper<DialogueOverviewWrapper, Pangoo.MetaTable.DialogueOverview, Pangoo.MetaTable.DialogueDetailRowWrapper, Pangoo.MetaTable.DialogueRowWrapper, Pangoo.MetaTable.DialogueNewRowWrapper, UnityDialogueRow>(m_OdinMenuTree, null, "对话");
                InitOverviewWrapper<CasesOverviewWrapper, Pangoo.MetaTable.CasesOverview, Pangoo.MetaTable.CasesDetailRowWrapper, Pangoo.MetaTable.CasesRowWrapper, Pangoo.MetaTable.CasesNewRowWrapper, UnityCasesRow>(m_OdinMenuTree, null, "案件");
                InitOverviewWrapper<ClueOverviewWrapper, Pangoo.MetaTable.ClueOverview, Pangoo.MetaTable.ClueDetailRowWrapper, Pangoo.MetaTable.ClueRowWrapper, Pangoo.MetaTable.ClueNewRowWrapper, UnityClueRow>(m_OdinMenuTree, null, "线索");
            }
            return m_OdinMenuTree;
        }

        Dictionary<string, object> DetailWrapperDict = new Dictionary<string, object>();

        Dictionary<string, object> RowWrapperDict = new Dictionary<string, object>();


        public void SetDetailWrapper(string uuid, object obj)
        {
            if (DetailWrapperDict.ContainsKey(uuid))
            {
                DetailWrapperDict[uuid] = obj;
            }
            else
            {
                DetailWrapperDict.Add(uuid, obj);
            }
        }

        public object GetDetailWrapper(string uuid)
        {
            if (DetailWrapperDict.TryGetValue(uuid, out object ret))
            {
                return ret;
            }
            return null;
        }

        public void SetRowWrapper(string uuid, object obj)
        {
            if (RowWrapperDict.ContainsKey(uuid))
            {
                RowWrapperDict[uuid] = obj;
            }
            else
            {
                RowWrapperDict.Add(uuid, obj);
            }
        }

        public object GetRowWrapper(string uuid)
        {
            if (RowWrapperDict.TryGetValue(uuid, out object ret))
            {
                return ret;
            }
            return null;
        }
    }

}