using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pangoo;
using Pangoo.Editor;
using UnityEditor;
using UnityEngine;


namespace Pangoo.Editor
{

    public static class GameMainConfigEditorUtility
    {
#if UNITY_EDITOR
        public static PackageConfig GetGameMainConfig()
        {
            var configs = AssetDatabaseUtility.FindAsset<PackageConfig>();
            if (configs != null && configs.Count() > 0)
            {
                return configs.First();
            }

            var gameMainConfig = ScriptableObject.CreateInstance<PackageConfig>();
            AssetDatabase.CreateAsset(gameMainConfig, Path.Join(AssetUtility.GetConfigs(), $"GameMainConfig.asset"));
            return gameMainConfig;
        }

#endif

    }
}