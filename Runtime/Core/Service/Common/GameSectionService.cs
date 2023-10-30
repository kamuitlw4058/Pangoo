using Pangoo;
using System.Collections;
using System.Collections.Generic;

using System;
using GameFramework;
using UnityGameFramework.Runtime;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;

namespace Pangoo.Core.Services
{
    public class GameSectionService : BaseService
    {
        public override int Priority => 10;

        ExcelTableService m_ExcelTableService;
        StaticSceneService m_StaticSceneService;
        DynamicObjectService m_DynamicObjectService;

        GameMainConfigService m_GameMainConfigService;

        GameSectionTable m_GameSectionTable;

        InstructionTable m_InstructionTable;


        public int LatestId = -1;


        protected override void DoAwake()
        {
            base.DoAwake();
            m_StaticSceneService = Parent.GetService<StaticSceneService>();
            m_ExcelTableService = Parent.GetService<ExcelTableService>();
            m_StaticSceneService = Parent.GetService<StaticSceneService>();
            m_GameMainConfigService = Parent.GetService<GameMainConfigService>();

            m_DynamicObjectService = Parent.GetService<DynamicObjectService>();

            Event.Subscribe(GameSectionChangeEventArgs.EventId, OnGameSectionChangeEvent);

        }

        void OnGameSectionChangeEvent(object sender, GameFrameworkEventArgs e)
        {
            var args = e as GameSectionChangeEventArgs;
            if (args.GameSectionId != 0)
            {
                SetGameSection(args.GameSectionId);
            }
        }

        protected override void DoStart()
        {
            Debug.Log($"DoStart GameSectionService");
            m_GameSectionTable = m_ExcelTableService.GetExcelTable<GameSectionTable>();
            m_InstructionTable = m_ExcelTableService.GetExcelTable<InstructionTable>();
            m_StaticSceneService.OnInitSceneLoaded += OnInitSceneLoaded;
            var enterGameSectionId = m_GameMainConfigService.GetGameMainConfig().EnterGameSectionId;
            SetGameSection(enterGameSectionId);
        }

        void OnInitSceneLoaded()
        {
            Debug.Log($"OnInitSceneLoaded");
            var GameSection = m_GameSectionTable.GetGameSectionRow(LatestId);
            var instructionIds = GameSection.InitedInstructionIds.ToListInt();
            if (instructionIds.Count > 0)
            {

                var instructions = InstructionList.BuildInstructionList(instructionIds, m_InstructionTable);
                var args = new Args();
                args.Main = Parent as MainService;
                instructions.Start(args);
            }
        }

        public void SetGameSection(int id)
        {
            if (id <= 0)
            {
                return;
            }

            if (LatestId != id)
            {
                LatestId = id;

                var GameSection = m_GameSectionTable.GetGameSectionRow(LatestId);
                if (GameSection == null)
                {
                    Debug.LogError($"GameSection is null:{GameSection}");
                }

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
                m_StaticSceneService.SetGameScetion(
                    GameSection.DynamicSceneIds.ToListInt(),
                    GameSection.KeepSceneIds.ToListInt(),
                    GameSection.InitSceneIds.ToListInt()
                    );

                m_DynamicObjectService.HideAllLoaded();
                var doIds = GameSection.DynamicObjectIds.ToListInt();
                foreach (var doId in doIds)
                {
                    m_DynamicObjectService.ShowDynamicObject(doId);
                }

                Log.Info($"Update Static Scene:{GameSection.Id} KeepSceneIds:{GameSection.KeepSceneIds} DynamicSceneIds:{GameSection.DynamicSceneIds}");
            }
        }


        protected override void DoDestroy()
        {
            if (m_StaticSceneService != null)
            {
                m_StaticSceneService.OnInitSceneLoaded -= OnInitSceneLoaded;

            }
            base.DoDestroy();
        }


    }
}