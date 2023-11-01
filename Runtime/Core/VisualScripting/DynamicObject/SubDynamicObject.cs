using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;


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


        public void AddPrefabValueDropdownList(ValueDropdownList<GameObject> ret, Transform trans, string prefix)
        {
            foreach (var child in trans.Children())
            {
                var path = $"{prefix}/{child.name}";
                if (prefix == string.Empty)
                {
                    path = child.name;
                }
                ret.Add(path, child.gameObject);
                GoPathDict.Add(child.gameObject, path);
                AddPrefabValueDropdownList(ret, child, path);
            }
        }


        IEnumerable OnTargetDropdown()
        {
            var ValueDropdown = new ValueDropdownList<GameObject>();
            ValueDropdown.Add(ConstString.Self, gameObject);
            GoPathDict.Clear();
            if (gameObject != null)
            {
                AddPrefabValueDropdownList(ValueDropdown, gameObject.transform, string.Empty);
            }
            return ValueDropdown;
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