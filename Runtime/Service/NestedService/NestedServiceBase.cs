using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Pangoo.Service
{
    [Serializable]
    public abstract partial class NestedServiceBase : INestedService
    {
        [field: NonSerialized]
        EventHelper m_EventHelper;

        [ShowInInspector]
        public virtual int Priority
        {
            get
            {
                return 0;
            }
        }

        public virtual float DeltaTime => UnityEngine.Time.deltaTime;
        public virtual float Time => UnityEngine.Time.time;

        [field: NonSerialized]
        protected INestedService m_Parent;

        public INestedService Parent
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


        public NestedServiceBase()
        {

        }

        public NestedServiceBase(INestedService parent)
        {
            m_Parent = parent;
        }


    }
}