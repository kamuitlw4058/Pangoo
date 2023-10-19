using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
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

        private IEnumerable GetProcedureType()
        {
            var typeList = GameSupportEditorUtility.GetTypeNames<GameFramework.Procedure.ProcedureBase>().ToList();
            typeList.Insert(0, string.Empty);
            return typeList;
        }

#if ENABLE_FGUI
        [ShowInInspector]
        [LabelText("初始化前Logo")]
        public List<LogoEntry> LogoEntries;
#endif

#if UNITY_EDITOR

        public bool InitUnloadScene = true;

        [ValueDropdown("GetGameSectionIds")]
        public int EnterGameSectionId;

        private IEnumerable GetGameSectionIds()
        {
            var typeList = GameSupportEditorUtility.GetExcelTableOverviewNamedIds<GameSectionTableOverview>();
            return typeList;
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
