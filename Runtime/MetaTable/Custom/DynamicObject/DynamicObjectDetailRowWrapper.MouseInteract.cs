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
        [LabelText("鼠标交互文本")]
        [FoldoutGroup("鼠标交互", Order = 15)]
        [ShowInInspector]
        public string MouseInteractList
        {
            get
            {
                return UnityRow.Row.MouseInteractList;
            }
        }


        List<DynamicObjectMouseInteractInfo> m_MouseInteractInfos;

        [LabelText("鼠标交互列表")]
        [FoldoutGroup("鼠标交互")]
        [ShowInInspector]
        [OnValueChanged("OnMouseInteractChanged", includeChildren: true)]
        [HideReferenceObjectPicker]
        [ListDrawerSettings(ShowFoldout = true, CustomAddFunction = "OnMounseInteractAdd")]

        public List<DynamicObjectMouseInteractInfo> MouseInteractInfos
        {
            get
            {
                if (m_MouseInteractInfos == null)
                {
                    m_MouseInteractInfos = JsonMapper.ToObject<List<DynamicObjectMouseInteractInfo>>(UnityRow.Row.MouseInteractList);
                    if (m_MouseInteractInfos == null)
                    {
                        m_MouseInteractInfos = new List<DynamicObjectMouseInteractInfo>();
                    }

                    foreach (var sub in m_MouseInteractInfos)
                    {
                        sub.gameObject = Prefab;
                    }

                }
                return m_MouseInteractInfos;
            }
            set
            {
                m_MouseInteractInfos = value;
            }
        }


        public void OnMounseInteractAdd()
        {
            Debug.Log($"OnMounseInteractAdd");
            var obj = new DynamicObjectMouseInteractInfo();
            obj.Path = string.Empty;
            obj.gameObject = Prefab;
            m_MouseInteractInfos.Add(obj);

            UnityRow.Row.MouseInteractList = JsonMapper.ToJson(MouseInteractInfos);
            Save();
        }

        void OnMouseInteractChanged()
        {
            // Debug.Log($"OnSubDynamicObjectsChanged:{JsonMapper.ToJson(SubDynamicObjects)}");
            UnityRow.Row.MouseInteractList = JsonMapper.ToJson(MouseInteractInfos);
            Save();
        }



    }
}
#endif

