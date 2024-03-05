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
        public override string ServiceName => "GameSectionService";
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

            StaticSceneSrv.OnInitSceneLoaded += CheckGameSectionLoaded;
            DynamicObjectSrv.OnGameSectionDynamicObjectLoaded += CheckGameSectionLoaded;

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


        void CheckGameSectionLoaded()
        {
            var GameSection = MetaTableSrv.GetGameSectionByUuid(LatestUuid);
            Log($"GameSection Loaded {CheckGameSectionLoadedCompleted(GameSection)}");
            if (CheckGameSectionLoadedCompleted(GameSection))
            {
                RunLoadedInstructions(GameSection);
            }
        }

        public void SetGameSection(string uuid = null, bool firstGameSection = false, Action OnFinishLoad = null)
        {
            Log($"SetGameSection is :{uuid.ToShortUuid()}");
            if (uuid.IsNullOrWhiteSpace())
            {
                // LogError($"SetGameSection Failed id <= 0:{uuid}");

                var CurrentGameSection = RuntimeDataSrv.GetVariable<string>(GameMainConfigSrv.GetGameMainConfig().CurrentGameSectionVariableUuid);
                if (CurrentGameSection.IsNullOrWhiteSpace())
                {
                    CurrentGameSection = GameMainConfigSrv.GetGameMainConfig().EnterGameSectionUuid;
                }

                if (!CurrentGameSection.IsNullOrWhiteSpace())
                {
                    uuid = CurrentGameSection;
                    // SetGameSection(CurrentGameSection, firstGameSection: true);
                }
                // return;
            }


            if (LatestUuid != uuid)
            {
                LatestUuid = uuid;

                var GameSection = MetaTableSrv.GetGameSectionByUuid(LatestUuid);
                if (GameSection == null)
                {
                    LogError($"GameSection is null:{GameSection}");
                }
                RuntimeDataSrv.SetVariable<string>(GameMainConfigSrv.GetGameMainConfig().CurrentGameSectionVariableUuid, uuid);
                if (!firstGameSection)
                {
                    SaveLoadSrv.Save();
                }

                StaticSceneSrv.SetGameScetion(
                    GameSection.DynamicSceneUuids.ToSplitList<string>(),
                    GameSection.KeepSceneUuids.ToSplitList<string>(),
                    GameSection.InitSceneUuids.ToSplitList<string>()
                    );

                DynamicObjectSrv.SetGameScetion(GameSection.DynamicObjectUuids.ToSplitList<string>());


                Log($"Update Static Scene:{GameSection.UuidShort} KeepSceneUuids:{GameSection.KeepSceneUuids} DynamicSceneUuids:{GameSection.DynamicSceneUuids} InitSceneUuids:{GameSection.InitSceneUuids}");
            }
        }


        protected override void DoDestroy()
        {
            if (StaticSceneSrv != null)
            {
                StaticSceneSrv.OnInitSceneLoaded -= CheckGameSectionLoaded;
            }

            if (DynamicObjectSrv != null)
            {
                DynamicObjectSrv.OnGameSectionDynamicObjectLoaded -= CheckGameSectionLoaded;
            }
            base.DoDestroy();
        }


    }
}