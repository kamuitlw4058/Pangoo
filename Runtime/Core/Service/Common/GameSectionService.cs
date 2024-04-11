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
using LitJson;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Services
{
    public class GameSectionService : MainSubService
    {
        public override string ServiceName => "GameSectionService";
        public override int Priority => 10;



        public string LatestUuid = null;

        public bool IsGameSectionLoaded;

        public bool CharacterShowed;



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


        bool CheckGameSectionLoadedCompleted()
        {
            if (LatestGameSectionRow == null) return false;

            bool dynamicObjectLoaded = DynamicObjectSrv.CheckGameSectionLoaded;
            var sceneLoaded = StaticSceneSrv.CheckGameSectionScenesLoaded();
            Log($"CheckGameSectionLoadedCompleted IsSceneLoaded:{sceneLoaded} IsDynamicObjectLoaded:{dynamicObjectLoaded}");
            if (sceneLoaded && dynamicObjectLoaded)
            {
                return true;
            }

            return false;
        }

        public IGameSectionRow LatestGameSectionRow
        {
            get
            {
                return MetaTableSrv.GetGameSectionByUuid(LatestUuid);
            }
        }

        Dictionary<int, CharacterBornInfo> m_BornDict;

        Dictionary<int, CharacterBornInfo> BornDict
        {
            get
            {
                if (m_BornDict == null)
                {
                    if (LatestGameSectionRow != null)
                    {
                        m_BornDict = JsonMapper.ToObject<Dictionary<int, CharacterBornInfo>>(LatestGameSectionRow.PlayerBirthPlaceList);
                    }
                }

                return m_BornDict;
            }
        }

        int BornCount
        {
            get
            {
                var bornDict = BornDict;
                if (bornDict == null)
                {
                    return 0;
                }

                return bornDict.Count;
            }
        }



        bool CheckGameSectionLoadedWithPlayerCompleted()
        {
            if (LatestGameSectionRow == null) return false;

            bool dynamicObjectLoaded = DynamicObjectSrv.CheckGameSectionLoaded;
            var sceneLoaded = StaticSceneSrv.CheckGameSectionScenesLoaded();
            Log($"CheckGameSectionLoadedCompleted IsSceneLoaded:{sceneLoaded} IsDynamicObjectLoaded:{dynamicObjectLoaded}");
            if (sceneLoaded && dynamicObjectLoaded)
            {
                if (BornCount == 0)
                {
                    return true;
                }

                var state = 0;
                if (!LatestGameSectionRow.StateVariableUuid.IsNullOrWhiteSpace())
                {
                    state = RuntimeDataSrv.GetVariable<int>(LatestGameSectionRow.StateVariableUuid);
                }

                var bornDict = BornDict;
                if (bornDict != null)
                {
                    if (bornDict.TryGetValue(state, out CharacterBornInfo val))
                    {
                        var entity = CharacterSrv.GetLoadedEntity(val.PlayerUuid);
                        if (entity != null)
                        {
                            return true;
                        }

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }



                return true;
            }

            return false;
        }

        void RunLoadedInstructions()
        {
            if (LatestGameSectionRow == null) return;
#if UNITY_EDITOR
            var editorInstructionUuids = LatestGameSectionRow.EditorInitedInstructionUuids.ToSplitList<string>();
            if (editorInstructionUuids.Count > 0)
            {
                var instructions = InstructionList.BuildInstructionList(editorInstructionUuids, MetaTableSrv.GetInstructionByUuid);
                var args = new Args();
                args.Main = Parent as MainService;
                instructions.Start(args);
            }

#endif
            var instructionUuids = LatestGameSectionRow.InitedInstructionUuids.ToSplitList<string>();
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
                m_BornDict = null;
                CharacterShowed = false;

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


        [ShowInInspector]
        public int InitState
        {
            get
            {
                var state = 0;
                var StateVariableUuid = string.Empty;
                if (!LatestGameSectionRow.StateVariableUuid.IsNullOrWhiteSpace() && !LatestGameSectionRow.StateVariableUuid.Equals(ConstString.Default))
                {
                    StateVariableUuid = LatestGameSectionRow.StateVariableUuid;
                }
                else
                {
                    StateVariableUuid = GameMainConfigSrv.GameMainConfig.GameSectionInitStateVariableUuid;
                }


                state = RuntimeDataSrv.GetGameSectionVariable<int>(LatestUuid, StateVariableUuid);
                Log($"GameSection:{LatestUuid}, StateVariableUuid:{StateVariableUuid}, val:{state}");
                return state;
            }
        }


        protected override void DoUpdate()
        {
            if (LatestUuid.IsNullOrWhiteSpace() || IsGameSectionLoaded)
            {
                return;
            }


            var LoadedWithoutPlayer = CheckGameSectionLoadedCompleted();
            if (LoadedWithoutPlayer)
            {
                var Loaded = CheckGameSectionLoadedWithPlayerCompleted();
                if (Loaded)
                {
                    var bornDict = BornDict;
                    Log($"Scene DynamicObject Loaded Player Complete:{bornDict}");
                    if (bornDict != null)
                    {
                        if (bornDict.TryGetValue(InitState, out CharacterBornInfo val))
                        {
                            var entity = CharacterSrv.GetLoadedEntity(val.PlayerUuid);
                            if (entity != null && val.ForceMove)
                            {
                                Log($"Loaded. Set Character:{val.PlayerUuid}, State:{InitState} Post:{val.Pose}");
                                entity.character.SetPose(val.Pose);
                            }
                        }
                    }

                    RunLoadedInstructions();
                    IsGameSectionLoaded = true;
                }
                else
                {
                    Log($"Scene DynamicObject Loaded,:{CharacterShowed} ");
                    if (!CharacterShowed)
                    {
                        var bornDict = BornDict;
                        if (bornDict != null)
                        {
                            if (bornDict.TryGetValue(InitState, out CharacterBornInfo val))
                            {
                                Log($"Loaded. Try Show Character:{val.PlayerUuid}, State:{InitState}");
                                CharacterSrv.ShowCharacter(val.PlayerUuid, val.Pose.Position, val.Pose.Rotation);
                            }

                        }
                        CharacterShowed = true;

                    }


                }

            }
        }

    }
}