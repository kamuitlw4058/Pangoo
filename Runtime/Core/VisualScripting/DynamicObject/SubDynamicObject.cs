using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
using Pangoo.Common;

namespace Pangoo.Core.VisualScripting
{

    public class SubDynamicObject
    {

        [JsonMember("Path")]
        [LabelText("加载路径")]
        public string Path;

        [TableTitleGroup("动态物体Id")]
        [HideLabel]
        [TableColumnWidth(200, resizable: false)]
        [JsonMember("DynamicObjectId")]
        [ValueDropdown("OnDynamicObjectIdDropdown")]
        // [OnValueChanged("OnDynamicObjectIdChanged")]
        public int DynamicObjectId;



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

        IEnumerable OnDynamicObjectIdDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<DynamicObjectTableOverview>();
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