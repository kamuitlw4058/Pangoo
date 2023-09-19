using System;
using UnityEngine;
using GameFramework;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;


namespace Pangoo.Service
{
    public abstract partial class NestedServiceBase
    {

        [SerializeReference]
        [HideIf("@this.m_ChildernArray.Length == 0")]
        private INestedService[] m_ChildernArray = new INestedService[0];
        private readonly Dictionary<Type, INestedService> m_ChildernDict = new Dictionary<Type, INestedService>();

        public INestedService[] Childern
        {
            get
            {
                return m_ChildernArray;
            }
        }
        public virtual void AddService(INestedService service)
        {
            INestedService cachedService;
            Type serviceType = service.GetType();
            if (m_ChildernDict.TryGetValue(serviceType, out cachedService))
            {
                return;
            }


            m_ChildernDict.Add(serviceType, service);

            if (m_ChildernArray == null || m_ChildernArray.Length == 0)
            {
                m_ChildernArray = new INestedService[1];
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

        public virtual void RemoveService(INestedService service)
        {
            INestedService cachedService;
            Type serviceType = service.GetType();
            if (!m_ChildernDict.TryGetValue(serviceType, out cachedService))
            {
                return;
            }

            cachedService.Disable();
            cachedService.Destroy();
        }

        public T GetService<T>() where T : class, INestedService
        {
            INestedService ret;
            var keyType = typeof(T);
            if (m_ChildernDict.TryGetValue(keyType, out ret))
            {
                return (T)ret;
            }

            return null;
        }
    }
}