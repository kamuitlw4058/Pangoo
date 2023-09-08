using System.Collections.Generic;
using System.Collections;

namespace Pangoo.Service
{
    public interface IServiceContainer
    {
        T GetVariable<T>(string key, T default_val = default(T));

        void SetVariable<T>(string key, object val, bool overwrite = true);

        T GetService<T>() where T : ServiceBase;

        public void RegisterService(ServiceBase service);
    }
}