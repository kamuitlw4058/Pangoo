// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pangoo.MetaTable
{
    [Serializable]
        [CreateAssetMenu(fileName = "AssetGroupOverview", menuName = "MetaTable/AssetGroupOverview")]
    public partial class AssetGroupOverview : MetaTableOverview
    {


        [TableList(AlwaysExpanded = true)]
        public List<UnityAssetGroupRow> Rows = new();

        public override string TableName => "AssetGroup";

         public override IReadOnlyList<MetaTableUnityRow> UnityBaseRows => Rows;

        public override MetaTableBase ToTable()
        {
           return ToTable<AssetGroupTable>();
        }
#if UNITY_EDITOR

        public static UnityAssetGroupRow GetUnityRowById(int id, string packageDir = null)
        {
           var overviews = AssetDatabaseUtility.FindAsset<AssetGroupOverview>(packageDir);
           foreach (var overview in overviews)
            {
               foreach (var row in overview.Rows)
                {
                   if (row.Row.Id == id)
                    {
                       return row;
                    }
                }
            }
             return null; 
        }

        public static IEnumerable GetIdDropdown(List<int> excludeIds = null, string packageDir = null)
        {
           var ret = new ValueDropdownList<int>();
           var overviews = AssetDatabaseUtility.FindAsset<AssetGroupOverview>(packageDir);
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

        public static IEnumerable GetUuidDropdown(List<string> excludeUuids = null, string packageDir = null, List<Tuple<string, string>> AdditionalOptions = null, List<string> includeUuids = null)
        {
           return GetUuidDropdown<AssetGroupOverview>(excludeUuids: excludeUuids, packageDir: packageDir,AdditionalOptions:AdditionalOptions,includeUuids:includeUuids);
        }

        public static UnityAssetGroupRow GetUnityRowByUuid(string uuid, string packageDir = null)
        {
           return GetUnityRowByUuid<AssetGroupOverview, UnityAssetGroupRow>(uuid);
        }

        public static AssetGroupOverview GetOverviewByUuid(string uuid, string packageDir = null)
        {
           return GetOverviewByUuid<AssetGroupOverview>(uuid);
        }

        public override void AddRow(MetaTableUnityRow unityRow)
        {
           AddRow<UnityAssetGroupRow>(unityRow);
           Rows.Add(unityRow as UnityAssetGroupRow);
        }

        public override void AddBaseRow(MetaTableRow row)
        {
           var unityRow = ScriptableObject.CreateInstance<UnityAssetGroupRow>();
           unityRow.Row = row as AssetGroupRow;
           AddRow<UnityAssetGroupRow>(unityRow);
           Rows.Add(unityRow);
        }

        public override void UpdateRow(string uuid, MetaTableRow baseRow)
        {
           foreach (var row in Rows)
            {
               if (row.Uuid.Equals(uuid))
                {
                   row.Row = baseRow as AssetGroupRow;
                   row.Row.Uuid = uuid;
                   return;
                }
            }
        }

        [Button("添加行")]
        public void AddRow()
        {
           var unityRow = AddRow<UnityAssetGroupRow>();
           Rows.Add(unityRow);
        }

        [Button("刷新行")]
        public override void RefreshRows()
        {
           Rows = RefreshRows<UnityAssetGroupRow>();
        }

        public override void RemoveByUuid(string uuid)
        {
           var unityRow = GetUnityRowByUuid(uuid) as UnityAssetGroupRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
        }
#endif
    }
}

