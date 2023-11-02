#if UNITY_EDITOR
using System.Collections.Generic;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using System;

using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;
using LitJson;



namespace Pangoo
{
    public partial class DynamicObjectDetailWrapper
    {
        [LabelText("子动态物体")]
        [TabGroup("子动态物体", Order = 13)]
        [PropertyOrder(14)]
        [ShowInInspector]
        public string SubDynamicObjecStr
        {
            get
            {
                return Row.SubDynamicObject;
            }
        }

        List<SubDynamicObject> m_SubDynamicObject;

        [LabelText("子动态物体")]
        [TabGroup("子动态物体")]
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
                    m_SubDynamicObject = JsonMapper.ToObject<List<SubDynamicObject>>(Row.SubDynamicObject);
                    if (m_SubDynamicObject == null)
                    {
                        m_SubDynamicObject = new List<SubDynamicObject>();
                    }

                    foreach (var sub in m_SubDynamicObject)
                    {
                        sub.gameObject = AssetPrefab;
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
            obj.DynamicObjectId = 0;
            obj.Path = string.Empty;
            obj.gameObject = AssetPrefab;
            m_SubDynamicObject.Add(obj);

            Row.SubDynamicObject = JsonMapper.ToJson(SubDynamicObjects);
            Save();
        }

        void OnSubDynamicObjectsChanged()
        {
            Debug.Log($"OnSubDynamicObjectsChanged:{JsonMapper.ToJson(SubDynamicObjects)}");
            Row.SubDynamicObject = JsonMapper.ToJson(SubDynamicObjects);
            Save();
        }
    }
}
#endif