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

        [ValueDropdown("GetAllScenes")]
        [LabelText("默认跳转场景")]
        public string DefaultJumpScene;


        public string GetDefaultJumpScene()
        {
            return $"{SceneBaseDir}/{DefaultJumpScene}.unity";
        }


        [ValueDropdown("GetProcedureType")]
        public string EntryProcedure;

        [ValueDropdown("GetGameSectionUuid")]
        [LabelText("默认进入游戏段落")]
        public string EnterGameSectionUuid;

        [ValueDropdown("GetDefaultPlayer")]
        [LabelText("默认玩家")]
        public string DefaultPlayer;

        [LabelText("默认交互夹角")]
        public float DefaultInteractRadian = 0.45f;

        [LabelText("默认交互范围")]
        public float DefaultInteractRadius = 2f;

        [LabelText("默认Hotspot范围")]
        public float DefaultHotspotRadius = 3f;


        [LabelText("默认字幕的UIId")]
        [ValueDropdown("GetUIId")]
        public string DefaultSubtitlePanelUuid = string.Empty;


        [LabelText("预览UI Uuid")]
        [ValueDropdown("GetUIId")]
        public string PreviewPanelUuid = string.Empty;

        [LabelText("调试指令")]
        [ValueDropdown("GetInstructions")]
        [ListDrawerSettings(Expanded = true)]
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

        private IEnumerable GetUIId()
        {
            return SimpleUIOverview.GetUuidDropdown();
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
