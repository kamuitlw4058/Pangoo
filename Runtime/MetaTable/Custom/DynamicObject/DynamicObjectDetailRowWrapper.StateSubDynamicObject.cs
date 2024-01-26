#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
using Sirenix.OdinInspector;
using MetaTable;
using Pangoo.Common;
using Pangoo.Core.VisualScripting;

namespace Pangoo.MetaTable
{
    public partial class DynamicObjectDetailRowWrapper : MetaTableDetailRowWrapper<DynamicObjectOverview, UnityDynamicObjectRow>
    {

        [TabGroup("状态子动态物体", Order = 14)]
        [ValueDropdown("GetVariableDropdown")]
        [ShowInInspector]
        public string StateVariableUuid
        {
            get
            {
                if (UnityRow.Row.StateVariableUuid.IsNullOrWhiteSpace())
                {
                    UnityRow.Row.StateVariableUuid = ConstString.Default;
                    Save();
                }

                return UnityRow.Row.StateVariableUuid;
            }
            set
            {
                UnityRow.Row.StateVariableUuid = value;
                Save();
            }
        }


        Dictionary<int, SubDynamicObject> m_StateSubDynamicObject;

        [LabelText("状态子动态物体")]
        [TabGroup("状态子动态物体")]
        // [ValueDropdown("TriggerIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true, CustomAddFunction = "OnStateSubDynamicObjectsAdd")]
        [ShowInInspector]
        [PropertyOrder(15)]
        [OnValueChanged("OnStateSubDynamicObjectsChanged", includeChildren: true)]
        [HideReferenceObjectPicker]
        public Dictionary<int, SubDynamicObject> StateSubDynamicObjects
        {
            get
            {
                if (m_StateSubDynamicObject == null)
                {
                    try
                    {
                        m_StateSubDynamicObject = JsonMapper.ToObject<Dictionary<int, SubDynamicObject>>(UnityRow.Row.StateSubDynamicObject);
                    }
                    catch
                    {
                        m_StateSubDynamicObject = null;
                    }
                    if (m_StateSubDynamicObject == null)
                    {
                        m_StateSubDynamicObject = new Dictionary<int, SubDynamicObject>();
                    }


                }
                foreach (var sub in m_StateSubDynamicObject)
                {
                    if (sub.Value != null)
                    {
                        sub.Value.gameObject = Prefab;
                    }
                }
                return m_StateSubDynamicObject;
            }
            set
            {
                Debug.Log($"Set SubDynamicObjects");
                m_StateSubDynamicObject = value;
            }
        }


        void OnStateSubDynamicObjectsChanged()
        {
            foreach (var sub in m_StateSubDynamicObject)
            {
                if (sub.Value != null)
                {
                    sub.Value.gameObject = Prefab;
                }
            }
            Debug.Log($"OnSubDynamicObjectsChanged:{JsonMapper.ToJson(SubDynamicObjects)}");
            UnityRow.Row.StateSubDynamicObject = JsonMapper.ToJson(StateSubDynamicObjects);
            Save();
        }

        IEnumerable GetVariableDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Int.ToString(), defaultOptions: true);
        }

    }
}
#endif

