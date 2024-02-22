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

    public enum DynamicObjectMouseInteractType
    {
        Extra,
        Base,
    }


    public class DynamicObjectMouseInteractInfo
    {
        [JsonMember("Path")]
        [LabelText("加载路径")]
        public string Path;


        [JsonMember("MouseInteractType")]
        [LabelText("鼠标交互类型")]
        public DynamicObjectMouseInteractType MouseInteractType;

        [JsonMember("HotSpotUuid")]
        [LabelText("鼠标交互HotSpotUuid")]
        [ValueDropdown("OnHotSpotUuidDropdown")]
        public string HotSpotUuid;

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


        IEnumerable OnHotSpotUuidDropdown()
        {
            return HotspotOverview.GetUuidDropdown();
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