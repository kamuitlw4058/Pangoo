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

        [ValueDropdown("GetAllEventsOverview")]
        public List<PangooEventsTableOverview> EventOverviews;
 

#if UNITY_EDITOR

        private IEnumerable GetAllEventsOverview()
        {
            return GameSupportEditorUtility.GetAllEventsOverview();
        }

#endif

    }
}