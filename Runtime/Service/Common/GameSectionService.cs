using Pangoo;
using System.Collections;
using System.Collections.Generic;

using System;
using GameFramework;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace Pangoo.Service
{
    public class GameSectionService : ServiceBase
    {
        public override int Priority => 10;

        ExcelTableService m_ExcelTableService;
        StaticSceneService m_StaticSceneService;
        DynamicObjectService m_DynamicObjectService;

        GameSectionTable m_GameSectionTable;


        public int CurrentId = 1;

        public int LatestId = -1;


        public override void DoAwake(IServiceContainer services)
        {
            base.DoAwake(services);
            m_StaticSceneService = services.GetService<StaticSceneService>();
            m_ExcelTableService = services.GetService<ExcelTableService>();
            m_DynamicObjectService = services.GetService<DynamicObjectService>();

            EventHelper.Subscribe(GameSectionChangeEventArgs.EventId, OnGameSectionChangeEvent);

        }

        void OnGameSectionChangeEvent(object sender, GameFrameworkEventArgs e)
        {
            var args = e as GameSectionChangeEventArgs;
            if (args.GameSectionId != 0)
            {
                CurrentId = args.GameSectionId;
            }
        }

        public override void DoStart()
        {
            Debug.Log($"DoStart GameSectionService");
            m_GameSectionTable = m_ExcelTableService.GetExcelTable<GameSectionTable>();
            UpdateStaticScene(true);
        }

        void UpdateStaticScene(bool isStart = false)
        {
            if (CurrentId != LatestId)
            {
                LatestId = CurrentId;
                var GameSection = m_GameSectionTable.GetGameSectionRow(CurrentId);
                m_StaticSceneService.SetHoldSceneId(GameSection.KeepSceneIds.ToListInt());
                m_StaticSceneService.SetSectionIds(GameSection.DynamicSceneIds.ToListInt());
                Tuple<int, int> sectionChange = new Tuple<int, int>(0, 0);
                if (!string.IsNullOrEmpty(GameSection.SectionJumpByScene))
                {
                    var itemList = GameSection.SectionJumpByScene.ToListInt("#");
                    if (itemList.Count == 2)
                    {
                        sectionChange = new Tuple<int, int>(itemList[0], itemList[1]);
                    }
                }
                m_StaticSceneService.SetGameSectionChange(sectionChange);

                var ids = GameSection.FirstDynamicSceneIds.ToListInt();
                if (isStart)
                {
                    foreach (var id in ids)
                    {
                        m_StaticSceneService.ShowStaticScene(id);
                    }
                }

                var doIds = GameSection.DynamicObjectIds.ToListInt();
                foreach (var doId in doIds)
                {
                    m_DynamicObjectService.ShowDynamicObject(doId);
                }

                Log.Info($"Update Static Scene:{GameSection.Id} KeepSceneIds:{GameSection.KeepSceneIds} DynamicSceneIds:{GameSection.DynamicSceneIds}");
            }
        }

        public override void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {
            UpdateStaticScene();
        }


    }
}