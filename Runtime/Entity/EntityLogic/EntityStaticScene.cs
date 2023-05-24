using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using GameFramework;

namespace Pangoo{
    public  class EntityStaticScene : EntityBase
    {
        [ShowInInspector]
        public EntityInfo Info{
            get{
                if(StaticSceneData != null){
                    return StaticSceneData.Info;
                }
                return null;
            }
        }

        [ShowInInspector]
        public EntityStaticSceneData StaticSceneData;

        [ShowInInspector]
        StaticSceneManager Manager{
            get{
                if(StaticSceneData != null){
                    return StaticSceneData.Manager;
                }
                return null;
            }
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
           
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            StaticSceneData = userData as EntityStaticSceneData;
            if (StaticSceneData == null)
            {
                Log.Error("Entity data is invalid.");
                return;
            }

            Name =  Utility.Text.Format("{0}[{1}]",StaticSceneData.Info.Name,Id);

        }

        

        public  void ShowScene(){
            foreach(var child in transform.Children()){
                child.gameObject.SetActive(true);
            }
        }
        public void HideScene(){
            foreach(var child in transform.Children()){
                child.gameObject.SetActive(false);
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        private void OnTriggerEnter(Collider other) {
            Manager.EnterScene(StaticSceneData.Info.Id);
        }


        private void OnTriggerExit(Collider other) {
            Manager.ExitScene(StaticSceneData.Info.Id);
        }


   

    }
}