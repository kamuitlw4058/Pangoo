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

        public void Awake()
        {
            if (IsAwaked)
            {
                return;
            }

            IsAwaked = true;
            m_EventHelper = EventHelper.Create(this);
            if (m_ChildernArray != null)
            {
                for (int i = 0; i < m_ChildernArray.Length; i++)
                {
                    m_ChildernArray[i].Awake();
                }
            }
            DoAwake();
        }

        public void Start()
        {
            IsStarted = true;
            if (m_ChildernArray != null)
            {
                for (int i = 0; i < m_ChildernArray.Length; i++)
                {
                    m_ChildernArray[i].Start();
                }
            }
            DoStart();

        }

        public void Update()
        {
            if (m_ChildernArray != null)
            {
                for (int i = 0; i < m_ChildernArray.Length; i++)
                {
                    m_ChildernArray[i].Update();
                }
            }

            DoUpdate();
        }


        public void Destroy()
        {
            if (m_ChildernArray != null)
            {

                for (int i = 0; i < m_ChildernArray.Length; i++)
                {
                    m_ChildernArray[i].Destroy();
                }
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
            if (m_ChildernArray != null)
            {
                for (int i = 0; i < m_ChildernArray.Length; i++)
                {
                    m_ChildernArray[i].Enable();
                }
            }
            DoEnable();
        }

        public void Disable()
        {
            if (m_ChildernArray != null)
            {
                for (int i = 0; i < m_ChildernArray.Length; i++)
                {
                    m_ChildernArray[i].Disable();
                }
            }
            DoDisable();
        }

        public void FixedUpdate()
        {
            if (m_ChildernArray != null)
            {
                for (int i = 0; i < m_ChildernArray.Length; i++)
                {
                    m_ChildernArray[i].FixedUpdate();
                }
            }
            DoFixedUpdate();
        }

        public void DrawGizmos()
        {
            if (m_ChildernArray != null)
            {
                for (int i = 0; i < m_ChildernArray.Length; i++)
                {
                    m_ChildernArray[i].DrawGizmos();
                }
            }

            DoDrawGizmos();
        }
    }
}