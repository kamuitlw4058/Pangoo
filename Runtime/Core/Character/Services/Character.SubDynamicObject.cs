using System.Collections.Generic;
using LitJson;
using Pangoo.Core.Services;
using Pangoo.Core.VisualScripting;
using UnityGameFramework.Runtime;

namespace Pangoo.Core.Characters
{
    public partial class Character
    {
        List<SubDynamicObject> m_SubDynamicObjectInfo;

        public List<string> LoadingDynamicObject = new List<string>();

        public Dictionary<string, DynamicObject> SubDynamicObjectDict = new Dictionary<string, DynamicObject>();

        DynamicObjectService m_DynamicObjectService;
        public DynamicObjectService DynamicObjectService
        {
            get
            {
                if (m_DynamicObjectService == null)
                {
                    m_DynamicObjectService = Main?.GetService<DynamicObjectService>();
                }
                return m_DynamicObjectService;
            }
        }
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

                Log($"加载子动态物体:{subDo.DynamicObjectUuid}");
                DynamicObjectService.ShowEntity(subDo.DynamicObjectUuid, Entity, subDo.Path, "Character", (o) =>
                {
                    var entity = o as EntityDynamicObject;
                    if (LoadingDynamicObject.Contains(subDo.DynamicObjectUuid))
                    {
                        LoadingDynamicObject.Remove(subDo.DynamicObjectUuid);
                    }
                    SubDynamicObjectDict.Add(subDo.DynamicObjectUuid, entity.DynamicObj);
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
            Log($"OnSubDynamicObjectLoadFinish");
        }
    }
}