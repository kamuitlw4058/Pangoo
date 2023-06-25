using System;
using System.Collections.Generic;
using System.Linq;

namespace Pangoo.Service
{
    public class TestServiceContainer : ServiceContainer,ILifeCycle
    {
        public TestServiceContainer(){
            RegisterService( new ExcelTableService());
            RegisterService( new StaticSceneService());
            RegisterService( new GameSectionService());
            RegisterService(new GlobalDataService());
            RegisterService(new SaveLoadService());
            RegisterService(new RuntimeDataService());
        }

        public void DoAwake(){
            DoAwake(this);
        }

        public void DoAwake(IServiceContainer services)
        {
            foreach(var service in GetAllServices()){
                service.DoAwake(services);
            }
        }

        public void DoDestroy()
        {
            
        }

        public void DoStart()
        {
            foreach(var service in GetAllServices()){
                service.DoStart();
            }
        }

        public void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {
            foreach(var service in GetAllServices()){
                service.DoUpdate(elapseSeconds,realElapseSeconds);
            }
        }
    }
}