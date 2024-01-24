using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;


#if UNITY_EDITOR
using System.Linq;
#endif

namespace Pangoo
{

    [CreateAssetMenu(fileName = "GameMainConfig", menuName = "Pangoo/GameMainConfig", order = 0)]
    public class GameMainConfig : PackageConfig
    {
        [ValueDropdown("GetAllSceneDirs")]
        [LabelText("场景目录")]

        //用于限定场景的目录避免一些测试或者第三方场景进入
        public string SceneBaseDir;



        [ValueDropdown("GetProcedureType")]
        [LabelText("默认进入流程")]
        public string EntryProcedure;

        [ValueDropdown("GetGameSectionUuid")]
        [LabelText("默认进入游戏段落")]
        public string EnterGameSectionUuid;

        [ValueDropdown("GetDefaultPlayer")]
        [LabelText("默认玩家")]
        [TabGroup("玩家")]
        public string DefaultPlayer;

        [LabelText("默认交互夹角")]
        [TabGroup("玩家")]
        public float DefaultInteractRadian = 0.45f;

        [LabelText("默认交互范围")]
        [TabGroup("玩家")]
        public float DefaultInteractRadius = 2f;

        [LabelText("默认Hotspot范围")]
        [TabGroup("玩家")]
        public float DefaultHotspotRadius = 3f;


        [LabelText("默认开启脚步声")]
        [TabGroup("玩家")]
        public bool DefaultEnabledFootstepSound = true;

        [LabelText("使用默认脚步声")]
        [TabGroup("玩家")]
        public bool UseDefaultFootstepSound = true;

        [LabelText("默认脚步声音量")]
        [TabGroup("玩家")]
        public float DefaultFootstepSoundVolume = 1;

        [LabelText("默认脚步声")]
        [TabGroup("玩家")]
        [ValueDropdown("GetSoundUuid")]
        public string[] DefaultFootstepSoundEffectUuids;

        [LabelText("脚步间隔")]
        [TabGroup("玩家")]
        public Vector2 FootstepSoundInterval = Vector2.one;

        [LabelText("脚步最小间隔")]
        [TabGroup("玩家")]
        public float FootstepSoundMinInterval = 0.3f;


        [LabelText("默认字幕的UIId")]
        [ValueDropdown("GetUIUuid")]
        [TabGroup("UI")]
        public string DefaultSubtitlePanelUuid = string.Empty;


        [LabelText("预览UI Uuid")]
        [ValueDropdown("GetUIUuid")]
        [TabGroup("UI")]

        public string PreviewPanelUuid = string.Empty;

        [LabelText("预览交互变量 Uuid")]
        [ValueDropdown("GetIntVariableUuid")]
        [TabGroup("UI")]

        public string DefaultPreviewIntVariable = string.Empty;


        [LabelText("预览退出变量 Uuid")]
        [ValueDropdown("GetBoolVariableUuid")]
        [TabGroup("UI")]

        public string DefaultPreviewExitVariable = string.Empty;

        [LabelText("调试指令")]
        [ValueDropdown("GetInstructions")]
        [ListDrawerSettings(Expanded = true)]
        [TabGroup("Debug")]

        public string[] DebuggerInstructions;



#if ENABLE_FGUI
        [ShowInInspector]
        [LabelText("初始化前Logo")]
        public List<LogoEntry> LogoEntries;
#endif

#if UNITY_EDITOR
        private IEnumerable GetInstructions()
        {
            return InstructionOverview.GetUuidDropdown();
        }


        private IEnumerable GetSoundUuid()
        {
            return SoundOverview.GetUuidDropdown();
        }

        private IEnumerable GetUIUuid()
        {
            return SimpleUIOverview.GetUuidDropdown();
        }

        private IEnumerable GetIntVariableUuid()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Int.ToString());
        }

        private IEnumerable GetBoolVariableUuid()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString());
        }


        private IEnumerable GetDefaultPlayer()
        {
            return CharacterOverview.GetUuidDropdown(AdditionalOptions: new List<Tuple<string, string>>(){
                new Tuple<string, string>("Default","Default"),
            });
        }

        private IEnumerable GetProcedureType()
        {
            var typeList = GameSupportEditorUtility.GetTypeNames<GameFramework.Procedure.ProcedureBase>().ToList();
            typeList.Insert(0, string.Empty);
            return typeList;
        }

        [TabGroup("其他")]
        public bool InitUnloadScene = true;

        private IEnumerable GetGameSectionUuid()
        {
            return GameSectionOverview.GetUuidDropdown();
        }



        private IEnumerable GetAllSceneDirs()
        {
            return GameSupportEditorUtility.GetAllSceneDirs();
        }
        private IEnumerable GetAllScenes()
        {
            var scenesList = GameSupportEditorUtility.GetAllScenes(SceneBaseDir).ToList();
            scenesList.Insert(0, ConstString.NULL);
            return scenesList;
        }
#endif
    }
#if ENABLE_FGUI
    [Serializable]
    public class LogoEntry{
        [ValueDropdown("GetUILogicTypes")]
        [ShowInInspector]
        public string LogoUIType;
        public UiConfigInfoTable.UiConfigInfoRow LogoUIConfig;
#if UNITY_EDITOR
        private IEnumerable GetUILogicTypes(){
            var uiTypes = GameSupportEditorUtility.GetTypeNames<UILogicBase>().ToList();
            uiTypes.Insert(0,null);
            return uiTypes;
        }
#endif
    }
#endif
}
