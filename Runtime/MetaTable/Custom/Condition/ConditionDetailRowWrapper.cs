#if UNITY_EDITOR

using System;
using System.Collections;
using Sirenix.OdinInspector;

using MetaTable;
using Pangoo.Core.VisualScripting;
using Pangoo;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class ConditionDetailRowWrapper : MetaTableDetailRowWrapper<ConditionOverview, UnityConditionRow>
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

                return UnityRow.Row.ConditionType;
            }
            set
            {

                UnityRow.Row.ConditionType = value;
                UnityRow.Row.Params = "{}";
                Save();
                UpdateCondition();
            }
        }

        void UpdateCondition()
        {
            if (UnityRow.Row.ConditionType.IsNullOrWhiteSpace())
            {
                return;
            }
            m_ConditionInstance = ClassUtility.CreateInstance(UnityRow.Row.ConditionType) as Condition;
            m_ConditionInstance?.Load(UnityRow.Row.Params);
        }

        [ShowInInspector]
        [ReadOnly]
        public string Params
        {
            get
            {
                return UnityRow.Row?.Params;
            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.Params = value;
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
            return ClassUtility.GetTypeByCategoryAttr<Condition>();
        }

        [Button("保存参数")]
        [TableColumnWidth(80, resizable: false)]
        public void SaveParams()
        {
            Params = ConditionInstance.Save();
        }

        [Button("加载参数")]
        [TableColumnWidth(80, resizable: false)]
        public void LoadParams()
        {
            ConditionInstance.Load(UnityRow.Row.Params);
        }
    }
}
#endif

