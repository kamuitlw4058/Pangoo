// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections;
using System.IO;
using System.Linq;
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
        [CreateAssetMenu(fileName = "StaticSceneOverview", menuName = "MetaTable/StaticSceneOverview")]
    public partial class StaticSceneOverview : MetaTableOverview
    {


        [TableList(AlwaysExpanded = true)]
        public List<UnityStaticSceneRow> Rows = new();

        public override string TableName => "StaticScene";

         public override IReadOnlyList<MetaTableUnityRow> UnityBaseRows => Rows;

        public override MetaTableBase ToTable()
        {
           return ToTable<StaticSceneTable>();
        }
#if UNITY_EDITOR

        public static UnityStaticSceneRow GetUnityRowById(int id, string packageDir = null)
        {
           var overviews = AssetDatabaseUtility.FindAsset<StaticSceneOverview>(packageDir);
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

        public static IEnumerable GetUuidDropdown(List<string> excludeUuids = null, string packageDir = null, List<Tuple<string, string>> AdditionalOptions = null, List<Tuple<string, string>> includeUuids = null)
        {
           return GetUuidDropdown<StaticSceneOverview>(excludeUuids: excludeUuids, packageDir: packageDir,AdditionalOptions:AdditionalOptions,includeUuids:includeUuids);
        }

        public static UnityStaticSceneRow GetUnityRowByUuid(string uuid, string packageDir = null)
        {
           return GetUnityRowByUuid<StaticSceneOverview, UnityStaticSceneRow>(uuid);
        }

        public static StaticSceneOverview GetOverviewByUuid(string uuid, string packageDir = null)
        {
           return GetOverviewByUuid<StaticSceneOverview>(uuid);
        }

        public override void AddRow(MetaTableUnityRow unityRow)
        {
           AddRow<UnityStaticSceneRow>(unityRow);
           Rows.Add(unityRow as UnityStaticSceneRow);
        }

        public override void AddBaseRow(MetaTableRow row)
        {
           var unityRow = ScriptableObject.CreateInstance<UnityStaticSceneRow>();
           unityRow.Row = row as StaticSceneRow;
           AddRow<UnityStaticSceneRow>(unityRow);
           Rows.Add(unityRow);
        }

        public override void UpdateRow(string uuid, MetaTableRow baseRow)
        {
           foreach (var row in Rows)
            {
               if (row.Uuid.Equals(uuid))
                {
                   row.Row = baseRow as StaticSceneRow;
                   row.Row.Uuid = uuid;
                   return;
                }
            }
        }

        [Button("添加行")]
        public void AddRow()
        {
           var unityRow = AddRow<UnityStaticSceneRow>();
           Rows.Add(unityRow);
        }

        [Button("刷新行")]
        public override void RefreshRows()
        {
           Rows = RefreshRows<UnityStaticSceneRow>();
        }

        public override void RemoveByUuid(string uuid)
        {
           var unityRow = GetUnityRowByUuid(uuid) as UnityStaticSceneRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
        }
#endif
    }
}

