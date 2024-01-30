using GameFramework;

using UnityEngine.EventSystems;


namespace Pangoo.Core.Services
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
            if (m_ChildernList != null)
            {
                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].Awake();
                }
            }
            DoAwake();
        }

        public override void Start()
        {
            IsStarted = true;
            if (m_ChildernList != null)
            {
                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].Start();
                }
            }
            DoStart();

        }

        public override void Update()
        {
            if (m_ChildernList != null)
            {
                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].Update();
                }
            }

            DoUpdate();
        }


        public override void Destroy()
        {
            if (m_ChildernList != null)
            {

                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].Destroy();
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
            if (m_ChildernList != null)
            {
                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].Enable();
                }
            }
            DoEnable();
        }

        public override void Disable()
        {
            if (m_ChildernList != null)
            {
                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].Disable();
                }
            }
            DoDisable();
        }

        public override void FixedUpdate()
        {
            if (m_ChildernList != null)
            {
                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].FixedUpdate();
                }
            }
            DoFixedUpdate();
        }

        public override void DrawGizmos()
        {
            if (m_ChildernList != null)
            {
                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].DrawGizmos();
                }
            }

            DoDrawGizmos();
        }


        public override void PointerEnter(PointerEventData pointerEventData)
        {
            if (m_ChildernList != null)
            {
                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].PointerEnter(pointerEventData);
                }
            }

            DoPointerEnter(pointerEventData);
        }

        public override void PointerExit(PointerEventData pointerEventData)
        {
            if (m_ChildernList != null)
            {
                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].PointerExit(pointerEventData);
                }
            }

            DoPointerExit(pointerEventData);
        }

        public override void PointerClick(PointerEventData pointerEventData)
        {
            if (m_ChildernList != null)
            {
                for (int i = 0; i < m_ChildernList.Count; i++)
                {
                    m_ChildernList[i].PointerClick(pointerEventData);
                }
            }

            DoPointerClick(pointerEventData);
        }

    }
}