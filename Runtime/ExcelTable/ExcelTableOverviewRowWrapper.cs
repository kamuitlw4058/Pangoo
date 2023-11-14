#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using UnityEngine.Serialization;


using System.IO;
using System.Text;
using UnityEditor;
using Sirenix.OdinInspector.Editor;


namespace Pangoo
{
    public class ExcelTableOverviewRowWrapper<TOverview, TRow> where TOverview : ExcelTableOverview where TRow : ExcelNamedRowBase, new()
    {
        public OdinEditorWindow Window { get; set; }
        public bool OutsideNeedRefresh { get; set; }
        public virtual bool CanNameChange
        {
            get
            {
                return false;
            }
        }

        public virtual bool CanIdChange
        {
            get
            {
                return false;
            }

        }


        protected TOverview m_Overview;

        public virtual TOverview Overview
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

        protected TRow m_Row = new TRow();

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

        public TRow Clone()
        {
            return CopyUtility.Clone<TRow>(Row);
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
                return m_Overview?.Config?.MainNamespace;
            }
        }

        [ShowInInspector]
        [TableColumnWidth(60, resizable: false)]
        [PropertyOrder(-2)]
        [DelayedProperty]
        [EnableIf("CanIdChange")]
        [InfoBox("Id 已经存在", InfoMessageType.Error, "CheckExistsId")]
        public virtual int Id
        {
            get { return m_Row?.Id ?? 0; }
            set
            {
                if (m_Row != null && m_Overview != null)
                {
                    m_Row.Id = value;
                }
            }
        }

        protected virtual bool CheckExistsId()
        {
            return false;
        }

        [ShowInInspector]
        [PropertyOrder(-1)]
        [EnableIf("CanNameChange")]
        [DelayedProperty]
        [InfoBox("已经有对应的名字", InfoMessageType.Warning, "CheckExistsName")]
        public virtual string Name
        {
            get { return m_Row?.Name ?? null; }
            set
            {
                if (m_Row != null && m_Overview != null)
                {
                    m_Row.Name = value;
                }
            }
        }

        protected virtual bool CheckExistsName()
        {
            return false;
        }



        public virtual void Save()
        {
            EditorUtility.SetDirty(m_Overview);
            AssetDatabase.SaveAssets();

            OutsideNeedRefresh = true;
        }
    }
}

#endif