using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using LitJson;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        List<SubDynamicObject> m_SubDynamicObjectInfo;

        public List<string> LoadingDynamicObject = new List<string>();

        public Dictionary<string, DynamicObject> SubDynamicObjectDict = new Dictionary<string, DynamicObject>();


        void DoAwakeSubDynamicObject()
        {
            SubDynamicObjectDict.Clear();

            m_SubDynamicObjectInfo = JsonMapper.ToObject<List<SubDynamicObject>>(Row.SubDynamicObject);
            if (m_SubDynamicObjectInfo == null || m_SubDynamicObjectInfo.Count == 0)
            {
                OnSubDynamicObjectLoadFinish();
                return;
            }

            foreach (var subDo in m_SubDynamicObjectInfo)
            {
                if (subDo.DynamicObjectUuid.IsNullOrWhiteSpace())
                {
                    continue;
                }
                Debug.Log($"加载子动态物体,{Row.Name} :{subDo.DynamicObjectUuid}");
                DynamicObjectService.ShowSubDynamicObject(subDo.DynamicObjectUuid, Entity.Id, subDo.Path, (o) =>
                {
                    if (LoadingDynamicObject.Contains(subDo.DynamicObjectUuid))
                    {
                        LoadingDynamicObject.Remove(subDo.DynamicObjectUuid);
                    }
                    SubDynamicObjectDict.Add(subDo.DynamicObjectUuid, o.DynamicObj);
                    if (LoadingDynamicObject.Count == 0)
                    {
                        OnSubDynamicObjectLoadFinish();
                    }
                });

                LoadingDynamicObject.Add(subDo.DynamicObjectUuid);
            }

        }


        void OnSubDynamicObjectLoadFinish()
        {
            Debug.Log($"OnSubDynamicObjectLoadFinish:{Row.Id}");
        }

    }
}