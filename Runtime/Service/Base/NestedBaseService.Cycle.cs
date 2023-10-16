using System;
using System.Linq;
using GameFramework;
using Sirenix.OdinInspector;
using System.Collections.Generic;



namespace Pangoo.Service
{
    public partial class NestedBaseService
    {
        public override void Awake()
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

        public override void Start()
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

        public override void Update()
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


        public override void Destroy()
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

        public override void Enable()
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

        public override void Disable()
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

        public override void FixedUpdate()
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

        public override void DrawGizmos()
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