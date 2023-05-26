using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;


namespace Pangoo.Service
{
    [Serializable]
    public class ServiceContainer : IServiceContainer
    {
        [ShowInInspector]
        private List<ServiceBase> ServerList{
            get{
                return m_AllServices.Values.ToList();
            }
        }

      
        private readonly Dictionary<Type, ServiceBase> m_AllServices = new Dictionary<Type, ServiceBase>();

        public ServiceBase[] GetAllServices()
        {
            return m_AllServices.Values.ToArray();
        }

        /// <summary> 通过接口类型作为key值注册service </summary>
        // public void RegisterService(IService service, bool overwriteExisting = true)
        public void RegisterService(ServiceBase service)
        {
            // 由于目前不存在一类代码多个实现的可能。
            // 这边取消了原先的接口设定使用基类的方式实现。用来提升开发效果。
            m_AllServices.Add(service.GetType(),service);

            // var interfaceTypes = service.GetType().FindInterfaces((type, criteria) =>
            //         type.GetInterfaces().Any(t => t == typeof(IService)), service)
            //     .ToArray();

            // foreach (var type in interfaceTypes)
            // {
            //     if (!m_AllServices.ContainsKey(type))
            //         m_AllServices.Add(type, service);
            //     else if (overwriteExisting)
            //     {
            //         m_AllServices[type] = service;
            //     }
            // }
        }

        public T GetService<T>() where T : ServiceBase
        {
            var key = typeof(T);
            return (T) m_AllServices[key];
        }
    }
}