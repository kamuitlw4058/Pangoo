using Sirenix.OdinInspector;
using UnityEngine;
using System;
using UnityEngine.Serialization;
using UnityEditor.Build;

#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
#endif

namespace Pangoo
{
    public class ExcelTableTableRowWrapper<TOverview, TRow> where TOverview : ExcelTableOverview where TRow : ExcelNamedRowBase
    {
        [field: NonSerialized]
        public Action<int> OnRemove;

        TOverview m_Overview;

        public TOverview Overview
        {
            get
            {
                return m_Overview;
            }
            set
            {
                m_Overview = value;
            }
        }

        TRow m_Row;

        public TRow Row
        {
            get
            {
                return m_Row;
            }
            set
            {
                m_Row = value;
            }
        }

        ExcelTableRowDetailWrapper<TOverview, TRow> m_DetailWrapper;


        public ExcelTableRowDetailWrapper<TOverview, TRow> DetailWrapper
        {
            get
            {
                return m_DetailWrapper;
            }
            set
            {
                m_DetailWrapper = value;
            }
        }



        public void Init()
        {

        }

        [ShowInInspector]
        [TableColumnWidth(60, resizable: false)]
        [TableTitleGroup("命名空间")]
        [HideLabel]
        public string Namespace
        {
            get
            {
                return m_Overview.Config.MainNamespace;
            }
        }

        [ShowInInspector]
        [TableColumnWidth(60, resizable: false)]
        public int Id
        {
            get { return m_Row?.Id ?? 0; }
        }

        [ShowInInspector]
        public string Name
        {
            get { return m_Row?.Name ?? null; }
        }

#if UNITY_EDITOR
        // public bool ShowEditor { get; set; }

        // public bool ShowRemove { get; set; }

        OdinMenuEditorWindow m_MenuWindow;

        public OdinMenuEditorWindow Window
        {
            get
            {
                return m_MenuWindow;
            }
            set
            {
                m_MenuWindow = value;
            }
        }




        [Button("编辑")]
        [TableColumnWidth(60, resizable: false)]
        // [ShowIf("@this.ShowEditor")]
        public void Editor()
        {
            m_MenuWindow?.TrySelectMenuItemWithObject(DetailWrapper);
        }


        // [ShowIf("@this.ShowRemove")]
        [Button("删除")]
        [TableColumnWidth(60, resizable: false)]
        public void Remove()
        {
            if (Id == 0) return;
            Overview?.Table?.RemoveId(Id);
            EditorUtility.SetDirty(m_Overview);
            AssetDatabase.SaveAssets();
            if (OnRemove != null)
            {
                OnRemove(Id);
            }

        }
#endif


    }
}

