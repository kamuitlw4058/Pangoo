using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        int m_EnterTriggerCount;

        [ShowInInspector]
        public int EnterTriggerCount
        {
            get
            {
                return m_EnterTriggerCount;
            }
            set
            {
                m_EnterTriggerCount = value;
                m_EnterTriggerCount = Mathf.Max(0, m_EnterTriggerCount);

                if (m_Tracker != null)
                {
                    if (m_EnterTriggerCount > 0)
                    {
                        m_Tracker.InteractTriggerEnter = true;
                    }
                    else
                    {
                        m_Tracker.InteractTriggerEnter = false;
                    }

                }
            }
        }

        List<DynamicObjectSubObjectTrigger> m_SubObjectTriggerInfo;


        void DoAwakeSubObjectTrigger()
        {
            if (Row.SubObjectTriggerList.IsNullOrWhiteSpace()) return;
            try
            {
                m_SubObjectTriggerInfo = JsonMapper.ToObject<List<DynamicObjectSubObjectTrigger>>(Row.SubObjectTriggerList);
            }
            catch
            {

            }

            if (m_SubObjectTriggerInfo != null)
            {
                foreach (var subObjectTrigger in m_SubObjectTriggerInfo)
                {
                    var collider = CachedTransfrom.Find(subObjectTrigger.Path)?.GetComponent<Collider>();
                    if (collider != null)
                    {
                        var receiver = collider.transform.GetOrAddComponent<PangooColliderReceiver>();
                        if (receiver != null)
                        {
                            receiver.dynamicObject = this;
                            receiver.subObjectTriggerEventType = subObjectTrigger.TriggerEventType;
                        }
                    }

                }
            }
        }

        public void TriggerEnter3d(Collider collider)
        {
            EnterTriggerCount += 1;
            TriggerInovke(TriggerTypeEnum.OnTriggerEnter3D);

        }

        public void TriggerExit3d(Collider collider)
        {
            EnterTriggerCount -= 1;
            TriggerInovke(TriggerTypeEnum.OnTriggerExit3D);
        }

        public void ExtraTriggerEnter3d(Collider collider)
        {
            EnterTriggerCount += 1;
            TriggerInovke(TriggerTypeEnum.OnExtraTriggerEnter3D);
        }

        public void ExtraTriggerExit3d(Collider collider)
        {
            EnterTriggerCount -= 1;
            TriggerInovke(TriggerTypeEnum.OnExtraTriggerExit3D);

        }


    }
}