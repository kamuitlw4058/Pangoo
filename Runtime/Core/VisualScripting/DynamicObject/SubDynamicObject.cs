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

    public class SubDynamicObject
    {

        [JsonMember("Path")]
        [LabelText("加载路径")]
        public string Path;

        [TableTitleGroup("动态物体Uuid")]
        [HideLabel]
        [TableColumnWidth(200, resizable: false)]
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectUuidDropdown")]
        // [OnValueChanged("OnDynamicObjectIdChanged")]
        public string DynamicObjectUuid;



#if UNITY_EDITOR
        [JsonNoMember]
        [HideInInspector]
        public Dictionary<GameObject, string> GoPathDict = new Dictionary<GameObject, string>();

        [ValueDropdown("OnTargetDropdown")]
        [OnValueChanged("OnTargetChanged")]
        [JsonNoMember]
        [LabelText("选择预制体路径")]
        public GameObject Target;

        [JsonNoMember]
        [HideInInspector]
        public GameObject gameObject;

        IEnumerable OnDynamicObjectUuidDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }


        IEnumerable OnTargetDropdown()
        {
            return GameSupportEditorUtility.RefPrefabDropdown(gameObject, GoPathDict);
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