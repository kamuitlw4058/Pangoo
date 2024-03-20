using GameFramework;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Pangoo.Core.Services
{
    public abstract partial class BaseService
    {
        protected bool IsAwaked = false;

        protected bool IsStarted = false;


        public virtual string ServiceName => "Service";


        public virtual void Log(string message)
        {
            Debug.Log($"[F:{UnityEngine.Time.frameCount % 1000}]{ServiceName}-> {message}");
        }

        public virtual void LogWarning(string message)
        {
            Debug.LogWarning($"[F:{UnityEngine.Time.frameCount % 1000}]{ServiceName}-> {message}");
        }



        public virtual void LogError(string message)
        {
            Debug.LogError($"[F:{UnityEngine.Time.frameCount % 1000}]{ServiceName}-> {message}");
        }


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

        public virtual void PointerEnter(PointerEventData pointerEventData)
        {
            DoPointerEnter(pointerEventData);
        }

        public virtual void PointerExit(PointerEventData pointerEventData)
        {
            DoPointerExit(pointerEventData);
        }

        public virtual void PointerClick(PointerEventData pointerEventData)
        {
            DoPointerClick(pointerEventData);
        }

    }
}