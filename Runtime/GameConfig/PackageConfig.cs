using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;

namespace Pangoo
{

    [CreateAssetMenu(fileName = "PackageConfig", menuName = "Pangoo/PackageConfig", order = 0)]
    public class PackageConfig : GameConfigBase
    {
        [LabelText("语言版本")]
        public string Lang = "cn";

        [FolderPath]
        public string PackageDir = AssetUtility.GetGameMain();

        [FolderPath(ParentFolder = "$PackageDir")]
        public string StreamResDir = "StreamRes";


        [FolderPath(ParentFolder = "$PackageDir")]
        public string ScriptsMainDir = "Scripts/Main";

        public string MainNamespace;


        [LabelText("资源路径起始Id")]
        public int AssetPathBaseId;


        [LabelText("动态物体起始Id")]
        public int DynmaicObjectBaseId;


        [LabelText("静态场景起始Id")]
        public int StaticSceneBaseId;


        [LabelText("角色起始Id")]
        public int CharacterBaseId;


        // [ValueDropdown("GetAllEventsOverview")]
        // public List<PangooEventsTableOverview> EventOverviews;


#if UNITY_EDITOR

        private IEnumerable GetAllEventsOverview()
        {
            return GameSupportEditorUtility.GetAllEventsOverview();
        }

#endif

    }
}