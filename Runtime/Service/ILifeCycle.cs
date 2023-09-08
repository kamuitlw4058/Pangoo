namespace Pangoo.Service
{
    public interface ILifeCycle
    {
        void DoAwake(IServiceContainer services);
        void DoStart();

        void DoEnable();
        
        void DoFixedUpdate();
        void DoUpdate(float elapseSeconds, float realElapseSeconds);
        

        void DoDisable();

        void DoDestroy();

        void DoDrawGizmos();
    }
}