using System;
using System.Linq;
using GameFramework;
using Sirenix.OdinInspector;
using System.Collections.Generic;



namespace Pangoo.Core.Services
{
    public partial class NestedBaseService : BaseService, IBaseServiceContainer
    {
        public override float DeltaTime => UnityEngine.Time.deltaTime;
        public override float Time => UnityEngine.Time.time;


        // [field: NonSerialized]
        // [ShowInInspector]
        // [HideIf("@this.m_ChildernList == null ||  (this.m_ChildernList != null && this.m_ChildernList.Count == 0)")]
        private List<BaseService> m_ChildernList;
        private Dictionary<Type, BaseService> m_ChildernDict;

        void CheckList()
        {
            if (m_ChildernList == null)
            {
                m_ChildernList = new List<BaseService>();
            }
        }

        void CheckDict()
        {
            if (m_ChildernDict == null)
            {
                m_ChildernDict = new Dictionary<Type, BaseService>();
            }
        }

        public BaseService[] Childern
        {
            get
            {
                CheckList();
                return m_ChildernList.ToArray();
            }
        }
        public void SortService()
        {
            CheckList();
            m_ChildernList.Sort((x, y) => x.Priority.CompareTo(y.Priority));
        }

        public virtual void AddService(BaseService service, bool sortService = true)
        {
            CheckList();
            CheckDict();

            BaseService cachedService;
            Type serviceType = service.GetType();
            if (m_ChildernDict.TryGetValue(serviceType, out cachedService))
            {
                return;
            }


            m_ChildernDict.Add(serviceType, service);

            if (m_ChildernList == null)
            {
                m_ChildernList = new List<BaseService>();
            }
            else
            {
                if (!m_ChildernList.Contains(service))
                {
                    m_ChildernList.Add(service);
                    if (sortService)
                    {
                        m_ChildernList.Sort((x, y) => x.Priority.CompareTo(y.Priority));
                    }
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
            CheckDict();
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
            CheckDict();
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