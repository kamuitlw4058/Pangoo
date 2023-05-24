using GameFramework;
using GameFramework.Event;
// using UnityGameFramework.Runtime;
using System;
using System.Collections.Generic;
// using Sirenix.OdinInspector;

namespace Pangoo
{
    [Serializable]
    public class StaticSceneManager : GameFrameworkModule,IStaticSceneManager
    {
        EntityLoader Loader = null;

        Dictionary<int,EntityStaticSceneData> m_StaticSceneDataDict;

        Dictionary<int,int> m_EnterSceneDict;
        Dictionary<int,EntityStaticScene> m_LoadedStaticSceneDict;
        List<int> m_LoadingScene;

        public Dictionary<int,EntityStaticScene>LoadedStaticSceneDict{
            get{
                return m_LoadedStaticSceneDict;
            }
        }

        public List<int> LoadingScene {
            get{
                return m_LoadingScene;
            }
        }

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
            m_EnterSceneDict = new Dictionary<int, int>();
            m_LoadingScene = new List<int>();
            m_LoadedStaticSceneDict = new Dictionary<int, EntityStaticScene>();
            // Loader = EntityLoader.Create(this);
            m_StaticSceneDataDict = new Dictionary<int, EntityStaticSceneData>();
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
                m_StaticSceneDataDict.Add(i,data);
            }

        }

        public List<int> GetLoadIds(int id){
             EntityStaticSceneData data;
            if(!m_StaticSceneDataDict.TryGetValue(id,out data)){
                return new List<int>();
            }
            return data.LoadIds;
        }

        public void  ShowStaticScene(int id){
            if(Loader == null){
                Loader = EntityLoader.Create(this);
            }

            EntityStaticScene scene;
            if(m_LoadedStaticSceneDict.TryGetValue(id,out scene)){
                return;
            }
       
            if(m_LoadingScene.Contains(id)){
                return;
            }else{
                EntityStaticSceneData data = CreateData(id);
                m_LoadingScene.Add(id);
                Loader.ShowEntity(EnumEntity.StaticScene,
                    (o)=>{
                        if(m_LoadingScene.Contains(id)){
                            m_LoadingScene.Remove(id);
                        }
                        m_LoadedStaticSceneDict.Add(id, o.Logic as EntityStaticScene);   
                    },
                    data.Info,
                    data);
            }
        }

        public void EnterScene(int id){
            int count;
            if(!m_EnterSceneDict.TryGetValue(id,out count)){
                m_EnterSceneDict.Add(id,1);
            }else{
                m_EnterSceneDict[id] = count +1;
            }
        }

        public void ExitScene(int id){
            int count;
            if(m_EnterSceneDict.TryGetValue(id,out count)){
                count -=1;
                if(count ==0){
                    m_EnterSceneDict.Remove(id);
                }else{
                    m_EnterSceneDict[id] = count;
                }
            }
        }

        public override void Update( float elapseSeconds, float realElapseSeconds){
            if(m_EnterSceneDict.Count == 0){
                return;
            }

            List<int> needLoadIds = new List<int>();
            foreach(var enterScene in  m_EnterSceneDict){
                EntityStaticScene scene;
                if(m_LoadedStaticSceneDict.TryGetValue(enterScene.Key,out scene)){
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
            foreach(var item in  m_LoadedStaticSceneDict){
                if(!needLoadIds.Contains(item.Value.Info.Id) && !m_EnterSceneDict.ContainsKey(item.Value.Info.Id)){
                    if(Loader.GetEntity(item.Value.Id)){
                        // Debug.Log($"Hide Entity:{item.Value.Id},{item.Key}");
                        Loader.HideEntity(item.Value.Id);  
                    }
                   
                    removeScene.Add(item.Key);
                }
            }

            foreach(var id in removeScene){
                m_LoadedStaticSceneDict.Remove(id);
            }

        }

        public override void Shutdown(){
            
        }

        // public static StaticSceneManager Create(object owner)
        // {
        //     StaticSceneManager val = ReferencePool.Acquire<StaticSceneManager>();
        //     // entityLoader.Owner = owner;
        //     // PangooEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, entityLoader.OnShowEntitySuccess);
        //     // PangooEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, entityLoader.OnShowEntityFail);

        //     return val;
        // }

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
