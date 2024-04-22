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


        [ShowInInspector]
        public string CurrentUuid = null;

        [ShowInInspector]
        public string TargetUuid = null;

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
                SetFirstGameSection();
                string uuid = null;
                var CurrentGameSection = RuntimeDataSrv.GetVariable<string>(GameMainConfigSrv.GetGameMainConfig().CurrentGameSectionVariableUuid);
                if (CurrentGameSection.IsNullOrWhiteSpace())
                {
                    CurrentGameSection = GameMainConfigSrv.GetGameMainConfig().EnterGameSectionUuid;
                }

                if (!CurrentGameSection.IsNullOrWhiteSpace())
                {
                    uuid = CurrentGameSection;
                }
                SetGameSection(uuid, firstGameSection: true);
            }
        }


        public IGameSectionRow LatestGameSectionRow
        {
            get
            {
                if (CurrentUuid == null)
                {
                    return null;
                }

                return MetaTableSrv.GetGameSectionByUuid(CurrentUuid);
            }
        }

        public IGameSectionRow GetGameSectionByUuid(string uuid)
        {
            return MetaTableSrv.GetGameSectionByUuid(CurrentUuid);
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

            // var sceneLoaded = StaticSceneSrv.CheckGameSectionScenesLoaded();t
            // Log($"CheckGameSectionLoadedCompleted IsSceneLoaded:{sceneLoaded} IsDynamicObjectLoaded:{dynamicObjectLoaded}");
            if (StaticSceneLoaded && DynamicObjectLoaded)
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

        public void SetFirstGameSection()
        {
            var CurrentGameSection = RuntimeDataSrv.GetVariable<string>(GameMainConfigSrv.GetGameMainConfig().CurrentGameSectionVariableUuid);
            if (CurrentGameSection.IsNullOrWhiteSpace())
            {
                CurrentGameSection = GameMainConfigSrv.GetGameMainConfig().EnterGameSectionUuid;
            }

            TargetUuid = CurrentGameSection;
        }


        public void SetGameSection(string uuid = null, bool firstGameSection = false, Action OnFinishLoad = null)
        {
            TargetUuid = uuid;
            IsGameSectionLoaded = false;
            // Log($"SetGameSection is :{uuid.ToShortUuid()}");
            // if (uuid.IsNullOrWhiteSpace())
            // {
            //     var CurrentGameSection = RuntimeDataSrv.GetVariable<string>(GameMainConfigSrv.GetGameMainConfig().CurrentGameSectionVariableUuid);
            //     if (CurrentGameSection.IsNullOrWhiteSpace())
            //     {
            //         CurrentGameSection = GameMainConfigSrv.GetGameMainConfig().EnterGameSectionUuid;
            //     }

            //     if (!CurrentGameSection.IsNullOrWhiteSpace())
            //     {
            //         uuid = CurrentGameSection;
            //     }
            // }
            // Log($"Apply Game Section is :{uuid.ToShortUuid()}");

            // if (CurrentUuid != uuid)
            // {
            //     IsGameSectionLoaded = false;
            //     CurrentUuid = uuid;
            //     m_BornDict = null;
            //     CharacterShowed = false;

            //     var GameSection = MetaTableSrv.GetGameSectionByUuid(CurrentUuid);
            //     if (GameSection == null)
            //     {
            //         LogError($"GameSection is null:{GameSection}");
            //     }
            //     RuntimeDataSrv.SetVariable<string>(GameMainConfigSrv.GetGameMainConfig().CurrentGameSectionVariableUuid, uuid);
            //     if (!firstGameSection)
            //     {
            //         SaveLoadSrv.Save();
            //     }
            //     // StaticSceneSrv.SetGameSectionScenes(GameSection.SceneUuids.ToSplitList<string>());

            //     DynamicObjectSrv.SetGameScetion(GameSection.DynamicObjectUuids.ToSplitList<string>());


            //     Log($"Update Static Scene:{GameSection.UuidShort} SceneUuids:{GameSection.SceneUuids}");
            // }
        }


        [ShowInInspector]
        public int InitState
        {
            get
            {
                var StateVariableUuid = string.Empty;

                if (LatestGameSectionRow == null) return 0;

                if (!LatestGameSectionRow.StateVariableUuid.IsNullOrWhiteSpace() && !LatestGameSectionRow.StateVariableUuid.Equals(ConstString.Default))
                {
                    StateVariableUuid = LatestGameSectionRow.StateVariableUuid;
                }
                else
                {
                    StateVariableUuid = GameMainConfigSrv.GameMainConfig.GameSectionInitStateVariableUuid;
                }

                return RuntimeDataSrv.GetGameSectionVariable<int>(CurrentUuid, StateVariableUuid);
            }
        }


        [ShowInInspector]
        public bool StaticSceneLoaded
        {
            get
            {
                var SceneUuids = LatestGameSectionRow?.SceneUuids.ToSplitList<string>();
                if (SceneUuids == null)
                {
                    return true;
                }

                var finished = true;
                foreach (var uuid in SceneUuids)
                {
                    if (!StaticSceneSrv.IsEntityLoadedData(uuid))
                    {
                        finished = false;
                    }
                }

                return finished;
            }

        }

        [ShowInInspector]
        public bool DynamicObjectLoaded
        {
            get
            {
                var Uuids = LatestGameSectionRow?.DynamicObjectUuids.ToSplitList<string>();
                if (Uuids == null)
                {
                    return true;
                }

                var finished = true;
                foreach (var uuid in Uuids)
                {
                    if (!DynamicObjectSrv.IsEntityLoadedData(uuid))
                    {
                        finished = false;
                    }
                }

                return finished;
            }

        }

        [ShowInInspector]
        public bool PlayerBornLoaded
        {

            get
            {
                if (BornCount == 0)
                {
                    return true;
                }

                var playerUuid = GetBornPlayerUuid;
                if (playerUuid != null)
                {

                    var entity = CharacterSrv.GetLoadedEntity(playerUuid);
                    if (entity != null)
                    {
                        return true;
                    }
                    return false;

                }

                return true;

            }
        }

        string GetBornPlayerUuid
        {
            get
            {
                var state = 0;
                if (LatestGameSectionRow == null)
                {
                    return null;
                }


                if (!LatestGameSectionRow.StateVariableUuid.IsNullOrWhiteSpace())
                {
                    state = RuntimeDataSrv.GetVariable<int>(LatestGameSectionRow.StateVariableUuid);
                }

                var bornDict = BornDict;
                if (bornDict != null)
                {
                    if (bornDict.TryGetValue(InitState, out CharacterBornInfo val))
                    {
                        return val.PlayerUuid;
                    }
                }
                return null;
            }
        }





        protected override void DoUpdate()
        {
            if (!TargetUuid.NullOrWhiteSpaceEquals(CurrentUuid))
            {
                List<string> CurrentSceneUuids = null;
                List<string> CurrentDOUuids = null;
                if (!CurrentUuid.IsNullOrWhiteSpace())
                {
                    CurrentSceneUuids = LatestGameSectionRow.SceneUuids.ToSplitList<string>();
                    CurrentDOUuids = LatestGameSectionRow.DynamicObjectUuids.ToSplitList<string>();
                }



                List<string> TargetSceneUuids = null;
                List<string> TargetDOUuids = null;

                if (!TargetUuid.IsNullOrWhiteSpace())
                {
                    CurrentUuid = TargetUuid;
                    TargetSceneUuids = LatestGameSectionRow.SceneUuids.ToSplitList<string>();
                    TargetDOUuids = LatestGameSectionRow.DynamicObjectUuids.ToSplitList<string>();
                }

                var DiffScenes = StaticSceneSrv.DiffTargetScenes(TargetSceneUuids, CurrentSceneUuids);
                if (DiffScenes != null)
                {
                    for (int i = 0; i < DiffScenes.Count; i++)
                    {
                        var diffScene = DiffScenes[i];
                        StaticSceneSrv.HideEntity(diffScene);
                    }
                }

                var DiffDynamicObjects = DynamicObjectSrv.DiffItems(TargetDOUuids, CurrentDOUuids);
                if (DiffDynamicObjects != null)
                {
                    for (int i = 0; i < DiffDynamicObjects.Count; i++)
                    {
                        var diffScene = DiffDynamicObjects[i];
                        DynamicObjectSrv.HideEntity(diffScene, ServiceName);
                    }
                }




                // IsGameSectionLoaded = false;
                // CurrentUuid = uuid;
                // m_BornDict = null;
                // CharacterShowed = false;

                // var GameSection = MetaTableSrv.GetGameSectionByUuid(CurrentUuid);
                // if (GameSection == null)
                // {
                //     LogError($"GameSection is null:{GameSection}");
                // }
                // RuntimeDataSrv.SetVariable<string>(GameMainConfigSrv.GetGameMainConfig().CurrentGameSectionVariableUuid, uuid);
                // if (!firstGameSection)
                // {
                //     SaveLoadSrv.Save();
                // }
                // StaticSceneSrv.SetGameSectionScenes(GameSection.SceneUuids.ToSplitList<string>());

                // DynamicObjectSrv.SetGameScetion(GameSection.DynamicObjectUuids.ToSplitList<string>());


                // Log($"Update Static Scene:{GameSection.UuidShort} SceneUuids:{GameSection.SceneUuids}");
                CurrentUuid = TargetUuid;
            }

            if (CurrentUuid.IsNullOrWhiteSpace() || IsGameSectionLoaded)
            {
                return;
            }


            var SceneUuids = LatestGameSectionRow.SceneUuids.ToSplitList<string>();
            foreach (var uuid in SceneUuids)
            {
                if (!StaticSceneSrv.IsEntityLoadedData(uuid))
                {
                    Debug.Log($"Show Scene:{uuid}");
                    StaticSceneSrv.ShowEntity(uuid);
                }
            }

            var DoUuids = LatestGameSectionRow.DynamicObjectUuids.ToSplitList<string>();
            foreach (var uuid in DoUuids)
            {
                if (!DynamicObjectSrv.IsEntityLoadedData(uuid))
                {
                    Debug.Log($"Show DynamicObject:{uuid}");
                    DynamicObjectSrv.ShowEntity(uuid, refName: ServiceName);
                }
            }

            if (StaticSceneLoaded && DynamicObjectLoaded)
            {
                if (PlayerBornLoaded)
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

                    var bornDict = BornDict;
                    if (bornDict != null)
                    {
                        if (bornDict.TryGetValue(InitState, out CharacterBornInfo val))
                        {
                            Log($"Loaded. Try Show Character:{val.PlayerUuid}, State:{InitState}");
                            CharacterSrv.ShowCharacter(val.PlayerUuid, val.Pose.Position, val.Pose.Rotation);
                        }

                    }
                }

            }
        }

    }
}