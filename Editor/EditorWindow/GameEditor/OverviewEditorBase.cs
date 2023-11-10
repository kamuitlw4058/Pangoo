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
            where TNewRowWrapper : ExcelTableRowNewWrapper<TOverview, TRow>, new()
            where TTableRowWrapper : ExcelTableTableRowWrapper<TOverview, TNewRowWrapper, TRow>, new()
            where TRow : ExcelNamedRowBase, new()
    {
        public OdinMenuTree Tree { get; set; }


        private static OdinEditorWindow m_CreateWindow;


        public string MenuDisplayName { get; set; }

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


        private Dictionary<string, OdinMenuItem> MenuItemDict = new Dictionary<string, OdinMenuItem>();


        public void InitWrappers()
        {
            foreach (var kv in MenuItemDict)
            {
                Tree.MenuItems.Remove(kv.Value);
            }
            MenuItemDict.Clear();
            m_AllWrappers.Clear();
            foreach (var overview in m_Overviews)
            {

                m_AllWrappers.AddRange(overview.Table.NamedBaseRows.Select(x =>
                {
                    var detailWrapper = new TRowDetailWrapper();
                    detailWrapper.Overview = overview;
                    detailWrapper.Row = x as TRow;

                    var wrapper = new TTableRowWrapper();
                    wrapper.Overview = overview;
                    wrapper.Row = x as TRow;
                    wrapper.Window = m_Window;
                    wrapper.DetailWrapper = detailWrapper;
                    wrapper.OnRemove += OnWrapperRemove;
                    return wrapper;
                }).ToList());
            }

            foreach (var wrapper in m_AllWrappers)
            {
                var itemMenuKey = GameEditorUtility.GetMenuItemKey(MenuKey, wrapper.Id, wrapper.Name);
                var customMenuItem = new OdinMenuItem(Tree, itemMenuKey, wrapper.DetailWrapper);
                MenuItemDict.Add(itemMenuKey, customMenuItem);
                Tree.AddMenuItemAtPath(MenuDisplayName, customMenuItem);
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
                var menuItemKey = GameEditorUtility.GetMenuItemKey(MenuKey, wrapper.Id, wrapper.Name);
                OdinMenuItem item;
                if (MenuItemDict.TryGetValue(menuItemKey, out item))
                {
                    var menuItem = Tree.GetMenuItem(MenuDisplayName);
                    menuItem.ChildMenuItems.Remove(item);
                    MenuItemDict.Remove(menuItemKey);
                }


            }
        }

        [Button("新建行")]
        public void NewRow()
        {
            var newType = new TNewRowWrapper();
            Debug.Log($"typeof:{newType.GetType()}");
            m_CreateWindow = OdinEditorWindow.InspectObject(newType);
            newType.AfterCreate = OnAfterCreate;
        }

        void OnAfterCreate(int id)
        {
            m_CreateWindow?.Close();
            InitWrappers();

        }


    }
}
