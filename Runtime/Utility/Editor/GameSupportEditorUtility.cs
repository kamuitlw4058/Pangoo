using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;
using GameFramework;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using System;

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


        public static string GetPakcageDirByOverviewRowId<T>(int id) where T : ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Table.BaseRows)
                {
                    if (row.Id == id)
                    {
                        return overview.Config.PackageDir;
                    }
                }
            }
            return null;
        }

        public static PackageConfig GetPakcageConfigByOverviewRowId<T>(int id) where T : ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Table.BaseRows)
                {
                    if (row.Id == id)
                    {
                        return overview.Config;
                    }
                }
            }
            return null;
        }

        public static T GetExcelTableOverviewByConfig<T>(PackageConfig config) where T : ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>(config.PackageDir).ToArray();
            if (overviews.Length > 0)
            {
                return overviews[0];
            }
            return null;
        }


        public static T GetExcelTableOverviewByRowId<T>(int id) where T : ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Table.BaseRows)
                {
                    if (row.Id == id)
                    {
                        return overview;
                    }
                }
            }
            return null;
        }

        public static R GetExcelTableRowWithOverviewById<T, R>(int id) where T : ExcelTableOverview where R : ExcelRowBase
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Table.BaseRows)
                {
                    if (row.Id == id)
                    {
                        return (R)row;
                    }
                }
            }
            return null;
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

        public static IEnumerable GetAllVolumeOverview()
        {
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>();
            var ret = new ValueDropdownList<VolumeTableOverview>();
            foreach (var overview in overviews)
            {
                ret.Add(overview.Config.MainNamespace, overview);
            }

            return ret;
        }



        public static IEnumerable GetVolumeRow()
        {
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>();
            var ret = new ValueDropdownList<int>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Data.Rows)
                {
                    ret.Add($"{row.Id}-{row.Name}", row.Id);
                }

            }
            return ret;
        }



        public static bool CheckVolumeId(int id)
        {
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Data.Rows)
                {
                    if (row.Id == id)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool CheckVolumeDupName(string name)
        {
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Data.Rows)
                {
                    if (row.Name == name)
                    {
                        return false;
                    }
                }
            }

            return true;
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
#endif
    }
}
