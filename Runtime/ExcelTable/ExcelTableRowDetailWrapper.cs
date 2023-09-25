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
    public class ExcelTableRowDetailWrapper<TOverview, TRow> where TOverview : ExcelTableOverview where TRow : ExcelNamedRowBase
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



        public void Init()
        {

        }

        [ShowInInspector]
        [TableColumnWidth(60, resizable: false)]
        [TableTitleGroup("命名空间")]
        [PropertyOrder(-3)]
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
        [PropertyOrder(-2)]
        public int Id
        {
            get { return m_Row?.Id ?? 0; }
        }

        [ShowInInspector]
        [PropertyOrder(-1)]
        public string Name
        {
            get { return m_Row?.Name ?? null; }
            set
            {
                if (m_Row != null && m_Overview != null)
                {
                    m_Row.Name = value;
#if UNITY_EDITOR
                    EditorUtility.SetDirty(m_Overview);
                    AssetDatabase.SaveAssets();
#endif

                }
            }
        }




    }
}

