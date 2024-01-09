using System.Collections;
using System.Collections.Generic;
using LitJson;
using MetaTable;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.MetaTable
{
    public partial class CharacterDetailRowWrapper : MetaTableDetailRowWrapper<CharacterOverview, UnityCharacterRow>
    {
        [LabelText("子动态物体")]
        [TabGroup("子动态物体", Order = 13)]
        [PropertyOrder(14)]
        [ShowInInspector]
        public string SubDynamicObjecStr
        {
            get
            {
                return UnityRow.Row.SubDynamicObject;
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
                    m_SubDynamicObject = JsonMapper.ToObject<List<SubDynamicObject>>(UnityRow.Row.SubDynamicObject);
                    if (m_SubDynamicObject == null)
                    {
                        m_SubDynamicObject = new List<SubDynamicObject>();
                    }

                    foreach (var sub in m_SubDynamicObject)
                    {
                        sub.gameObject = Prefab;
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
            obj.DynamicObjectUuid = string.Empty;
            obj.Path = string.Empty;
            obj.gameObject = Prefab;
            m_SubDynamicObject.Add(obj);

            UnityRow.Row.SubDynamicObject = JsonMapper.ToJson(SubDynamicObjects);
            Save();
        }

        void OnSubDynamicObjectsChanged()
        {
            Debug.Log($"OnSubDynamicObjectsChanged:{JsonMapper.ToJson(SubDynamicObjects)}");
            UnityRow.Row.SubDynamicObject = JsonMapper.ToJson(SubDynamicObjects);
            Save();
        }


    }
}
