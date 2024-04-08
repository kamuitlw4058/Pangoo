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
        [LabelText("碰撞触发类型")]
        [FoldoutGroup("碰撞触发", Order = 14)]
        [ShowInInspector]
        public ColliderTriggerTypeEnum ColliderTriggerType
        {
            get
            {
                return UnityRow.Row.ColliderTriggerType.ToEnum<ColliderTriggerTypeEnum>();
            }
            set
            {
                UnityRow.Row.ColliderTriggerType = value.ToString();
                Save();
            }
        }


        List<PColliderRange> m_ColliderRanges;

        [ShowInInspector]
        [FoldoutGroup("碰撞触发", Order = 14)]
        [LabelText("碰撞范围")]
        [ShowIf("@this.ColliderTriggerType == ColliderTriggerTypeEnum.Manual")]
        [ListDrawerSettings(CustomAddFunction = "OnAddColliderRanges")]
        [OnValueChanged("OnColliderRangesChanged", includeChildren: true)]
        public List<PColliderRange> ColliderRanges
        {
            get
            {
                if (m_ColliderRanges == null)
                {
                    try
                    {
                        m_ColliderRanges = JsonMapper.ToObject<List<PColliderRange>>(UnityRow.Row.ColliderTriggerManualList);
                    }
                    catch
                    {

                    }

                    if (m_ColliderRanges == null)
                    {
                        m_ColliderRanges = new List<PColliderRange>();
                    }
                }

                return m_ColliderRanges;
            }
            set
            {
                var colliderRange = new PColliderRange();
                m_ColliderRanges.Add(colliderRange);

            }
        }

        void OnAddColliderRanges()
        {
            if (m_ColliderRanges == null)
            {
                m_ColliderRanges = new List<PColliderRange>();
            }

            var colliderRange = new PColliderRange();
            m_ColliderRanges.Add(colliderRange);
            UnityRow.Row.ColliderTriggerManualList = JsonMapper.ToJson(m_ColliderRanges);
            Save();
        }

        void OnColliderRangesChanged()
        {
            if (m_ColliderRanges == null)
            {
                m_ColliderRanges = new List<PColliderRange>();
            }
            UnityRow.Row.ColliderTriggerManualList = JsonMapper.ToJson(m_ColliderRanges);
            Save();

        }


        [LabelText("碰撞体触发停留超时")]
        [FoldoutGroup("碰撞触发")]
        [ShowInInspector]
        [InfoBox("停留超时事件只会在停留超时设置在0.1以上发生", InfoMessageType.Warning, "@ColliderTriggerStayTimeout < 0.1f")]
        public float ColliderTriggerStayTimeout
        {
            get
            {

                return UnityRow.Row.ColliderTriggerStayTimeout;
            }
            set
            {
                UnityRow.Row.ColliderTriggerStayTimeout = value;
                Save();
            }
        }

        [LabelText("碰撞体触发停留退出延迟")]
        [FoldoutGroup("碰撞触发")]
        [InfoBox("停留超时退出事件只会在停留退出延迟设置在0.1以上发生", InfoMessageType.Warning, "@ColliderTriggerStayExitDelay < 0.1f")]
        [ShowInInspector]
        public float ColliderTriggerStayExitDelay
        {
            get
            {

                return UnityRow.Row.ColliderTriggerStayExitDelay;
            }
            set
            {
                UnityRow.Row.ColliderTriggerStayExitDelay = value;
                Save();
            }
        }




        [LabelText("子物体触发文本")]
        [FoldoutGroup("碰撞触发/子物体", Order = 14)]
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
        [FoldoutGroup("碰撞触发/子物体")]
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

