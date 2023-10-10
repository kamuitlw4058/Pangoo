using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;
using GameFramework;

namespace Pangoo
{

    public static partial class GameSupportEditorUtility
    {
#if UNITY_EDITOR

        public static bool GetExcelTableNameInPangoo(string name)
        {
            var types = Utility.Assembly.GetTypes(typeof(ExcelTableBase));
            foreach (var type in types)
            {
                if (type.FullName == $"Pangoo.{name}Table")
                {
                    return true;
                }
            }

            return false;
        }

        public static List<string> GetExcelTablePangooTableNames()
        {
            return Utility.Assembly.GetTypes(typeof(ExcelTableBase)).Select(o => o.FullName).Where(o => o.StartsWith("Pangoo.")).ToList();

        }

        public static string GetExcelTablePangooTableName(string name)
        {
            return $"Pangoo.{name}Table";
        }



        public static IEnumerable GetExcelTableOverview<T>() where T : ExcelTableOverview
        {
            var ret = new ValueDropdownList<T>();
            var overviews = AssetDatabaseUtility.FindAsset<T>();
            foreach (var overview in overviews)
            {
                ret.Add(overview.Config.MainNamespace, overview);
            }
            return ret;
        }

        public static List<int> GetExcelTableOverviewIds<T>(List<int> ids = null, string packageDir = null) where T : ExcelTableOverview
        {
            List<int> ret = new List<int>();
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Table.BaseRows)
                {
                    if (ids == null)
                    {
                        ret.Add(row.Id);
                    }
                    else
                    {
                        if (!ids.Contains(row.Id))
                        {
                            ret.Add(row.Id);
                        }
                    }
                }
            }
            return ret;
        }

        public static IEnumerable GetExcelTableOverviewNamedIds<T>(List<int> ids = null, string packageDir = null) where T : ExcelTableOverview
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach (var overview in overviews)
            {
                var namedRows = overview.Table.NamedBaseRows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {
                    if (ids == null)
                    {
                        ret.Add($"{row.Id}-{row.Name}", row.Id);
                    }
                    else
                    {
                        if (!ids.Contains(row.Id))
                        {
                            ret.Add($"{row.Id}-{row.Name}", row.Id);
                        }
                    }
                }
            }
            return ret;
        }

        public static bool ExistsAssetPath(string packageDir, string name, string assetType, string fileType)
        {
            var path = AssetUtility.GetAssetPath(packageDir, assetType, $"{name}.{fileType}");
            return File.Exists(path);
        }

        public static IEnumerable GetAssetPathIds(List<int> ids = null, List<string> assetTypes = null, string packageDir = null)
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<AssetPathTableOverview>(packageDir);
            foreach (var overview in overviews)
            {
                var Rows = overview.Data.Rows;
                if (Rows == null)
                {
                    continue;
                }
                foreach (var row in Rows)
                {
                    if (assetTypes != null)
                    {
                        if (!assetTypes.Contains(row.AssetType))
                        {
                            continue;
                        }
                    }

                    if (ids == null)
                    {
                        ret.Add($"{row.Id}-{row.Name}", row.Id);
                    }
                    else
                    {
                        if (!ids.Contains(row.Id))
                        {
                            ret.Add($"{row.Id}-{row.Name}", row.Id);
                        }
                    }
                }
            }
            return ret;
        }



        public static bool ExistsExcelTableOverviewId<T>(int id, string packageDir = null) where T : ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Table.BaseRows)
                {
                    if (row.Id == id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool ExistsExcelTableOverviewName<T>(string name, string packageDir = null) where T : ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach (var overview in overviews)
            {
                var rows = overview.Table.NamedBaseRows;
                if (rows == null)
                {
                    return false;
                }

                foreach (var row in rows)
                {
                    if (row.Name == name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public static IEnumerable GetGameSectionIds(List<int> ids = null)
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<GameSectionTableOverview>();
            foreach (var overview in overviews)
            {
                var namedRows = overview.Data.Rows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {
                    if (ids == null)
                    {
                        ret.Add($"{row.Id}-{row.Name}", row.Id);
                    }
                    else
                    {
                        if (!ids.Contains(row.Id))
                        {
                            ret.Add($"{row.Id}-{row.Name}", row.Id);
                        }
                    }
                }
            }
            return ret;
        }


        public static GameSectionTable.GameSectionRow GetGameSectionRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<GameSectionTableOverview, GameSectionTable.GameSectionRow>(id);
        }

        public static DynamicObjectTable.DynamicObjectRow GetDynamicObjectRow(int id)
        {
            return GetExcelTableRowWithOverviewById<DynamicObjectTableOverview, DynamicObjectTable.DynamicObjectRow>(id);
        }


        public static StaticSceneTable.StaticSceneRow GetStaticSceneRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<StaticSceneTableOverview, StaticSceneTable.StaticSceneRow>(id);
        }

        public static AssetPathTable.AssetPathRow GetAssetPathRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<AssetPathTableOverview, AssetPathTable.AssetPathRow>(id);
        }

        public static TriggerEventTable.TriggerEventRow GetTriggerEventRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<TriggerEventTableOverview, TriggerEventTable.TriggerEventRow>(id);
        }

#endif
    }
}
