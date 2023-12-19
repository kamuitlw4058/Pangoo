using Pangoo;
using System.Collections;
using System.Collections.Generic;

using System;
using GameFramework;
using UnityGameFramework.Runtime;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;

using Pangoo.Common;

namespace Pangoo.Core.Services
{
    public class GameSectionService : MainSubService
    {
        public override string ServiceName => "GameSection";
        public override int Priority => 10;



        GameSectionTable m_GameSectionTable;

        InstructionTable m_InstructionTable;


        public int LatestId = -1;


        protected override void DoAwake()
        {
            base.DoAwake();
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
            Log("DoStart");
            m_GameSectionTable = ExcelTableSrv.GetExcelTable<GameSectionTable>();
            m_InstructionTable = ExcelTableSrv.GetExcelTable<InstructionTable>();

            StaticSceneSrv.OnInitSceneLoaded += OnInitSceneLoaded;
            var enterGameSectionId = GameMainConfigSrv.GetGameMainConfig().EnterGameSectionId;
            SetGameSection(enterGameSectionId);
        }

        bool CheckDynamicObjectLoaded(GameSectionTable.GameSectionRow row)
        {
            var doIds = row.DynamicObjectIds.ToSplitList<int>();
            foreach (var doId in doIds)
            {
                if (DynamicObjectSrv.GetLoadedEntity(doId) == null)
                {
                    return false;
                }
            }
            return true;
        }


        bool CheckGameSectionLoadedCompleted(GameSectionTable.GameSectionRow row)
        {
            bool IsDynamicObjectLoaded = CheckDynamicObjectLoaded(row);
            Log($"CheckGameSectionLoadedCompleted IsSceneLoaded:{StaticSceneSrv.SectionInited} IsDynamicObjectLoaded:{IsDynamicObjectLoaded}");
            if (StaticSceneSrv.SectionInited && CheckDynamicObjectLoaded(row))
            {
                return true;
            }

            return false;
        }

        void RunLoadedInstructions(GameSectionTable.GameSectionRow GameSectionRow)
        {
#if UNITY_EDITOR
            var editorInstructionIds = GameSectionRow.EditorInitedInstructionIds.ToSplitList<int>();
            if (editorInstructionIds.Count > 0)
            {
                var instructions = InstructionList.BuildInstructionList(editorInstructionIds, m_InstructionTable);
                var args = new Args();
                args.Main = Parent as MainService;
                instructions.Start(args);
            }

#endif


            var instructionIds = GameSectionRow.InitedInstructionIds.ToSplitList<int>();
            if (instructionIds.Count > 0)
            {

                var instructions = InstructionList.BuildInstructionList(instructionIds, m_InstructionTable);
                var args = new Args();
                args.Main = Parent as MainService;
                instructions.Start(args);
            }
        }




        void OnInitSceneLoaded()
        {
            var GameSection = m_GameSectionTable.GetGameSectionRow(LatestId);
            if (CheckGameSectionLoadedCompleted(GameSection))
            {
                RunLoadedInstructions(GameSection);
            }
        }

        public void SetGameSection(int id)
        {
            Log($"SetGameSection is :{id}");
            if (id <= 0)
            {
                LogError($"SetGameSection Failed id <= 0:{id}");
                return;
            }


            if (LatestId != id)
            {
                LatestId = id;

                var GameSection = m_GameSectionTable.GetGameSectionRow(LatestId);
                if (GameSection == null)
                {
                    LogError($"GameSection is null:{GameSection}");
                }

                Tuple<int, int> sectionChange = new Tuple<int, int>(0, 0);
                if (!string.IsNullOrEmpty(GameSection.SectionJumpByScene))
                {
                    var itemList = GameSection.SectionJumpByScene.ToSplitList<int>("#");
                    if (itemList.Count == 2)
                    {
                        sectionChange = new Tuple<int, int>(itemList[0], itemList[1]);
                    }
                }

                StaticSceneSrv.SetGameSectionChange(sectionChange);
                StaticSceneSrv.SetGameScetion(
                    GameSection.DynamicSceneIds.ToSplitList<int>(),
                    GameSection.KeepSceneIds.ToSplitList<int>(),
                    GameSection.InitSceneIds.ToSplitList<int>()
                    );

                DynamicObjectSrv.HideAllLoaded();
                var doIds = GameSection.DynamicObjectIds.ToSplitList<int>();
                foreach (var doId in doIds)
                {
                    DynamicObjectSrv.ShowDynamicObject(doId, (dynamicObjectId) =>
                    {
                        Log($"Loaded DynamicObject Finish:{dynamicObjectId}");
                        if (CheckGameSectionLoadedCompleted(GameSection))
                        {
                            RunLoadedInstructions(GameSection);
                        }
                    });
                }

                Log($"Update Static Scene:{GameSection.Id} KeepSceneIds:{GameSection.KeepSceneIds} DynamicSceneIds:{GameSection.DynamicSceneIds}");
            }
        }


        protected override void DoDestroy()
        {
            if (StaticSceneSrv != null)
            {
                StaticSceneSrv.OnInitSceneLoaded -= OnInitSceneLoaded;

            }
            base.DoDestroy();
        }


    }
}