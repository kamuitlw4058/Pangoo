using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo
{
    [Serializable]
     public class StaticSceneManager : IReference
    {
        // private Dictionary<int, Action<Entity>> dicCallback;
        // private Dictionary<int, Entity> dicSerial2Entity;

        // private List<int> tempList;

        // public object Owner
        // {
        //     get;
        //     private set;
        // }
        EntityLoader Loader = null;

        [ShowInInspector]

        Dictionary<int,EntityStaticSceneData> StaticSceneDataDict;

        Dictionary<int,int> EnterSceneDict;
        Dictionary<int,EntityStaticScene> StaticSceneDict;
        List<int> LoadingScene;

        
        EntityStaticSceneData CreateData(int id){
              var info = new EntityInfo{
                    Id = id,
                    Name = $"Room{id}",
                    AssetPath= $"Assets/Plugins/Pangoo/StreamRes/Prefab/Demo/Room{id}.prefab",
                    GroupName = "Default",
                };
                var loadIds = new List<int>();
                switch(id){
                    case 1:
                        loadIds.Add(2);
                        break;
                    case 2:
                        loadIds.Add(3);
                        loadIds.Add(4);
                        loadIds.Add(1);
                        break;
                    case 3:
                        loadIds.Add(2);
                        break;
                    case 4:
                        loadIds.Add(2);
                        break;
                }
            return EntityStaticSceneData.Create(info,loadIds,this);
        }

        public StaticSceneManager()
        {
            EnterSceneDict = new Dictionary<int, int>();
            LoadingScene = new List<int>();
            StaticSceneDict = new Dictionary<int, EntityStaticScene>();
            Loader = EntityLoader.Create(this);
            StaticSceneDataDict = new Dictionary<int, EntityStaticSceneData>();
            for(int i =1; i<=4;i ++){
                var info = new EntityInfo{
                    Id = i,
                    Name = $"Room{i}",
                    AssetPath= $"Assets/Plugins/Pangoo/StreamRes/Prefab/Demo/Room{i}.prefab",
                    GroupName = "Default",
                };
                var loadIds = new List<int>();
                switch(i){
                    case 1:
                        loadIds.Add(2);
                        break;
                    case 2:
                        loadIds.Add(3);
                        loadIds.Add(4);
                        loadIds.Add(1);
                        break;
                    case 3:
                        loadIds.Add(2);
                        break;
                    case 4:
                        loadIds.Add(2);
                        break;
                }
                var data = EntityStaticSceneData.Create(info,loadIds,this);
                StaticSceneDataDict.Add(i,data);
            }

        }

        public void PrintDebug(){
            // Debug.Log($"Enter PrintDebug");
            // foreach(var id in EnterSceneList){
            //     Debug.Log($"Enter Id:{id}");
            // }

            // List<int> needLoadIds = new List<int>();
            // foreach(var enterScene in  EnterSceneList){
            //     var ids = GetLoadIds(enterScene);
            //     foreach(var id in ids){
            //         if(!needLoadIds.Contains(id)){
            //             needLoadIds.Add(id);
            //         }
            //     }
            // }

            // foreach(var id in needLoadIds){
            //     Debug.Log($"NeedLoad Ids:{id}");
            // }

            // foreach(var scene in StaticSceneDict){

            //     Debug.Log($"SceneDict Ids:{scene.Value.Info.Id}");
            // }
        }

        public List<int> GetLoadIds(int id){
             EntityStaticSceneData data;
            if(!StaticSceneDataDict.TryGetValue(id,out data)){
                return new List<int>();
            }
            return data.LoadIds;
        }

        public void  ShowStaticScene(int id){
            // Debug.Log($"Show Static Scene:{id}");
            EntityStaticSceneData data = CreateData(id);
            EntityStaticScene scene;

            if(StaticSceneDict.TryGetValue(id,out scene)){
                return;
            }
       

            if(LoadingScene.Contains(id)){
                return;
            }else{
                LoadingScene.Add(id);
                Loader.ShowEntity(EnumEntity.StaticScene,
                    (o)=>{
                        if(LoadingScene.Contains(id)){
                            LoadingScene.Remove(id);
                        }
                        StaticSceneDict.Add(id, o.Logic as EntityStaticScene);   
                    },
                    data.Info,
                    data);
            }

  
        }

        public void EnterScene(int id){
            int count;
            if(!EnterSceneDict.TryGetValue(id,out count)){
                EnterSceneDict.Add(id,1);
            }else{
                EnterSceneDict[id] = count +1;
            }
        }

        public void ExitScene(int id){
            int count;
            if(EnterSceneDict.TryGetValue(id,out count)){
                count -=1;
                if(count ==0){
                    EnterSceneDict.Remove(id);
                }else{
                    EnterSceneDict[id] = count;
                }
            }
        }


        public void OnUpdate( float elapseSeconds, float realElapseSeconds){
            if(EnterSceneDict.Count == 0){
                return;
            }

            List<int> needLoadIds = new List<int>();
            foreach(var enterScene in  EnterSceneDict){
                EntityStaticScene scene;
                if(StaticSceneDict.TryGetValue(enterScene.Key,out scene)){
                    foreach(var id in scene.StaticSceneData.LoadIds){
                        if(!needLoadIds.Contains(id)){
                            needLoadIds.Add(id);
                        }
                    }
                }
     
            }

            foreach(var loadId in needLoadIds){
                ShowStaticScene(loadId);
            }

            List<int> removeScene = new List<int>();
            foreach(var item in  StaticSceneDict){
                if(!needLoadIds.Contains(item.Value.Info.Id) && !EnterSceneDict.ContainsKey(item.Value.Info.Id)){
                    if(Loader.GetEntity(item.Value.Id)){
                        Debug.Log($"Hide Entity:{item.Value.Id},{item.Key}");
                        Loader.HideEntity(item.Value.Id);  
                    }
                   
                    removeScene.Add(item.Key);
                }
            }

            foreach(var id in removeScene){
                StaticSceneDict.Remove(id);
            }

        }

        public static StaticSceneManager Create(object owner)
        {
            StaticSceneManager val = ReferencePool.Acquire<StaticSceneManager>();
            // entityLoader.Owner = owner;
            // PangooEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, entityLoader.OnShowEntitySuccess);
            // PangooEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, entityLoader.OnShowEntityFail);

            return val;
        }

        public void Clear()
        {
            // Owner = null;
            // dicSerial2Entity.Clear();
            // dicCallback.Clear();
            // PangooEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            // PangooEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFail);
        }
    }
}
