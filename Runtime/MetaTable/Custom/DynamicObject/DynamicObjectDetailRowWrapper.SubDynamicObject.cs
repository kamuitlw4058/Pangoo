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
        [LabelText("子动态物体")]
        [FoldoutGroup("子动态物体", Order = 13)]
        [ShowInInspector]
        public string SubDynamicObjecStr
        {
            get
            {
                return UnityRow.Row.SubDynamicObject;
            }
        }

        List<SubDynamicObject> m_SubDynamicObject;

        [LabelText("子动态物体")]
        [FoldoutGroup("子动态物体")]
        // [ValueDropdown("TriggerIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true, CustomAddFunction = "OnSubDynamicObjectsAdd")]
        [ShowInInspector]
        [PropertyOrder(15)]
        [OnValueChanged("OnSubDynamicObjectsChanged", includeChildren: true)]
        [HideReferenceObjectPicker]
        public List<SubDynamicObject> SubDynamicObjects
        {
            get
            {
                if (m_SubDynamicObject == null)
                {
                    m_SubDynamicObject = JsonMapper.ToObject<List<SubDynamicObject>>(UnityRow.Row.SubDynamicObject);
                    if (m_SubDynamicObject == null)
                    {
                        m_SubDynamicObject = new List<SubDynamicObject>();
                    }

                    foreach (var sub in m_SubDynamicObject)
                    {
                        sub.gameObject = Prefab;
                    }

                }
                return m_SubDynamicObject;
            }
            set
            {
                Debug.Log($"Set SubDynamicObjects");
                m_SubDynamicObject = value;
            }
        }

        public void OnSubDynamicObjectsAdd()
        {
            Debug.Log($"OnSubDynamicObjectsAdd");
            var obj = new SubDynamicObject();
            obj.DynamicObjectUuid = string.Empty;
            obj.Path = string.Empty;
            obj.gameObject = Prefab;
            m_SubDynamicObject.Add(obj);

            UnityRow.Row.SubDynamicObject = JsonMapper.ToJson(SubDynamicObjects);
            Save();
        }

        void OnSubDynamicObjectsChanged()
        {
            Debug.Log($"OnSubDynamicObjectsChanged:{JsonMapper.ToJson(SubDynamicObjects)}");
            UnityRow.Row.SubDynamicObject = JsonMapper.ToJson(SubDynamicObjects);
            Save();
        }


    }
}
#endif

