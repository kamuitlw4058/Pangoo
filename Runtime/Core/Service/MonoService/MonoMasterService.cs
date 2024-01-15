using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.Core.Common;
using System;

namespace Pangoo.Core.Services
{

    public class MonoMasterService : NestedBaseService
    {

        [SerializeField] TimeMode MonoTimeMode;

        public override float DeltaTime => MonoTimeMode.DeltaTime;
        public override float Time => MonoTimeMode.Time;

        [NonSerialized]
        protected Transform m_CachedTransfrom;

        [NonSerialized]
        GameObject m_GameObject;

        public GameObject gameObject
        {
            get
            {
                return m_GameObject;
            }
            set
            {
                m_GameObject = value;
            }
        }

        [ShowInInspector]
        public Transform CachedTransfrom
        {
            get
            {
                if (m_CachedTransfrom == null)
                {
                    m_CachedTransfrom = m_GameObject?.transform;
                }
                return m_CachedTransfrom;
            }

        }


        public MonoMasterService(GameObject gameObject)
        {
            m_GameObject = gameObject;
        }

        public MonoMasterService() { }

    }

}

