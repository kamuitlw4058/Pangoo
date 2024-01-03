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
using Pangoo.MetaTable;

namespace Pangoo.Core.Services
{
    public class GameSectionService : MainSubService
    {
        public override string ServiceName => "GameSection";
        public override int Priority => 10;



        public string LatestUuid = null;


        protected override void DoAwake()
        {
            base.DoAwake();
            Event.Subscribe(GameSectionChangeEventArgs.EventId, OnGameSectionChangeEvent);
        }

        void OnGameSectionChangeEvent(object sender, GameFrameworkEventArgs e)
        {
            var args = e as GameSectionChangeEventArgs;
            if (!args.GameSectionUuid.IsNullOrWhiteSpace())
            {
                SetGameSection(args.GameSectionUuid);
            }
        }

        protected override void DoStart()
        {
            Log("DoStart");

            StaticSceneSrv.OnInitSceneLoaded += OnInitSceneLoaded;
            SetGameSection(GameMainConfigSrv.GetGameMainConfig().EnterGameSectionUuid);
        }

        bool CheckDynamicObjectLoaded(IGameSectionRow row)
        {
            var uuids = row.DynamicObjectUuids.ToSplitList<string>();
            foreach (var dynamicObjectUuid in uuids)
            {
                if (DynamicObjectSrv.GetLoadedEntity(dynamicObjectUuid) == null)
                {
                    return false;
                }
            }
            return true;
        }


        bool CheckGameSectionLoadedCompleted(IGameSectionRow row)
        {
            bool IsDynamicObjectLoaded = CheckDynamicObjectLoaded(row);
            Log($"CheckGameSectionLoadedCompleted IsSceneLoaded:{StaticSceneSrv.SectionInited} IsDynamicObjectLoaded:{IsDynamicObjectLoaded}");
            if (StaticSceneSrv.SectionInited && CheckDynamicObjectLoaded(row))
            {
                return true;
            }

            return false;
        }

        void RunLoadedInstructions(IGameSectionRow GameSectionRow)
        {
#if UNITY_EDITOR
            var editorInstructionUuids = GameSectionRow.EditorInitedInstructionUuids.ToSplitList<string>();
            if (editorInstructionUuids.Count > 0)
            {
                var instructions = InstructionList.BuildInstructionList(editorInstructionUuids, MetaTableSrv.GetInstructionByUuid);
                var args = new Args();
                args.Main = Parent as MainService;
                instructions.Start(args);
            }

#endif


            var instructionUuids = GameSectionRow.InitedInstructionUuids.ToSplitList<string>();
            if (instructionUuids.Count > 0)
            {

                var instructions = InstructionList.BuildInstructionList(instructionUuids, MetaTableSrv.GetInstructionByUuid);
                var args = new Args();
                args.Main = Parent as MainService;
                instructions.Start(args);
            }
        }


        void OnInitSceneLoaded()
        {
            var GameSection = MetaTableSrv.GetGameSectionByUuid(LatestUuid);
            Log($"Loaded Scene {CheckGameSectionLoadedCompleted(GameSection)}");
            if (CheckGameSectionLoadedCompleted(GameSection))
            {
                RunLoadedInstructions(GameSection);
            }
        }

        public void SetGameSection(string uuid)
        {
            Log($"SetGameSection is :{uuid.ToShortUuid()}");
            if (uuid.IsNullOrWhiteSpace())
            {
                LogError($"SetGameSection Failed id <= 0:{uuid}");
                return;
            }


            if (LatestUuid != uuid)
            {
                LatestUuid = uuid;

                var GameSection = MetaTableSrv.GetGameSectionByUuid(LatestUuid);
                if (GameSection == null)
                {
                    LogError($"GameSection is null:{GameSection}");
                }

                // Tuple<string, string> sectionChange = null;
                // if (!string.IsNullOrEmpty(GameSection.SectionJumpByScene))
                // {
                //     var itemList = GameSection.SectionJumpByScene.ToSplitList<int>("#");
                //     if (itemList.Count == 2)
                //     {
                //         sectionChange = new Tuple<int, int>(itemList[0], itemList[1]);
                //     }
                // }

                // StaticSceneSrv.SetGameSectionChange(sectionChange);
                StaticSceneSrv.SetGameScetion(
                    GameSection.DynamicSceneUuids.ToSplitList<string>(),
                    GameSection.KeepSceneUuids.ToSplitList<string>(),
                    GameSection.InitSceneUuids.ToSplitList<string>()
                    );

                // DynamicObjectSrv.HideAllLoaded();
                var loadedUuids = DynamicObjectSrv.GetLoadedUuids();
                var doUuids = GameSection.DynamicObjectUuids.ToSplitList<string>();
                foreach (var doUuid in doUuids)
                {
                    if (loadedUuids.Contains(doUuid)) continue;

                    DynamicObjectSrv.ShowDynamicObject(doUuid, (dynamicObjectUuid) =>
                    {
                        Log($"Loaded DynamicObject Finish:[{dynamicObjectUuid.ToShortUuid()}]");
                        if (CheckGameSectionLoadedCompleted(GameSection))
                        {
                            RunLoadedInstructions(GameSection);
                        }
                    });
                }

                foreach (var loadedUuid in loadedUuids)
                {
                    if (!doUuids.Contains(loadedUuid))
                    {
                        DynamicObjectSrv.HideEntity(loadedUuid);
                    }
                }

                Log($"Update Static Scene:{GameSection.UuidShort} KeepSceneUuids:{GameSection.KeepSceneUuids} DynamicSceneUuids:{GameSection.DynamicSceneUuids} InitSceneUuids:{GameSection.InitSceneUuids}");
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