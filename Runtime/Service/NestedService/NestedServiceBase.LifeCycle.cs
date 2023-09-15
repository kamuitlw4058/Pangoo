using System;
using UnityEngine;
using GameFramework;


namespace Pangoo.Service
{
    public abstract partial class NestedServiceBase
    {
        bool IsAwaked = false;

        bool IsStarted = false;

        // public void Awake()
        // {
        //     Awake(m_Parent);
        // }

        public void Awake(INestedService parent)
        {
            m_Parent = parent;
            if (IsAwaked)
            {
                return;
            }

            IsAwaked = true;
            m_EventHelper = EventHelper.Create(this);
            for (int i = 0; i < m_ChildernArray.Length; i++)
            {
                m_ChildernArray[i].Awake(this);
            }
            DoAwake(parent);
        }

        public void Start()
        {
            IsStarted = true;
            for (int i = 0; i < m_ChildernArray.Length; i++)
            {
                m_ChildernArray[i].Start();
            }
            DoStart();

        }

        public void Update()
        {
            for (int i = 0; i < m_ChildernArray.Length; i++)
            {
                m_ChildernArray[i].Update();
            }

            DoUpdate();
        }


        public void Destroy()
        {
            for (int i = 0; i < m_ChildernArray.Length; i++)
            {
                m_ChildernArray[i].Destroy();
            }

            DoDestroy();

            if (m_EventHelper != null)
            {
                m_EventHelper.UnSubscribeAll();
                ReferencePool.Release(m_EventHelper);

            }
        }

        public void Enable()
        {
            for (int i = 0; i < m_ChildernArray.Length; i++)
            {
                m_ChildernArray[i].Enable();
            }
            DoEnable();
        }

        public void Disable()
        {
            for (int i = 0; i < m_ChildernArray.Length; i++)
            {
                m_ChildernArray[i].Disable();
            }
            DoDisable();
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < m_ChildernArray.Length; i++)
            {
                m_ChildernArray[i].FixedUpdate();
            }
            DoFixedUpdate();
        }

        public void DrawGizmos()
        {
            for (int i = 0; i < m_ChildernArray.Length; i++)
            {
                m_ChildernArray[i].DrawGizmos();
            }
            DoDrawGizmos();
        }
    }
}