using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
using Sirenix.OdinInspector.Editor;
#endif

namespace Pangoo
{
    public class ExcelTableRowNewWrapper<TOverview, TRow> : ExcelTableOverviewRowWrapper<TOverview, TRow> where TOverview : ExcelTableOverview where TRow : ExcelNamedRowBase, new()
    {

#if UNITY_EDITOR
        public override bool CanNameChange
        {
            get
            {
                return true;
            }
        }
        public override bool CanIdChange
        {
            get
            {
                return true;
            }
        }


        [ShowInInspector]
        [PropertyOrder(-4)]
        [ValueDropdown("OnOverviewDropDown")]
        public override TOverview Overview
        {
            get
            {
                if (m_Overview == null)
                {
                    var overviews = AssetDatabaseUtility.FindAsset<TOverview>();
                    if (overviews != null && overviews.Count() > 0)
                    {
                        m_Overview = overviews.ToList()[0];
                    }
                }

                return m_Overview;
            }
            set
            {
                m_Overview = value;
            }
        }



        public IEnumerable OnOverviewDropDown()
        {
            return GameSupportEditorUtility.GetExcelTableOverview<TOverview>();
        }

        public override int Id
        {
            get
            {
                if (m_Row == null)
                {
                    m_Row = new TRow();
                }
                return m_Row.Id;
            }
            set
            {
                if (m_Row == null)
                {
                    m_Row = new TRow();
                }
                m_Row.Id = value;
            }
        }

        protected override bool CheckExistsId()
        {
            return GameSupportEditorUtility.ExistsExcelTableOverviewId<TOverview>(Id);
        }


        public override string Name
        {
            get
            {
                if (m_Row == null)
                {
                    m_Row = new TRow();
                }
                return m_Row.Name;
            }
            set
            {
                if (m_Row == null)
                {
                    m_Row = new TRow();
                }
                m_Row.Name = value;
            }
        }


        protected override bool CheckExistsName()
        {
            return GameSupportEditorUtility.ExistsExcelTableOverviewName<TOverview>(Name);
        }

        [Button("新建")]
        public void NewRow()
        {
            Debug.Log($"m_Row:{m_Row.Id} id exists:{CheckExistsId()}");
        }


    }
#endif
}

