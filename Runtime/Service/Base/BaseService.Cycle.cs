using System;
using GameFramework;
using Sirenix.OdinInspector;


namespace Pangoo.Service
{
    public abstract partial class BaseService
    {
        protected bool IsAwaked = false;

        protected bool IsStarted = false;



        public virtual void Awake()
        {
            m_EventHelper = EventHelper.Create(this);
            DoAwake();
        }

        public virtual void Destroy()
        {
            DoDestroy();
            if (m_EventHelper != null)
            {
                m_EventHelper.UnSubscribeAll();
                ReferencePool.Release(m_EventHelper);
            }
        }

        public virtual void Disable()
        {
            DoDisable();
        }

        public virtual void DrawGizmos()
        {
            DoDrawGizmos();
        }

        public virtual void Enable()
        {
            DoEnable();
        }

        public virtual void FixedUpdate()
        {
            DoFixedUpdate();
        }

        public virtual void Start()
        {
            DoStart();
        }

        public virtual void Update()
        {
            DoUpdate();
        }


        // ----------


        protected virtual void DoAwake()
        {
        }

        protected virtual void DoStart()
        {
        }

        protected virtual void DoUpdate()
        {

        }


        protected virtual void DoDestroy()
        {

        }

        protected virtual void DoEnable()
        {

        }

        protected virtual void DoDisable()
        {

        }

        protected virtual void DoFixedUpdate()
        {

        }

        protected virtual void DoDrawGizmos()
        {

        }
    }
}