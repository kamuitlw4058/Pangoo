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

        public bool IsGameSectionLoaded;


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

            if (GameMainConfigSrv.GetGameMainConfig().SkipMainMenu)
            {
                SetGameSection(firstGameSection: true);
            }
        }


        bool CheckGameSectionLoadedCompleted(IGameSectionRow row)
        {
            bool dynamicObjectLoaded = DynamicObjectSrv.CheckGameSectionLoaded;
            var sceneLoaded = StaticSceneSrv.CheckGameSectionScenesLoaded();
            Log($"CheckGameSectionLoadedCompleted IsSceneLoaded:{sceneLoaded} IsDynamicObjectLoaded:{dynamicObjectLoaded}");
            if (sceneLoaded && dynamicObjectLoaded)
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


        public void SetGameSection(string uuid = null, bool firstGameSection = false, Action OnFinishLoad = null)
        {
            Log($"SetGameSection is :{uuid.ToShortUuid()}");
            if (uuid.IsNullOrWhiteSpace())
            {
                var CurrentGameSection = RuntimeDataSrv.GetVariable<string>(GameMainConfigSrv.GetGameMainConfig().CurrentGameSectionVariableUuid);
                if (CurrentGameSection.IsNullOrWhiteSpace())
                {
                    CurrentGameSection = GameMainConfigSrv.GetGameMainConfig().EnterGameSectionUuid;
                }

                if (!CurrentGameSection.IsNullOrWhiteSpace())
                {
                    uuid = CurrentGameSection;
                }
            }
            Log($"Apply Game Section is :{uuid.ToShortUuid()}");


            if (LatestUuid != uuid)
            {
                IsGameSectionLoaded = false;
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
                StaticSceneSrv.SetGameSectionScenes(GameSection.SceneUuids.ToSplitList<string>());

                DynamicObjectSrv.SetGameScetion(GameSection.DynamicObjectUuids.ToSplitList<string>());


                Log($"Update Static Scene:{GameSection.UuidShort} SceneUuids:{GameSection.SceneUuids}");
            }
        }
        protected override void DoUpdate()
        {
            if (LatestUuid.IsNullOrWhiteSpace() || IsGameSectionLoaded)
            {
                return;
            }

            var GameSection = MetaTableSrv.GetGameSectionByUuid(LatestUuid);
            var Loaded = CheckGameSectionLoadedCompleted(GameSection);
            if (Loaded)
            {
                RunLoadedInstructions(GameSection);
                IsGameSectionLoaded = true;
            }
        }

    }
}