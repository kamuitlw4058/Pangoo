using Pangoo;
using System.Collections;
using System.Collections.Generic;

using System;
using GameFramework;
using UnityGameFramework.Runtime;

namespace Pangoo.Service
{
    public class GameSectionService : ServiceBase
    {
        ExcelTableService m_ExcelTableService;
        StaticSceneService m_StaticSceneService;

        GameSectionTable m_GameSectionTable;

        EventHelper m_EventHelper;

        int  CurrentId =1;

        int LatestId =-1;


        public override void DoAwake(IServiceContainer services){
            m_StaticSceneService = services.GetService<StaticSceneService>();
            m_ExcelTableService = services.GetService<ExcelTableService>();
            m_EventHelper = EventHelper.Create(this);
            m_EventHelper.Subscribe(GameSectionChangeEventArgs.EventId, OnGameSectionChangeEvent);
        }

        void OnGameSectionChangeEvent(object sender,GameFrameworkEventArgs e){

        }

        public override void DoStart(){
            m_GameSectionTable = m_ExcelTableService.GetExcelTable<GameSectionTable>();
            UpdateStaticScene(true);
        }

        void UpdateStaticScene(bool isStart = false){
            if(CurrentId != LatestId){
                var  GameSection = m_GameSectionTable.GetGameSectionRow(CurrentId);
                m_StaticSceneService.SetHoldSceneId(GameSection.KeepSceneIds.ToListInt());
                m_StaticSceneService.SetSectionIds(GameSection.DynamicSceneIds.ToListInt());
                var ids = GameSection.FirstDynamicSceneIds.ToListInt();
                if(isStart){
                    foreach( var id in ids){
                        m_StaticSceneService.ShowStaticScene(id);
                    }
                }
                  Log.Info($"Update Static Scene:{GameSection.Id} KeepSceneIds:{GameSection.KeepSceneIds} DynamicSceneIds:{GameSection.DynamicSceneIds}");
            }
        }

        public override void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {
            UpdateStaticScene();
        }
        
        public override void DoDestroy(){
            
            if(m_EventHelper != null){
                m_EventHelper.UnSubscribeAll();
                ReferencePool.Release(m_EventHelper);
            }
        }
        

        
    }
}