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
        public int State;

        public int ShowState = -1;

        Dictionary<int, SubDynamicObject> m_StateSubDynamicObjectInfo;


        public Dictionary<int, DynamicObject> StateSubDynamicObjectDict = new Dictionary<int, DynamicObject>();


        void DoAwakeStateSubDynamicObject()
        {
            StateSubDynamicObjectDict.Clear();

            State = GetVariable<int>(Main.DynamicObjectStateVariableUuid);

            m_StateSubDynamicObjectInfo = JsonMapper.ToObject<Dictionary<int, SubDynamicObject>>(Row.StateSubDynamicObject);
            if (m_StateSubDynamicObjectInfo == null || m_StateSubDynamicObjectInfo.Count == 0)
            {
                // OnStateSubDynamicObjectLoadFinish();
                return;
            }

            foreach (var kv in m_StateSubDynamicObjectInfo)
            {
                var subDo = kv.Value;
                if (subDo.DynamicObjectUuid.IsNullOrWhiteSpace())
                {
                    return;
                }

                Log($"加载状态子动态物体:{subDo.DynamicObjectUuid}");
                DynamicObjectService.ShowEntity(subDo.DynamicObjectUuid, Entity, subDo.Path, "DynamicObject", (o) =>
                {
                    var subEntity = o as EntityDynamicObject;
                    StateSubDynamicObjectDict.Add(kv.Key, subEntity.DynamicObj);
                    OnStateSubDynamicObjectLoadFinish(kv.Key, subDo, subEntity.DynamicObj);
                });
            }


        }

        void DoDisableStateSubDynamicObject()
        {
            StateSubDynamicObjectDict.Clear();
        }


        public void StateEnabled(DynamicObject stateDynamicObject, bool val)
        {
            if (val)
            {
                stateDynamicObject.ModelActive = true;
                stateDynamicObject.AllTriggerEnabled = true;
            }
            else
            {
                stateDynamicObject.ModelActive = false;
                stateDynamicObject.AllTriggerEnabled = false;
            }
        }


        void DoUpdateStateSubDynamicObject()
        {
            State = GetVariable<int>(Main.DynamicObjectStateVariableUuid);
            if (ShowState != State)
            {
                foreach (var kv in StateSubDynamicObjectDict)
                {
                    StateEnabled(kv.Value, kv.Key == State);
                }
            }
            ShowState = State;
        }



        void OnStateSubDynamicObjectLoadFinish(int state, SubDynamicObject subDynamicObject, DynamicObject dynamicObject)
        {
            StateEnabled(dynamicObject, state == State);
            Log($"OnStateSubDynamicObjectLoadFinish");
        }

    }
}