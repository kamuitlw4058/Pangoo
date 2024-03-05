using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Cinemachine;
using GameFramework;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using System;
using Pangoo.MetaTable;

namespace Pangoo
{

    public static partial class GameSupportEditorUtility
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



        public static IEnumerable GetAllEventsOverview()
        {
            // var datas = AssetDatabaseUtility.FindAsset<PangooEventsTableOverview>();
            // return datas;
            return null;
        }





        public static IEnumerable GetGameInfoItem()
        {
            var infos = Utility.Assembly.GetTypes(typeof(BaseInfo));
            var ret = new ValueDropdownList<string>();
            foreach (var info in infos)
            {
                ret.Add(info.FullName);
            }

            return ret;
        }



        public static IEnumerable<string> GetTypeNames<T>()
        {
            return TypeUtility.GetRuntimeTypeNames(typeof(T));
        }

        public static IEnumerable<System.Type> GetTypes<T>()
        {
            return TypeUtility.GetRuntimeTypes(typeof(T));
        }

        public static string[] GetPakcageDirs()
        {
            var packages = AssetDatabaseUtility.FindAsset<PackageConfig>().ToArray();
            string[] ret = new string[packages.Length];
            for (int i = 0; i < packages.Length; i++)
            {
                ret[i] = packages[i].PackageDir;
            }
            return ret;
        }

        public static IEnumerable GetPrefabs(string assetType)
        {
            var packageDirs = GetPakcageDirs();
            var ret = new ValueDropdownList<GameObject>();
            for (int i = 0; i < packageDirs.Length; i++)
            {
                var datas = AssetDatabaseUtility.FindAsset<GameObject>(AssetUtility.GetPrefabDir(packageDirs[i], assetType));
                foreach (var data in datas)
                {
                    ret.Add(data.name, data);
                }
            }

            return ret;
        }



        public static GameObject GetPrefabByAssetPathUuid(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace()) return null;

            var row = AssetPathOverview.GetUnityRowByUuid(uuid);
            if (row == null) return null;

            var finalPath = row.ToPrefabPath();
            return AssetDatabaseUtility.LoadAssetAtPath<GameObject>(finalPath);

        }



        public static GameObject GetPrefabByDynamicObjectUuid(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace()) return null;

            var row = DynamicObjectOverview.GetUnityRowByUuid(uuid);
            if (row == null) return null;

            var assetRow = AssetPathOverview.GetUnityRowByUuid(row.Row.AssetPathUuid);
            if (assetRow == null) return null;

            var finalPath = assetRow.ToPrefabPath();
            return AssetDatabaseUtility.LoadAssetAtPath<GameObject>(finalPath);

        }


        public static IEnumerable GetPackageConfig()
        {
            var datas = AssetDatabaseUtility.FindAsset<PackageConfig>();
            var ret = new ValueDropdownList<PackageConfig>();
            foreach (var data in datas)
            {
                ret.Add(data.MainNamespace, data);
            }

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

        public static IEnumerable GetInstructionType(string currentTypeStr = null)
        {
            var types = Utility.Assembly.GetTypes(typeof(Instruction));
            Type currentType = null;
            if (currentTypeStr != null)
            {
                currentType = Utility.Assembly.GetType(currentTypeStr);
            }

            ValueDropdownList<string> ret = new();
            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type == currentType)
                {
                    continue;
                }
                var attr = type.GetCustomAttribute(typeof(CategoryAttribute));
                ret.Add(attr.ToString(), types[i].ToString());
            }
            return ret;
        }

        public static IEnumerable GetUIParamsType(string currentTypeStr = null)
        {
            var types = Utility.Assembly.GetTypes(typeof(UIPanelParams));
            Type currentType = null;
            if (currentTypeStr != null)
            {
                currentType = Utility.Assembly.GetType(currentTypeStr);
            }

            ValueDropdownList<string> ret = new();
            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type == currentType)
                {
                    continue;
                }
                var attr = type.GetCustomAttribute(typeof(CategoryAttribute));
                ret.Add(attr.ToString(), types[i].ToString());
            }
            return ret;
        }

        public static IEnumerable GetSubTypeWithCategory<T>(string currentTypeStr = null)
        {
            var types = Utility.Assembly.GetTypes(typeof(T));
            Type currentType = null;
            if (currentTypeStr != null)
            {
                currentType = Utility.Assembly.GetType(currentTypeStr);
            }

            ValueDropdownList<string> ret = new();
            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type == currentType)
                {
                    continue;
                }
                var attr = type.GetCustomAttribute(typeof(CategoryAttribute));
                ret.Add(attr.ToString(), types[i].ToString());
            }
            return ret;
        }


        public static IEnumerable GetTriggerEvent(string currentTypeStr = null)
        {
            var types = Utility.Assembly.GetTypes(typeof(TriggerEvent));
            Type currentType = null;
            if (currentTypeStr != null)
            {
                currentType = Utility.Assembly.GetType(currentTypeStr);
            }

            ValueDropdownList<string> ret = new();
            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type == currentType)
                {
                    continue;
                }
                var attr = type.GetCustomAttribute(typeof(CategoryAttribute));
                ret.Add(attr.ToString(), types[i].ToString());
            }
            return ret;
        }


        public static void AddPrefabValueDropdownList(ValueDropdownList<GameObject> ret, Transform trans, string prefix, Dictionary<GameObject, string> goPathDict)
        {
            foreach (var child in trans.Children())
            {
                var path = $"{prefix}/{child.name}";
                if (prefix == string.Empty)
                {
                    path = child.name;
                }
                ret.Add(path, child.gameObject);
                goPathDict?.Add(child.gameObject, path);
                AddPrefabValueDropdownList(ret, child, path, goPathDict);
            }
        }


        public static IEnumerable RefPrefabDropdown(GameObject prefab, Dictionary<GameObject, string> goPathDict, bool hasSelf = true)
        {
            var ValueDropdown = new ValueDropdownList<GameObject>();
            if (hasSelf)
            {
                ValueDropdown.Add(ConstString.Self, prefab);
            }
            goPathDict?.Clear();
            if (prefab != null)
            {
                AddPrefabValueDropdownList(ValueDropdown, prefab.transform, string.Empty, goPathDict);
            }
            return ValueDropdown;
        }

        public static void AddPrefabStringDropdownList(ValueDropdownList<string> ret, Transform trans, string prefix)
        {
            foreach (var child in trans.Children())
            {
                var path = $"{prefix}/{child.name}";
                if (prefix == string.Empty)
                {
                    path = child.name;
                }
                ret.Add(path);
                AddPrefabStringDropdownList(ret, child, path);
            }
        }


        public static IEnumerable RefPrefabStringDropdown(GameObject prefab)
        {

            var ValueDropdown = new ValueDropdownList<string>();
            ValueDropdown.Add(ConstString.Self);
            ValueDropdown.Add(ConstString.Target);
            if (prefab != null)
            {
                AddPrefabStringDropdownList(ValueDropdown, prefab.transform, string.Empty);
            }
            return ValueDropdown;
        }

        public static IEnumerable GetNoiseSettings()
        {
            var ret = new ValueDropdownList<string>();

            var noiseSettingsAssets = Resources.LoadAll<NoiseSettings>("NoiseSettings");
            foreach (var assets in noiseSettingsAssets)
            {
                ret.Add(assets.name);
            }
            return ret;
        }


#endif
    }
}
