using System;
using System.Linq;
using GameFramework;
using Sirenix.OdinInspector;
using System.Collections.Generic;



namespace Pangoo.Service
{
    [Serializable]
    public partial class NestedBaseService : BaseService, IBaseServiceContainer
    {
        public override float DeltaTime => UnityEngine.Time.deltaTime;
        public override float Time => UnityEngine.Time.time;


        [field: NonSerialized]
        [ShowInInspector]
        [HideIf("@this.m_ChildernArray.Length == 0")]
        private BaseService[] m_ChildernArray = new BaseService[0];
        private readonly Dictionary<Type, BaseService> m_ChildernDict = new Dictionary<Type, BaseService>();

        public BaseService[] Childern
        {
            get
            {
                return m_ChildernArray;
            }
        }
        public virtual void AddService(BaseService service)
        {
            BaseService cachedService;
            Type serviceType = service.GetType();
            if (m_ChildernDict.TryGetValue(serviceType, out cachedService))
            {
                return;
            }


            m_ChildernDict.Add(serviceType, service);

            if (m_ChildernArray == null || m_ChildernArray.Length == 0)
            {
                m_ChildernArray = new BaseService[1];
                m_ChildernArray[0] = service;
            }
            else
            {
                var childernList = m_ChildernArray.ToList();
                if (!childernList.Contains(service))
                {
                    childernList.Add(service);
                    childernList.Sort((x, y) => x.Priority.CompareTo(y.Priority));
                    m_ChildernArray = childernList.ToArray();
                }
            }
            service.Parent = this;

            if (IsAwaked)
            {
                service.Awake();
            }


            if (IsStarted)
            {
                service.Start();
            }
        }

        public virtual void RemoveService(BaseService service)
        {
            BaseService cachedService;
            Type serviceType = service.GetType();
            if (!m_ChildernDict.TryGetValue(serviceType, out cachedService))
            {
                return;
            }

            cachedService.Disable();
            cachedService.Destroy();
        }

        public T GetService<T>() where T : BaseService
        {
            BaseService ret;
            var keyType = typeof(T);
            if (m_ChildernDict.TryGetValue(keyType, out ret))
            {
                return (T)ret;
            }

            return null;
        }
    }
}