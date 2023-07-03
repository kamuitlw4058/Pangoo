using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pangoo.Service
{
    public class TestServiceContainer : ServiceContainer,ILifeCycle
    {
        public DataContainerService dataContainerService;
        public TestServiceContainer(){
            RegisterService( new ExcelTableService());
            RegisterService( new StaticSceneService());
            RegisterService( new GameSectionService());
            RegisterService(new GlobalDataService());
            RegisterService(new SaveLoadService());
            RegisterService(new RuntimeDataService());
            RegisterService(new DataContainerService());
            
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
        public int obj;
        public int testObj;
        public void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {
            foreach(var service in GetAllServices()){
                service.DoUpdate(elapseSeconds,realElapseSeconds);
            }
        }
    }
}