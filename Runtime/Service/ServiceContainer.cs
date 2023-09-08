using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Sirenix.OdinInspector;


namespace Pangoo.Service
{
    [Serializable]
    public class ServiceContainer : IServiceContainer
    {
        Dictionary<string, object> m_VariableDict = new Dictionary<string, object>();


        [SerializeReference]
        private ServiceBase[] m_ServiesArray;
        private readonly Dictionary<Type, ServiceBase> m_AllServices = new Dictionary<Type, ServiceBase>();

        public ServiceBase[] GetAllServices()
        {
            return m_ServiesArray;
        }

        /// <summary> 通过接口类型作为key值注册service </summary>
        // public void RegisterService(IService service, bool overwriteExisting = true)
        public virtual void RegisterService(ServiceBase service)
        {
            // 由于目前不存在一类代码多个实现的可能。
            // 这边取消了原先的接口设定使用基类的方式实现。用来提升开发效果。
            m_AllServices.Add(service.GetType(), service);

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

            if (m_ServiesArray == null)
            {
                m_ServiesArray = new ServiceBase[1];
                m_ServiesArray[0] = service;
            }
            else
            {
                if (!m_ServiesArray.Contains(service))
                {
                    var listServies = m_ServiesArray.ToList();
                    listServies.Add(service);
                    listServies.Sort((x, y) => x.Priority.CompareTo(y.Priority));
                    m_ServiesArray = listServies.ToArray();
                }
            }

        }

        public T GetService<T>() where T : ServiceBase
        {
            var key = typeof(T);
            return (T)m_AllServices[key];
        }

        public T GetVariable<T>(string key, T default_val = default(T))
        {
            object val = null;
            if (m_VariableDict.TryGetValue(key, out val))
            {
                return (T)val;
            }
            return default_val;
        }

        public void SetVariable<T>(string key, object val, bool overwrite = true)
        {

            if (!m_VariableDict.TryAdd(key, (T)val) && overwrite)
            {
                m_VariableDict[key] = (T)val;
            }
        }

    }
}