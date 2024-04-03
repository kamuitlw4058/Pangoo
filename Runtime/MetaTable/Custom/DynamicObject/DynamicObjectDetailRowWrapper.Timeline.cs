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
        [LabelText("Timeline配置字符串")]
        [FoldoutGroup("Timeline", Order = 16)]
        [ShowInInspector]
        public string TimelineHelperListString
        {
            get
            {
                return UnityRow.Row.TimelineHelperList;
            }
        }

        List<DynamicObjectTimelineHelperInfo> m_TimelineHelperInfo;

        [LabelText("Timeline配置")]
        [FoldoutGroup("Timeline")]
        [ShowInInspector]
        [OnValueChanged("OnTimelineHelperInfoChanged", includeChildren: true)]
        [HideReferenceObjectPicker]
        [ListDrawerSettings(DefaultExpandedState = true, CustomAddFunction = "OnTimelineHelperInfoAdd")]
        public List<DynamicObjectTimelineHelperInfo> TimelineHelperInfo
        {
            get
            {
                if (m_TimelineHelperInfo == null)
                {
                    try
                    {
                        m_TimelineHelperInfo = JsonMapper.ToObject<List<DynamicObjectTimelineHelperInfo>>(UnityRow.Row.TimelineHelperList);
                        foreach (var info in m_TimelineHelperInfo)
                        {
                            info.gameObject = Prefab;
                        }
                    }
                    catch
                    {

                    }

                    if (m_TimelineHelperInfo == null)
                    {
                        m_TimelineHelperInfo = new List<DynamicObjectTimelineHelperInfo>();
                    }
                }
                return m_TimelineHelperInfo;
            }
            set
            {

            }
        }

        public void OnTimelineHelperInfoAdd()
        {
            if (m_TimelineHelperInfo == null)
            {
                m_TimelineHelperInfo = new List<DynamicObjectTimelineHelperInfo>();
            }

            var obj = new DynamicObjectTimelineHelperInfo();
            obj.gameObject = Prefab;
            m_TimelineHelperInfo.Add(obj);

            UnityRow.Row.TimelineHelperList = JsonMapper.ToJson(m_TimelineHelperInfo);
            Save();
        }

        public void OnTimelineHelperInfoChanged()
        {
            UnityRow.Row.TimelineHelperList = JsonMapper.ToJson(m_TimelineHelperInfo);
            Save();
        }
    }
}
#endif

