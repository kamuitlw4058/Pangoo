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
        [LabelText("子物体触发文本")]
        [FoldoutGroup("子物体触发", Order = 14)]
        [ShowInInspector]
        public string SubObjectTriggerList
        {
            get
            {
                return UnityRow.Row.SubObjectTriggerList;
            }
        }

        List<DynamicObjectSubObjectTrigger> m_SubObjectTriggers;

        [LabelText("子物体触发")]
        [FoldoutGroup("子物体触发")]
        [ShowInInspector]
        [OnValueChanged("OnSubObjectTriggersChanged", includeChildren: true)]
        [HideReferenceObjectPicker]
        [ListDrawerSettings(ShowFoldout = true, CustomAddFunction = "OnSubObjectTriggersAdd")]

        public List<DynamicObjectSubObjectTrigger> SubObjectTriggers
        {
            get
            {
                if (m_SubObjectTriggers == null)
                {
                    m_SubObjectTriggers = JsonMapper.ToObject<List<DynamicObjectSubObjectTrigger>>(UnityRow.Row.SubObjectTriggerList);
                    if (m_SubObjectTriggers == null)
                    {
                        m_SubObjectTriggers = new List<DynamicObjectSubObjectTrigger>();
                    }

                    foreach (var sub in m_SubObjectTriggers)
                    {
                        sub.gameObject = Prefab;
                    }

                }
                return m_SubObjectTriggers;
            }
            set
            {
                m_SubObjectTriggers = value;
            }
        }

        public void OnSubObjectTriggersAdd()
        {
            Debug.Log($"OnSubObjectTriggersAdd");
            var obj = new DynamicObjectSubObjectTrigger();
            obj.Path = string.Empty;
            obj.gameObject = Prefab;
            m_SubObjectTriggers.Add(obj);

            UnityRow.Row.SubObjectTriggerList = JsonMapper.ToJson(SubObjectTriggers);
            Save();
        }

        void OnSubObjectTriggersChanged()
        {
            // Debug.Log($"OnSubDynamicObjectsChanged:{JsonMapper.ToJson(SubDynamicObjects)}");
            UnityRow.Row.SubObjectTriggerList = JsonMapper.ToJson(SubObjectTriggers);
            Save();
        }

    }
}
#endif

