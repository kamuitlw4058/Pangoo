using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
using Pangoo.Core.Characters;

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

        [LabelText("跳过主菜单")]
        public bool SkipMainMenu;

        [LabelText("鼠标开关按键")]
        public KeyCode CursorOnOffKeyCode;

        [LabelText("当前游戏段落变量")]
        [ValueDropdown("GetStringVariableUuid")]
        public string CurrentGameSectionVariableUuid;

        [ValueDropdown("GetDefaultPlayer")]
        [LabelText("默认玩家")]
        [FoldoutGroup("玩家", expanded: true)]
        public string DefaultPlayer;

        [LabelText("默认交互夹角")]
        [TabGroup("玩家/玩家配置", "交互")]

        public float DefaultInteractRadian = 0.45f;

        [LabelText("默认交互范围")]
        [TabGroup("玩家/玩家配置", "交互")]
        public float DefaultInteractRadius = 2f;

        [LabelText("默认Hotspot范围")]
        [TabGroup("玩家/玩家配置", "交互")]
        public float DefaultHotspotRadius = 3f;


        [LabelText("默认开启脚步声")]
        [TabGroup("玩家/玩家配置", "脚步声")]
        public bool DefaultEnabledFootstepSound = true;

        [LabelText("使用默认脚步声")]
        [TabGroup("玩家/玩家配置", "脚步声")]
        public bool UseDefaultFootstepSound = true;

        [HideLabel]
        [TabGroup("玩家/玩家配置", "脚步声")]
        public FootstepEntry FootstepEntry;

        [ValueDropdown("GetIntVariableUuid")]
        [FoldoutGroup("动态物体", expanded: true)]
        [LabelText("动态物体状态子物体")]
        public string DynamicObjectStateVariableUuid = string.Empty;


        [LabelText("默认字幕的UIId")]
        [ValueDropdown("GetUIUuid")]
        [FoldoutGroup("UI", expanded: true)]
        public string DefaultSubtitlePanelUuid = string.Empty;

        [LabelText("默认主菜单的Uuid")]
        [ValueDropdown("GetUIUuid")]
        [FoldoutGroup("UI")]
        public string DefaultMainMenuPanelUuid = string.Empty;


        [LabelText("预览UI Uuid")]
        [ValueDropdown("GetUIUuid")]
        [FoldoutGroup("UI")]

        public string PreviewPanelUuid = string.Empty;

        [LabelText("对话UI Uuid")]
        [ValueDropdown("GetUIUuid")]
        [FoldoutGroup("UI")]
        public string DialoguePanelUuid = string.Empty;


        [LabelText("案件UI Uuid")]
        [ValueDropdown("@SimpleUIOverview.GetUuidDropdown()")]
        [FoldoutGroup("UI")]
        public string CasePanelUuid = string.Empty;

        [LabelText("预览交互变量 Uuid")]
        [ValueDropdown("GetIntVariableUuid")]
        [FoldoutGroup("UI")]

        public string DefaultPreviewIntVariable = string.Empty;


        [LabelText("预览退出变量 Uuid")]
        [ValueDropdown("GetBoolVariableUuid")]
        [FoldoutGroup("UI")]

        public string DefaultPreviewExitVariable = string.Empty;

        [LabelText("调试指令")]
        [ValueDropdown("GetInstructions")]
        [ListDrawerSettings(Expanded = true)]
        [FoldoutGroup("Debug", expanded: true)]

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

        private IEnumerable GetStringVariableUuid()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.String.ToString());
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

        [FoldoutGroup("其他")]
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
