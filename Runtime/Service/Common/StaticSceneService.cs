using Pangoo;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using System;
using UnityGameFramework.Runtime;
using System.Linq;
using Sirenix.OdinInspector;

namespace Pangoo.Service
{
    public class StaticSceneService : ServiceBase
    {
        ExcelTableService m_ExcelTableService;

        StaticSceneTable m_StaticSceneTable;

        EntityLoader Loader = null;

        [ShowInInspector]
        Dictionary<int,int> m_EnterAssetCountDict;

        [ShowInInspector]
        Dictionary<int,EntityStaticScene> m_LoadedSceneAssetDict;

        [ShowInInspector]
        List<int> m_LoadingAssetIds;


        [ShowInInspector]

        Dictionary<int,StaticSceneInfo> m_SectionSceneInfos;

        [ShowInInspector]
        List<int> m_HoldStaticSceneIds;

        [ShowInInspector]
        Dictionary<int,int> NeedLoadDict;

        public bool AutoLoad {get;set;}


        public bool AutoRelease {get;set;}


        public Tuple<int,int> m_SectionChange;

       

        public override void DoAwake(IServiceContainer services){
            base.DoAwake(services);
            m_LoadingAssetIds = new List<int>();
            m_HoldStaticSceneIds = new List<int>();
            NeedLoadDict = new Dictionary<int, int>();
            m_SectionSceneInfos = new Dictionary<int, StaticSceneInfo>();
            m_ExcelTableService = services.GetService<ExcelTableService>();
            m_LoadedSceneAssetDict = new Dictionary<int, EntityStaticScene>();
            m_EnterAssetCountDict = new Dictionary<int, int>();
            AutoLoad = true;
            AutoRelease= true;
            EventHelper.Subscribe(EnterStaticSceneEventArgs.EventId,OnEnterStaticSceneEven);
            EventHelper.Subscribe(ExitStaticSceneEventArgs.EventId,OnExitStaticSceneEven);
        }

        void OnEnterStaticSceneEven(object sender, GameFrameworkEventArgs e){
            var args = e as EnterStaticSceneEventArgs;
            EnterSceneAsset(args.AssetPathId);
        }

        void OnExitStaticSceneEven(object sender, GameFrameworkEventArgs e){
            var args = e as ExitStaticSceneEventArgs;
            ExitSceneAsset(args.AssetPathId);
        }

        public override void DoStart(){
            m_StaticSceneTable = m_ExcelTableService.GetExcelTable<StaticSceneTable>();


        }

        public void SetSectionIds(List<int> ids){
            m_SectionSceneInfos.Clear();
            foreach(var id in ids){
                var sceneInfo = m_StaticSceneTable.GetStaticSceneInfo(id);
                m_SectionSceneInfos.Add(sceneInfo.AssetPathId,sceneInfo);
            }
        }

        public void SetHoldSceneId(List<int> ids){
            m_HoldStaticSceneIds.Clear();
            m_HoldStaticSceneIds.AddRange(ids);
        }

        public void SetGameSectionChange(Tuple<int,int> value){
            m_SectionChange = value;
        }



        public void EnterSceneAsset(int assetPathId){
            int count;

            Log.Debug($"EnterSceneAsset:{assetPathId} {m_EnterAssetCountDict.Count}");
            if(!m_EnterAssetCountDict.TryGetValue(assetPathId,out count)){
                m_EnterAssetCountDict.Add(assetPathId,1);
            }else{
                m_EnterAssetCountDict[assetPathId] = count +1;
            }

            StaticSceneInfo staticSceneInfo;
            if(m_SectionSceneInfos.TryGetValue(assetPathId,out staticSceneInfo)){
                if(staticSceneInfo.Id == m_SectionChange.Item1){
                    EventHelper.Fire(this,GameSectionChangeEventArgs.Create(m_SectionChange.Item2));
                }
            }

        }

        public void ExitSceneAsset(int assetPathId){
            int count;
            Log.Debug($"ExitSceneAsset:{assetPathId},{m_EnterAssetCountDict.Count}");
            if(m_EnterAssetCountDict.TryGetValue(assetPathId,out count)){
                count -=1;
                if(count <= 0){
                    m_EnterAssetCountDict.Remove(assetPathId);
                }else{
                    m_EnterAssetCountDict[assetPathId] = count;
                }
            }
        }

