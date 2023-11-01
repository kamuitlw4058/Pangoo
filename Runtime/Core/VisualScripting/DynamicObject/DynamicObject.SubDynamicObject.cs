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

        public List<int> LoadingDynamicObject = new List<int>();

        public Dictionary<int, DynamicObject> SubDynamicObjectDict = new Dictionary<int, DynamicObject>();



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
                if (subDo.DynamicObjectId == 0)
                {
                    continue;
                }

                DynamicObjectService.ShowSubDynamicObject(subDo.DynamicObjectId, Entity.Id, subDo.Path, (o) =>
                {
                    if (LoadingDynamicObject.Contains(subDo.DynamicObjectId))
                    {
                        LoadingDynamicObject.Remove(subDo.DynamicObjectId);
                    }
                    SubDynamicObjectDict.Add(subDo.DynamicObjectId, o.DynamicObj);
                    if (LoadingDynamicObject.Count == 0)
                    {
                        OnSubDynamicObjectLoadFinish();
                    }
                });

                LoadingDynamicObject.Add(subDo.DynamicObjectId);
            }

        }

        void OnSubDynamicObjectLoadFinish()
        {
            Debug.Log($"OnSubDynamicObjectLoadFinish:{Row.Id}");
        }

    }
}