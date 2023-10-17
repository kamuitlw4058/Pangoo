using System;
using GameFramework;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Services
{
    [Serializable]
    public abstract partial class BaseService : IBaseServiceLifeCycle
    {

        protected IBaseServiceContainer m_Parent;
        protected EventHelper m_EventHelper;

        public EventHelper Event
        {
            get
            {
                return m_EventHelper;
            }
            set
            {
                m_EventHelper = value;
            }
        }

        public virtual bool HasChildService => false;

        public virtual float DeltaTime => Parent != null ? Parent.DeltaTime : UnityEngine.Time.deltaTime;
        public virtual float Time => Parent != null ? Parent.Time : UnityEngine.Time.time;

        public virtual IBaseServiceContainer Parent
        {
            get
            {
                return m_Parent;
            }
            set
            {
                m_Parent = value;
            }
        }

        public virtual int Priority => 0;

    }
}