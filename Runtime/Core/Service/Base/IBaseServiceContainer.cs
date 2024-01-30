using System.Collections.Generic;
using System.Collections;

namespace Pangoo.Core.Services
{
    public interface IBaseServiceContainer : IBaseServiceTime
    {
        T GetService<T>() where T : BaseService;

        void RemoveService(BaseService service);

        void AddService(BaseService service, bool sortService = true);


        BaseService[] Childern { get; }


    }
}