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
            //RegisterService(new KeyValueService());
            RegisterService(new GlobalDataService());
            RegisterService(new SaveLoadService());
            RegisterService(new RuntimeDataService());
            RegisterService(dataContainerService = new DataContainerService());
            
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
        public float obj;
        public int testObj;
        public void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {
            foreach(var service in GetAllServices()){
                service.DoUpdate(elapseSeconds,realElapseSeconds);
            }
            
            if (Input.GetKeyDown(KeyCode.K))
            {
                obj=dataContainerService.Get<float>("Cube0.Light.Inset");
                //testObj = dataContainerService.Get<int>("123");
                
                Debug.LogError($"obj:{obj}");
                //Debug.LogError($"testObj:{testObj}");
            }
        }
    }
}