using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;


namespace Pangoo.Service
{
    [Serializable]
    public class LifeCycleServiceContainer : ServiceContainer, ILifeCycle
    {
        public void Awake()
        {

            foreach (var service in GetAllServices())
            {
                service.DoAwake(this);
            }

            DoAwake(this);
        }

        public void Destroy()
        {
            DoDestroy();
        }

        public void Start()
        {
            foreach (var service in GetAllServices())
            {
                service.DoStart();
            }

            DoStart();
        }

        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach (var service in GetAllServices())
            {
                service.DoUpdate(elapseSeconds, realElapseSeconds);
            }

            DoUpdate(elapseSeconds, realElapseSeconds);
        }

        public void Enable()
        {
            foreach (var service in GetAllServices())
            {
                service.DoEnable();
            }
            DoEnable();
        }

        public void Disable()
        {
            foreach (var service in GetAllServices())
            {
                service.DoDisable();
            }
            DoDisable();
        }

        public void DrawGizmos()
        {
            foreach (var service in GetAllServices())
            {
                service.DoDisable();
            }
            DoDrawGizmos();
        }

        public void FixedUpdate()
        {
            DoFixedUpdate();
        }

        public virtual void DoAwake(IServiceContainer services)
        {
        }

        public virtual void DoDestroy()
        {

        }

        public virtual void DoStart()
        {

        }
        public virtual void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {

        }

        public virtual void DoEnable()
        {
        }

        public virtual void DoFixedUpdate()
        {
        }

        public virtual void DoDisable()
        {
        }

        public virtual void DoDrawGizmos()
        {
        }

    }
}