        public void  ShowStaticScene(int id){
            if(Loader == null){
                Loader = EntityLoader.Create(this);
            }

            //通过路径ID去判断是否被加载。用来在不同的章节下用了不用的静态场景ID,但是使用不同的加载Ids
            var sceneInfo = m_StaticSceneTable.GetStaticSceneInfo(id);
            var AssetPathId = sceneInfo.AssetPathId;
            if(m_LoadedSceneAssetDict.ContainsKey(AssetPathId)){
                return;
            }

             Log.Info($"ShowStaticScene:{id}");
       

            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if(m_LoadingAssetIds.Contains(AssetPathId)){
                return;
            }else{
                EntityStaticSceneData data = EntityStaticSceneData.Create(sceneInfo.EntityInfo,this);
                m_LoadingAssetIds.Add(AssetPathId);
                Loader.ShowEntity(EnumEntity.StaticScene,
                    (o)=>{
                        if(m_LoadingAssetIds.Contains(AssetPathId)){
                            m_LoadingAssetIds.Remove(AssetPathId);
                        }
                        m_LoadedSceneAssetDict.Add(AssetPathId,o.Logic as EntityStaticScene);   
                    },
                    data.EntityInfo,
                    data);
            }
        }

        public void UpdateNeedLoadDict(){
            NeedLoadDict.Clear();
            
            // 玩家进入对应的场景后。对应场景有相应的场景加载要求。
            foreach(var enterAssetId in  m_EnterAssetCountDict.Keys){
                StaticSceneInfo sceneInfo;
                if(m_SectionSceneInfos.TryGetValue(enterAssetId,out sceneInfo)){
                    foreach(var id in sceneInfo.LoadSceneIds){
                        if(!NeedLoadDict.ContainsKey(id)){
                            var loadSceneInfo = m_StaticSceneTable.GetStaticSceneInfo(id);
                            NeedLoadDict.Add(id,loadSceneInfo.AssetPathId);
                        }
                    }
                    if(!NeedLoadDict.ContainsKey(sceneInfo.Id)){
                        NeedLoadDict.Add(sceneInfo.Id,sceneInfo.AssetPathId);
                    }
                }
            }


            //章节服务会设置常驻的场景ID。用来打开该场景下通用的设置。
            foreach(var keepSceneId in m_HoldStaticSceneIds){
                StaticSceneInfo sceneInfo = m_StaticSceneTable.GetStaticSceneInfo(keepSceneId);
                    if(!NeedLoadDict.ContainsKey(sceneInfo.Id)){
                        NeedLoadDict.Add(sceneInfo.Id,sceneInfo.AssetPathId);
                    }
            }

            // Log.Info($"UpdateNeedLoadIds:{NeedLoadDict.Keys.ToList().ToItemString()} EnterSceneId:{m_EnterAssetCountDict.Keys.ToList().ToItemString()}");
        }

        public void UpdateAutoLoad(){
           if(AutoLoad){
                foreach(var loadId in NeedLoadDict.Keys){
                    ShowStaticScene(loadId);
                }
            }
        }

        public void UpdateAutoRelease(){
            if(AutoRelease){
                List<int> removeScene = new List<int>();
                foreach(var item in  m_LoadedSceneAssetDict){
                    if(!NeedLoadDict.ContainsValue(item.Key) && !m_EnterAssetCountDict.ContainsKey(item.Key)){
                        if(Loader.GetEntity(item.Value.Id)){
                            Loader.HideEntity(item.Value.Id);  
                            Log.Info($"HideEntity:{item.Key}");
                        }
                        removeScene.Add(item.Key);
                    }
                }

                foreach(var id in removeScene){
                    m_LoadedSceneAssetDict.Remove(id);
                }
            }

        }

        public void DumpStr(){
             Log.Info("load :{}");
        }

        public override void DoUpdate(float elapseSeconds, float realElapseSeconds){
            if(m_EnterAssetCountDict.Count == 0){
                return;
            }

            UpdateNeedLoadDict();
            UpdateAutoLoad();
            UpdateAutoRelease();

           




        }
        
    }
}