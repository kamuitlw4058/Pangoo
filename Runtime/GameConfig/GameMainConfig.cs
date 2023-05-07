using System.Collections;
using System.Collections.Generic;
using Pangoo;
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
            return $"{SceneBaseDir}/{DefaultJumpScene}";
        }


       [ValueDropdown("GetProcedureType")]
        public string EntryProcedure;

        public UiConfigInfoTable.UiConfigInfoRow LogoUI;

#if UNITY_EDITOR

        public bool InitUnloadScene = true;

        private IEnumerable GetProcedureType()
        {
            var typeList = GameSupportEditorUtility.GetTypeNames<GameFramework.Procedure.ProcedureBase>().ToList();
            typeList.Insert(0,string.Empty);
            return typeList;
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
