using System.Collections.Generic;
using System.Collections;

namespace Pangoo.Service
{
    public interface IBaseServiceContainer : IBaseServiceTime
    {
        T GetService<T>() where T : BaseService;

        void RemoveService(BaseService service);

        void AddService(BaseService service);


        BaseService[] Childern { get; }


    }
}