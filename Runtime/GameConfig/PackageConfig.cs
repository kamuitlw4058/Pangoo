using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo
{

    [CreateAssetMenu(fileName = "PackageConfig", menuName = "Pangoo/PackageConfig", order = 0)]
    public class PackageConfig : GameConfigBase
    {
        [LabelText("语言版本")]
        public string Lang = "cn";

        [ValueDropdown("GetAllSceneDirs")]
        [LabelText("场景目录")]

        //用于限定场景的目录避免一些测试或者第三方场景进入
        public string SceneBaseDir;



        [ValueDropdown("GetAllScenes")]
        [LabelText("默认跳转场景")]
        public string DefaultJumpScene;

#if UNITY_EDITOR

        public bool InitUnloadScene = true;
#endif
        public string GetDefaultJumpScene()
        {
            return $"{SceneBaseDir}/{DefaultJumpScene}";
        }

        [FolderPath]
        public string PackageDir = AssetUtility.GetGameMain();

        [FolderPath(ParentFolder = "$PackageDir")]
        public string StreamResDir = "StreamRes";


        [FolderPath(ParentFolder = "$PackageDir")]
        public string ScriptsMainDir = "Scripts/Main";



        public string MainNamespace;

        [ValueDropdown("GetAllEventsOverview")]
        public List<PangooEventsTableOverview> EventOverviews;


#if UNITY_EDITOR

        private IEnumerable GetAllEventsOverview()
        {
            return GameSupportEditorUtility.GetAllEventsOverview();
        }

        private IEnumerable GetAllSceneDirs()
        {
            return GameSupportEditorUtility.GetAllSceneDirs();
        }
        private IEnumerable GetAllScenes()
        {
            return GameSupportEditorUtility.GetAllScenes(SceneBaseDir);
        }
#endif

    }
}