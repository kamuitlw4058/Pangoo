using System.Collections.Generic;
using System.Linq;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;


namespace Pangoo.Editor
{
    public class OverviewEditorBase<TOverview, TTableWrapper, TRow> where TOverview : ExcelTableOverview where TTableWrapper : ExcelTableRowTableWrapper<TOverview, TRow>, new() where TRow : ExcelNamedRowBase
    {
        string m_Model;

        public string Model
        {
            get
            {
                return m_Model;
            }
            set
            {
                m_Model = value;
            }

        }

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

        private readonly List<TTableWrapper> m_AllWrappers = new List<TTableWrapper>();


        public List<TTableWrapper> Wrappers
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
                    var wrapper = new TTableWrapper();
                    wrapper.Overview = overview;
                    wrapper.Row = x as TRow;
                    wrapper.Window = m_Window;
                    wrapper.OnRemove += OnWrapperRemove;
                    return wrapper;
                }).ToList());
            }

        }

        TTableWrapper GetWrapperId(int id)
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
        public string GetMenuItemKey(string model, int id, string name)
        {
            return $"{model}-{id}-{name}";
        }

        private void OnWrapperRemove(int id)
        {
            var wrapper = GetWrapperId(id);
            if (wrapper != null)
            {
                m_AllWrappers.Remove(wrapper);
                var item = Window?.MenuTree.GetMenuItem(GetMenuItemKey(Model, wrapper.Id, wrapper.Name));
                if (item != null)
                {
                    Window?.MenuTree.MenuItems.Remove(item);
                }

            }
        }







    }
}
