using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
using Pangoo.Common;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    public enum SubObjectTriggerEventType
    {
        BaseTrigger,
        ExtraTrigger,
        Both,
    }


    public enum ColliderTriggerTypeEnum
    {
        [LabelText("预制体设置")]
        Prefab,
        [LabelText("手动设置")]
        Manual

    }

    public class DynamicObjectSubObjectTrigger
    {
        [JsonMember("Path")]
        [LabelText("加载路径")]
        public string Path;


        [JsonMember("TriggerType")]
        [LabelText("触发事件类型")]
        public SubObjectTriggerEventType TriggerEventType;

        [JsonMember("ColliderTriggerType")]
        [LabelText("碰撞触发类型")]
        public ColliderTriggerTypeEnum ColliderTriggerType;

        [JsonMember("ColliderTriggerPose")]
        [LabelText("手动碰撞触发Pose")]
        [ShowIf("@this.ColliderTriggerType == ColliderTriggerTypeEnum.Manual")]
        public List<PColliderRange> ColliderTriggerPose = new List<PColliderRange>();



#if UNITY_EDITOR
        [JsonNoMember]
        [HideInInspector]
        public Dictionary<GameObject, string> GoPathDict = new Dictionary<GameObject, string>();

        GameObject m_Target;

        [ValueDropdown("OnTargetDropdown")]
        [OnValueChanged("OnTargetChanged")]
        [JsonNoMember]
        [LabelText("选择预制体路径")]
        [ShowInInspector]
        public GameObject Target
        {
            get
            {
                if (m_Target == null)
                {
                    GameSupportEditorUtility.RefPrefabDropdown(gameObject, GoPathDict);
                    foreach (var kv in GoPathDict)
                    {
                        if (kv.Value == Path)
                        {
                            m_Target = kv.Key;
                            break;
                        }
                    }
                }
                return m_Target;
            }
            set { m_Target = value; }
        }

        [JsonNoMember]
        [HideInInspector]
        public GameObject gameObject;

        IEnumerable OnDynamicObjectUuidDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }


        IEnumerable OnTargetDropdown()
        {
            return GameSupportEditorUtility.RefPrefabDropdown(gameObject, GoPathDict, hasSelf: false);
        }

        void OnTargetChanged()
        {

            if (GoPathDict.TryGetValue(Target, out string goPath))
            {
                Path = goPath;
            }
            else
            {
                Path = ConstString.Self;
            }
        }
#endif
    }

}