using System.Collections.Generic;
using System.Linq;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;


namespace Pangoo.Editor
{
    public class OverviewEditorBase<TOverview, TRowDetailWrapper, TTableRowWrapper, TNewRowWrapper, TRow>
            where TOverview : ExcelTableOverview
            where TRowDetailWrapper : ExcelTableRowDetailWrapper<TOverview, TRow>, new()
            where TTableRowWrapper : ExcelTableTableRowWrapper<TOverview, TRow>, new()
            where TNewRowWrapper : ExcelTableRowNewWrapper<TOverview, TRow>, new()
            where TRow : ExcelNamedRowBase, new()
    {

        private static OdinEditorWindow m_CreateWindow;

        public string MenuKey
        { get; set; }

        List<TOverview> m_Overviews;

        public List<TOverview> Overviews
        {
            get
            {
                return m_Overviews;
            }
            set
            {
                m_Overviews = value;
            }
        }

        [TableList(IsReadOnly = true, AlwaysExpanded = true), ShowInInspector]

        private readonly List<TTableRowWrapper> m_AllWrappers = new List<TTableRowWrapper>();


        public List<TTableRowWrapper> Wrappers
        {
            get
            {
                return m_AllWrappers;
            }
        }

        OdinMenuEditorWindow m_Window;

        public OdinMenuEditorWindow Window
        {
            get
            {
                return m_Window;
            }
            set
            {
                m_Window = value;
            }
        }

        public void InitWrappers()
        {
            foreach (var overview in m_Overviews)
            {

                m_AllWrappers.AddRange(overview.Table.NamedBaseRows.Select(x =>
                {
                    var wrapper = new TTableRowWrapper();
                    wrapper.Overview = overview;
                    wrapper.Row = x as TRow;
                    wrapper.Window = m_Window;
                    var detailWrapper = new TRowDetailWrapper();
                    detailWrapper.Overview = overview;
                    detailWrapper.Row = x as TRow;
                    wrapper.DetailWrapper = detailWrapper;
                    wrapper.OnRemove += OnWrapperRemove;
                    return wrapper;
                }).ToList());
            }

        }

        TTableRowWrapper GetWrapperId(int id)
        {
            foreach (var wrapper in m_AllWrappers)
            {
                if (wrapper.Id == id)
                {
                    return wrapper;
                }
            }
            return null;
        }

        private void OnWrapperRemove(int id)
        {
            var wrapper = GetWrapperId(id);
            if (wrapper != null)
            {
                m_AllWrappers.Remove(wrapper);
                var item = Window?.MenuTree.GetMenuItem(GameEditorUtility.GetMenuItemKey(MenuKey, wrapper.Id, wrapper.Name));
                if (item != null)
                {
                    Window?.MenuTree.MenuItems.Remove(item);
                }

            }
        }

        [Button("新建行")]
        public void NewRow()
        {
            //                 PackageConfig config = GameSupportEditorUtility.GetPakcageConfigByOverviewRowId<GameSectionTableOverview>(m_Editor.Section);
            // var window = new AssetPathWrapper(config, Id, ConstExcelTable.DynamicObjectAssetTypeName, Name, ConstExcelTable.PrefabType, AfterCreateAsset);
            //                 m_CreateAssetPathWindow = OdinEditorWindow.InspectObject(window);
            var newType = new TNewRowWrapper();
            Debug.Log($"typeof:{newType.GetType()}");
            OdinEditorWindow.InspectObject(newType);
        }







    }
}
