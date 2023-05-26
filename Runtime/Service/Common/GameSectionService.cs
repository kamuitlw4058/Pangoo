using Pangoo;
using System.Collections;
using System.Collections.Generic;

using System;
using GameFramework;

namespace Pangoo.Service
{
    public class GameSectionService : ServiceBase
    {
        ExcelTableService m_ExcelTableService;
        StaticSceneService m_StaticSceneService;

        GameSectionTable m_GameSectionTable;

        EventHelper m_EventHelper;

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
            // m_StaticSceneTable = m_ExcelTableService.GetExcelTable<StaticSceneTable>();

        }
        

        public override void DoDestroy(){
            
            if(m_EventHelper != null){
                m_EventHelper.UnSubscribeAll();
                ReferencePool.Release(m_EventHelper);
            }
        }
        

        
    }
}