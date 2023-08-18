using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;


namespace Pangoo
{

    public static class GameSupportEditorUtility
    {
#if UNITY_EDITOR
        public static IEnumerable GetAssembly()
        {
            List<string> paths = new List<string>();
            IEnumerable<string> assets = AssetDatabase.FindAssets("t:AssemblyDefinitionAsset");
            return assets.Select(x =>
            {
                var path = AssetDatabase.GUIDToAssetPath(x);
                return Path.GetFileNameWithoutExtension(path);
            });
        }


        public static IEnumerable GetAllSceneDirs()
        {
            List<string> paths = new List<string>();
            IEnumerable<string> assets = AssetDatabase.FindAssets("t:Scene");
            return assets.Select(x =>
            {
                var path = AssetDatabase.GUIDToAssetPath(x);
                return Path.GetDirectoryName(path).Replace("\\", "/");
            });
            
        }

        public static IEnumerable<string> GetAllScenes(string prefix)
        {
            IEnumerable<string> assets = AssetDatabase.FindAssets("t:Scene");
            if (!string.IsNullOrEmpty(prefix))
            {
                assets = assets.Where(x => AssetDatabase.GUIDToAssetPath(x).StartsWith(prefix));
            }
            
            return assets.Select(x =>
            {
                var path = AssetDatabase.GUIDToAssetPath(x);
                return Path.GetFileNameWithoutExtension(path);
            });
        }

        public static IEnumerable GetAllExcelOverview()
        {
            var datas = AssetDatabaseUtility.FindAsset<ExcelTableOverview>();
            return datas;
        }

        public static IEnumerable GetAllEventsOverview()
        {
            // var datas = AssetDatabaseUtility.FindAsset<PangooEventsTableOverview>();
            // return datas;
            return null;
        }

        public static IEnumerable GetAllVolumeOverview(){
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>();
            var ret = new ValueDropdownList<VolumeTableOverview>();
            foreach(var overview in overviews){
                ret.Add(overview.Config.MainNamespace, overview);
            }

            return ret;
        }

        public static bool CheckVolumeId(int id){
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>();
            foreach(var overview in overviews){
                foreach(var row in overview.Data.Rows){
                    if(row.Id == id){
                        return false;
                    }
                }
            }

            return true;
        }



        public static IEnumerable<string> GetTypeNames<T>(){
            return TypeUtility.GetRuntimeTypeNames(typeof(T));
        }

        public static IEnumerable<System.Type> GetTypes<T>(){
            return TypeUtility.GetRuntimeTypes(typeof(T));
        }


        public static IEnumerable GetAllPackageConfig()
        {
            var datas = AssetDatabaseUtility.FindAsset<PackageConfig>();
            return datas;
        }

        public static IEnumerable GetNamespaces()
        {
            List<string> Namespaces = new List<string>();
            var assets = AssetDatabaseUtility.FindAsset<PackageConfig>();
            var namespaces = assets.Select(x => x.MainNamespace).ToList();
            namespaces.Insert(0, "");
            return namespaces;
        }
#endif
    }
}
