namespace Pangoo.Service
{
    public interface IServiceContainer
    {
        T GetService<T>() where T : ServiceBase;

        public void RegisterService(ServiceBase service);
    }
}