#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;

using UnityEditor;
using Sirenix.OdinInspector.Editor;


namespace Pangoo
{
    public class ExcelTableRowNewWrapper<TOverview, TRow> : ExcelTableOverviewRowWrapper<TOverview, TRow> where TOverview : ExcelTableOverview where TRow : ExcelNamedRowBase, new()
    {
        public bool ShowCreateButton { get; set; } = true;

        public Action<int> AfterCreate;

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

                return m_Row.Id;
            }
            set
            {

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

                return m_Row.Name;
            }
            set
            {

                m_Row.Name = value;
            }
        }


        protected override bool CheckExistsName()
        {
            return GameSupportEditorUtility.ExistsExcelTableOverviewName<TOverview>(Name);
        }

        [Button("新建")]
        [ShowIf("@this.ShowCreateButton")]
        public virtual void Create()
        {
            if (m_Row == null) return;

            if (CheckExistsId() || CheckExistsName())
            {
                Debug.Log($"Row:  id:{m_Row.Id}  exists:{CheckExistsId()} name:{m_Row.Name} exists:{CheckExistsName()}");
                return;
            }

            Overview.Table.AddNamedRow(m_Row);
            if (AfterCreate != null)
            {
                AfterCreate(Id);
            }
        }


    }
}

#endif