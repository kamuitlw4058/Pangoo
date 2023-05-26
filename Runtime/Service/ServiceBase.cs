using GameFramework;


namespace Pangoo.Service
{
    public abstract class ServiceBase : ILifeCycle
    {
         EventHelper m_EventHelper;

         public EventHelper EventHelper{
            get{
                return m_EventHelper;
            }
         }

        public virtual  void DoAwake(IServiceContainer services)
        {
            m_EventHelper = EventHelper.Create(this);
        }

        public virtual void DoStart()
        {
        }

        public virtual void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }


        public virtual void DoDestroy()
        {
            if(m_EventHelper != null){
                m_EventHelper.UnSubscribeAll();
                ReferencePool.Release(m_EventHelper);
                
            }
        }
    }
}