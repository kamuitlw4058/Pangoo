using System;
using GameFramework;
using Sirenix.OdinInspector;


namespace Pangoo.Service
{
    [Serializable]
    public abstract class ServiceBase : ILifeCycle
    {
        EventHelper m_EventHelper;
        IServiceContainer m_Services;

        [ShowInInspector]
        public virtual int Priority
        {
            get
            {
                return 0;
            }
        }

        public EventHelper EventHelper
        {
            get
            {
                return m_EventHelper;
            }
        }

        public IServiceContainer Services
        {
            get
            {
                return m_Services;
            }
        }

        bool m_IsActivate = false;

        public bool IsActivate
        {
            get
            {
                return m_IsActivate;
            }
            private set
            {
                if (m_IsActivate == value)
                {
                    return;
                }

                m_IsActivate = value;
                switch (value)
                {
                    case true: DoEnable(); break;
                    case false: DoDisable(); break;
                }

            }
        }


        public void SetActivate(bool val)
        {
            IsActivate = val;
        }

        public virtual void DoAwake(IServiceContainer services)
        {
            m_EventHelper = EventHelper.Create(this);
            m_Services = services;
        }

        public virtual void DoStart()
        {
        }

        public virtual void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {

        }


        public virtual void DoDestroy()
        {
            if (m_EventHelper != null)
            {
                m_EventHelper.UnSubscribeAll();
                ReferencePool.Release(m_EventHelper);

            }
        }

        public virtual void DoEnable()
        {

        }

        public virtual void DoDisable()
        {

        }

        public virtual void DoFixedUpdate()
        {

        }

        public virtual void DoDrawGizmos()
        {

        }
    }
}