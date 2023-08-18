#if UNITY_EDITOR
using System.Collections.Generic;
using System.Collections;
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

        public static List<T> GetAssetListByPath<T>(string[] paths) where T : UnityEngine.Object
        {
            var items = AssetDatabase.FindAssets($"t:{typeof(T).Name}",paths)
           .Select(x => AssetDatabase.GUIDToAssetPath(x))
           .Select(x => AssetDatabase.LoadAssetAtPath<T>(x));
            return items.ToList();
        }


        public static List<T> GetAssetListByPath<T>(string path) where T : UnityEngine.Object
        {

            return GetAssetListByPath<T>(new string[]{path});
        }


        public static string GetAssetDir(UnityEngine.Object obj){
            var path = AssetDatabase.GetAssetPath(obj);
            var index = path.LastIndexOf('/');
            if(index > 0){
                return path.Substring(0,index + 1);
            }

            return null;
        }


        public static string GetAssetFullFileName(UnityEngine.Object obj){
            var path = AssetDatabase.GetAssetPath(obj);
            var index = path.LastIndexOf('/');
            if(index > 0){
                return path.Substring(index + 1);
            }

            return null;
        }


        public static string GetAssetFileExtension(UnityEngine.Object obj){
            var path = AssetDatabase.GetAssetPath(obj);
            var index = path.LastIndexOf('.');
            if(index > 0){
                return path.Substring(index + 1);
            }

            return null;
        }



#endif
        public static string GetVolumeProfile(string packageDir,string name){
            return $"{packageDir}/StreamRes/Volume/{name}.asset";
        }

        public static string GetVolumePrefab(string packageDir,string name){
            return $"{packageDir}/StreamRes/Prefab/Volume/{name}.prefab";
        }

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

        public static string GetResourceFGUIPackage(string package)
        {
            return $"FGUI/{package}/{package}_fui";
        }

        public static string GetFGUIAsset(string package, string component)
        {
            return $"{GetStreamRes("FGUI")}/{package}/{component}";
        }

        public static string GetFGUIResourceAsset(string package, string assetName)
        {
            return $"FGUI/{package}/{assetName}";
        }
        public static string GetConfigs()
        {
            return GetStreamRes("Configs");
        }

        public static string GetScene(string SceneName){
                return $"{GetStreamRes("Scenes")}/{SceneName}.unity";
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