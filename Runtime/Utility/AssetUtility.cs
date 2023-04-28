#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;
#endif

namespace Pangoo
{
    public static class AssetUtility
    {
#if UNITY_EDITOR

        public static T GetAssetByPath<T>(string path) where T : UnityEngine.Object
        {
            var items = AssetDatabase.FindAssets($"t:{typeof(T).Name}")
           .Select(x => AssetDatabase.GUIDToAssetPath(x))
           .Select(x => AssetDatabase.LoadAssetAtPath<T>(x));
            if (items.Count() > 0)
            {
                return items.First();
            }
            return null;
        }
#endif


        public static string GetPrefab()
        {
            return GetStreamRes("Prefab");
        }

        public static string GetItem(string name)
        {
            return $"{GetPrefab()}/Items/{name}.prefab";
        }

        public static string GetGameMainConfig()
        {
            return $"{GetConfigs()}/GameMainConfig.asset";
        }

        public static string GetGameMain()
        {
            return $"Assets/GameMain";
        }

        public static string GetStreamRes(string moduleName = "")
        {
            return $"{GetGameMain()}/StreamRes/{moduleName}";
        }


        public static string GetFGUIPackage(string package)
        {
            return $"{GetStreamRes("FGUI")}/{package}_fui.bytes";
        }

        public static string GetFGUIAsset(string package, string component)
        {
            return $"{GetStreamRes("FGUI")}/{component}";
        }
        public static string GetConfigs()
        {
            return GetStreamRes("Configs");
        }

        public static string GetResources()
        {
            return "Assets/Resources";
        }

        public static string GetHotfixDLLEditorAsset()
        {
            return "Library/ScriptAssemblies/FairyWay.Hotfix.dll";
        }

        public static string GetHotfixPDBEditorAsset()
        {
            return "Library/ScriptAssemblies/FairyWay.Hotfix.pdb";
        }


    }
}