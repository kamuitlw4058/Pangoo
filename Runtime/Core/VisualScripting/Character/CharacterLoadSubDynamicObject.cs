using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using Pangoo.Core.Services;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    [DisallowMultipleComponent]
    public class CharacterLoadSubDynamicObject : MonoBehaviour
    {
        public EntityCharacter Entity;
        public ICharacterRow Row;
        
        List<SubDynamicObject> m_SubDynamicObjectInfo;
        public List<string> LoadingDynamicObject = new List<string>();

        public Dictionary<string, DynamicObject> SubDynamicObjectDict = new Dictionary<string, DynamicObject>();
        
        private DynamicObjectService m_DynamicObjectService;
        
        public void Start()
        {
            m_DynamicObjectService=PangooEntry.Service.mainService.GetService<DynamicObjectService>();
            LoadSubDynamicObject();
        }

        public void LoadSubDynamicObject()
        {
            SubDynamicObjectDict.Clear();
            
            m_SubDynamicObjectInfo = JsonMapper.ToObject<List<SubDynamicObject>>(Row.SubDynamicObject);

            if (m_SubDynamicObjectInfo == null || m_SubDynamicObjectInfo.Count == 0)
            {
                return;
            }
            
            foreach (var subDo in m_SubDynamicObjectInfo)
            {
                if (subDo.DynamicObjectUuid.IsNullOrWhiteSpace())
                {
                    continue;
                }

                m_DynamicObjectService.ShowSubDynamicObject(subDo.DynamicObjectUuid, Entity.Id, subDo.Path, null);
                
                LoadingDynamicObject.Add(subDo.DynamicObjectUuid);
            }
        }
    }
}
