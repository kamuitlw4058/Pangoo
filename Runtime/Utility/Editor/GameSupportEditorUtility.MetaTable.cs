using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using Sirenix.OdinInspector;
using GameFramework;
using Pangoo.Core.VisualScripting;
using UnityEngine;
using MetaTable;
using Pangoo.Common;
using Pangoo.Core.Common;

namespace Pangoo.MetaTable
{

    public static partial class GameSupportEditorUtility
    {
#if UNITY_EDITOR

        public static string GetAssetGroupUuidByAssetGroup(string AssetGroup)
        {
            if (AssetGroup.IsNullOrWhiteSpace())
            {
                return null;
            }

            var overviews = AssetDatabaseUtility.FindAsset<AssetGroupOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Rows)
                {

                    if (row.Row.AssetGroup.Equals(AssetGroup))
                    {
                        return row.Uuid;
                    }
                }
            }
            return null;
        }

        public static string GetAssetGroupByAssetGroupUuid(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace())
            {
                return null;
            }


            var overviews = AssetDatabaseUtility.FindAsset<AssetGroupOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Rows)
                {

                    if (row.Uuid.Equals(uuid))
                    {
                        return row.Row.AssetGroup;
                    }
                }
            }

            return null;
        }


        public static IEnumerable GetAssetGroupUuidDropdown(string excludeAssetGroup = null)
        {
            var ret = new ValueDropdownList<string>();
            ret.Add("Default", string.Empty);

            var overviews = AssetDatabaseUtility.FindAsset<AssetGroupOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Rows)
                {
                    if (excludeAssetGroup.IsNullOrWhiteSpace())
                    {
                        ret.Add($"{row.UuidShort}-{row.Name}", row.Uuid);
                    }
                    else
                    {
                        if (!row.Row.AssetGroup.Equals(excludeAssetGroup))
                        {
                            ret.Add($"{row.UuidShort}-{row.Name}", row.Uuid);
                        }
                    }
                }
            }
            return ret;
        }

        public static GameObject GetPrefabByAssetPathUuid(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace()) return null;

            var row = GameSupportEditorUtility.GetAssetPathRowByUuid(uuid);
            if (row == null) return null;

            var finalPath = AssetUtility.GetAssetPath(row.AssetPackageDir, row.AssetType, row.AssetPath, row.AssetGroup);
            return AssetDatabaseUtility.LoadAssetAtPath<GameObject>(finalPath);

        }
        public static AssetPathRow GetAssetPathRowByUuid(string uuid)
        {
            return GetMetaTableRowWithOverviewByUuid<AssetPathOverview, AssetPathRow>(uuid);
        }



        public static R GetMetaTableRowWithOverviewByUuid<T, R>(string uuid) where T : MetaTableOverview where R : MetaTableRow
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.BaseRows)
                {
                    if (row.Uuid.Equals(uuid))
                    {
                        return (R)row;
                    }
                }
            }
            return null;
        }


        public static UnityAssetPathRow GetAssetPathById(int id, string packageDir = null)
        {
            UnityAssetPathRow ret = null;
            var overviews = AssetDatabaseUtility.FindAsset<AssetPathOverview>(packageDir);
            foreach (var overview in overviews)
            {

                foreach (var row in overview.Rows)
                {
                    if (row.Row.Id == id)
                    {
                        ret = row;
                    }

                }
            }
            return ret;
        }

        public static IEnumerable GetAssetPathIds(List<int> excludeIds = null, List<string> assetTypes = null, string packageDir = null)
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<AssetPathOverview>(packageDir);
            foreach (var overview in overviews)
            {

                foreach (var row in overview.Rows)
                {
                    if (assetTypes != null)
                    {
                        if (!assetTypes.Contains(row.Row.AssetType))
                        {
                            continue;
                        }
                    }

                    if (excludeIds == null)
                    {
                        ret.Add($"{row.Row.Id}-{row.Name}", row.Row.Id);
                    }
                    else
                    {
                        if (!excludeIds.Contains(row.Row.Id))
                        {
                            ret.Add($"{row.Row.Id}-{row.Name}", row.Row.Id);
                        }
                    }
                }
            }
            return ret;
        }

        public static IEnumerable GetAssetPathUuids(List<string> excludeUuid = null, List<string> assetTypes = null, string packageDir = null)
        {
            var ret = new ValueDropdownList<string>();
            var overviews = AssetDatabaseUtility.FindAsset<AssetPathOverview>(packageDir);
            foreach (var overview in overviews)
            {

                foreach (var row in overview.Rows)
                {
                    if (assetTypes != null)
                    {
                        if (!assetTypes.Contains(row.Row.AssetType))
                        {
                            continue;
                        }
                    }
                    bool flag = excludeUuid == null ? true : !excludeUuid.Contains(row.Row.Uuid) ? true : false;
                    if (flag)
                    {
                        ret.Add($"{row.UuidShort}-{row.Name}", row.Uuid);
                    }
                }
            }
            return ret;
        }

        public static IEnumerable GetStaticSceneIds(List<int> excludeIds = null, string packageDir = null)
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<StaticSceneOverview>(packageDir);
            foreach (var overview in overviews)
            {

                foreach (var row in overview.Rows)
                {
                    bool flag = excludeIds == null ? true : !excludeIds.Contains(row.Row.Id) ? true : false;
                    if (flag)
                    {
                        ret.Add($"{row.Row.Id}-{row.Name}", row.Row.Id);
                    }
                }
            }
            return ret;
        }

        public static IEnumerable GetStaticSceneUuids(List<string> excludeUuid = null, string packageDir = null)
        {
            var ret = new ValueDropdownList<string>();
            var overviews = AssetDatabaseUtility.FindAsset<StaticSceneOverview>(packageDir);
            foreach (var overview in overviews)
            {

                foreach (var row in overview.Rows)
                {
                    bool flag = excludeUuid == null ? true : !excludeUuid.Contains(row.Row.Uuid) ? true : false;
                    if (flag)
                    {
                        ret.Add($"{row.UuidShort}-{row.Name}", row.Uuid);
                    }
                }
            }
            return ret;
        }

        public static UnityStaticSceneRow GetStaticSceneById(int id, string packageDir = null)
        {
            UnityStaticSceneRow ret = null;
            var overviews = AssetDatabaseUtility.FindAsset<StaticSceneOverview>(packageDir);
            foreach (var overview in overviews)
            {

                foreach (var row in overview.Rows)
                {
                    if (row.Row.Id == id)
                    {
                        ret = row;
                    }

                }
            }
            return ret;
        }

        public static UnityStaticSceneRow GetStaticSceneByUuid(string uuid, string packageDir = null)
        {
            UnityStaticSceneRow ret = null;
            var overviews = AssetDatabaseUtility.FindAsset<StaticSceneOverview>(packageDir);
            foreach (var overview in overviews)
            {

                foreach (var row in overview.Rows)
                {
                    if (row.Row.Uuid == uuid)
                    {
                        ret = row;
                    }

                }
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
                var attr = type.GetCustomAttribute(typeof(Pangoo.Core.Common.CategoryAttribute));
                string key = attr != null ? attr.ToString() : types[i].ToString();
                ret.Add(key, types[i].ToString());
            }
            return ret;
        }

        public static IEnumerable GetInstructionType(string currentTypeStr = null)
        {
            var types = AssemblyUtility.GetTypes(typeof(Instruction));
            Type currentType = null;
            if (currentTypeStr != null)
            {
                currentType = AssemblyUtility.GetType(currentTypeStr);
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
