using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEditor;
using GameFramework;
using Pangoo.Core.VisualScripting;
using UnityEngine;
using MetaTable;

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
                    bool flag = false;
                    if (excludeUuid == null)
                    {
                        flag = true;
                    }
                    else
                    {
                        if (!excludeUuid.Contains(row.UuidShort))
                        {
                            flag = true;
                        }
                    }

                    if (flag)
                    {
                        ret.Add($"{row.UuidShort}-{row.Name}", row.Uuid);
                    }

                }
            }
            return ret;
        }


#endif
    }
}
