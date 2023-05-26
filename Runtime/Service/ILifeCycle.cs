namespace Pangoo.Service
{
    public interface ILifeCycle
    {
        void DoAwake(IServiceContainer services);
        void DoStart();

        void DoUpdate(float elapseSeconds, float realElapseSeconds);
        void DoDestroy();
    }
}