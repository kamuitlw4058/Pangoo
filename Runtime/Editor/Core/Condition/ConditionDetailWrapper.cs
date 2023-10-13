#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;
using GameFramework;

namespace Pangoo
{
    public class ConditionDetailWrapper : ExcelTableRowDetailWrapper<ConditionTableOverview, ConditionTable.ConditionRow>
    {

        [ShowInInspector]
        [ValueDropdown("GetConditionType")]
        public string ConditionType
        {
            get
            {
                if (m_ConditionInstance == null)
                {
                    UpdateCondition();
                }

                return Row?.ConditionType;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.ConditionType = value;
                    Row.Params = "{}";
                    Save();
                    UpdateCondition();
                }
            }
        }

        void UpdateCondition()
        {
            if (Row.ConditionType.IsNullOrWhiteSpace())
            {
                return;
            }

            var conditionType = Utility.Assembly.GetType(Row.ConditionType);
            if (conditionType == null)
            {
                return;
            }

            m_ConditionInstance = Activator.CreateInstance(conditionType) as Condition;
            m_ConditionInstance.LoadParams(Row.Params);
        }

        [ShowInInspector]
        [ReadOnly]
        public string Params
        {
            get
            {
                return Row?.Params;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.Params = value;
                    Save();
                }
            }
        }
        Condition m_ConditionInstance;

        [ShowInInspector]
        [HideLabel]
        [HideReferenceObjectPicker]
        public Condition ConditionInstance
        {
            get
            {
                return m_ConditionInstance;
            }
            set
            {
                m_ConditionInstance = value;
            }
        }



        public IEnumerable GetConditionType()
        {
            return GameSupportEditorUtility.GetSubTypeWithCategory<Condition>();
        }

        [Button("保存参数")]
        [TableColumnWidth(80, resizable: false)]
        public void SaveParams()
        {
            Params = ConditionInstance.ParamsString();
        }

        [Button("加载参数")]
        [TableColumnWidth(80, resizable: false)]
        public void LoadParams()
        {
            ConditionInstance.LoadParams(Row.Params);
        }

    }


}
#endif