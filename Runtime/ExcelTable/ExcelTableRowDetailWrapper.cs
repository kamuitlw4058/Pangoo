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
    public class ExcelTableRowDetailWrapper<TOverview, TRow> : ExcelTableOverviewRowWrapper<TOverview, TRow> where TOverview : ExcelTableOverview where TRow : ExcelNamedRowBase
    {
        [field: NonSerialized]
        public Action<int> OnRemove;

        [ReadOnly]
        public override int Id
        {
            get
            {
                return base.Id;
            }
        }


        [ShowInInspector]
        [PropertyOrder(-1)]
        public override string Name
        {
            get { return m_Row?.Name ?? null; }
            set
            {
                if (m_Row != null && m_Overview != null)
                {
                    m_Row.Name = value;
                    Save();
                }
            }
        }




    }
}

