#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System;


using System.IO;
using System.Text;
using UnityEditor;
using Sirenix.OdinInspector.Editor;

namespace Pangoo
{
    public class ExcelTableTableRowWrapper<TOverview, TRow> : ExcelTableOverviewRowWrapper<TOverview, TRow> where TOverview : ExcelTableOverview where TRow : ExcelNamedRowBase
    {
        [field: NonSerialized]
        public Action<int> OnRemove;


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

        [ReadOnly]
        public override int Id
        {
            get
            {
                return base.Id;
            }
        }


        [ReadOnly]
        public override string Name
        {
            get
            {
                return base.Name;
            }
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

#endif
