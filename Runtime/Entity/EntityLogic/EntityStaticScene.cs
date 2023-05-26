using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using GameFramework;
using Pangoo.Service;

namespace Pangoo{
    public  class EntityStaticScene : EntityBase
    {
        [ShowInInspector]
        public EntityInfo Info{
            get{
                if(SceneData != null){
                    return SceneData.EntityInfo;
                }
                return null;
            }
        }

        [ShowInInspector]
        public EntityStaticSceneData SceneData;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
           
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            SceneData = userData as EntityStaticSceneData;
            if (SceneData == null)
            {
                Log.Error("Entity data is invalid.");
                return;
            }

            Name =  Utility.Text.Format("{0}[{1}]",SceneData.EntityInfo.AssetName,Id);
        }


        private void OnTriggerEnter(Collider other) {
            EventHelper.Fire(this,EnterStaticSceneEventArgs.Create(SceneData.AssetPathId));
        }


        private void OnTriggerExit(Collider other) {
            EventHelper.Fire(this,ExitStaticSceneEventArgs.Create(SceneData.AssetPathId));
        }


    }
}