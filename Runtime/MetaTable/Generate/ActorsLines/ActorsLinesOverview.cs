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
        [CreateAssetMenu(fileName = "ActorsLinesOverview", menuName = "MetaTable/ActorsLinesOverview")]
    public partial class ActorsLinesOverview : MetaTableOverview
    {


        [TableList(AlwaysExpanded = true)]
        public List<UnityActorsLinesRow> Rows = new();

        public override string TableName => "ActorsLines";

         public override IReadOnlyList<MetaTableUnityRow> UnityBaseRows => Rows;

        public override MetaTableBase ToTable()
        {
           return ToTable<ActorsLinesTable>();
        }
#if UNITY_EDITOR

        public static IEnumerable GetUuidDropdown()
        {
           return GetUuidDropdown<ActorsLinesOverview>();
        }

        public static IEnumerable GetUuidDropdown(List<Tuple<string, string>> AdditionalOptions = null)
        {
           return GetUuidDropdown<ActorsLinesOverview>(AdditionalOptions:AdditionalOptions);
        }

        public static IEnumerable GetUuidDropdown(List<string> excludeUuids = null, string packageDir = null, List<Tuple<string, string>> AdditionalOptions = null, List<Tuple<string, string>> includeUuids = null)
        {
           return GetUuidDropdown<ActorsLinesOverview>(excludeUuids: excludeUuids, packageDir: packageDir,AdditionalOptions:AdditionalOptions,includeUuids:includeUuids);
        }

        public static UnityActorsLinesRow GetUnityRowByUuid(string uuid, string packageDir = null)
        {
           return GetUnityRowByUuid<ActorsLinesOverview, UnityActorsLinesRow>(uuid);
        }

        public static ActorsLinesOverview GetOverviewByUuid(string uuid, string packageDir = null)
        {
           return GetOverviewByUuid<ActorsLinesOverview>(uuid);
        }

        public override void AddRow(MetaTableUnityRow unityRow)
        {
           AddRow<UnityActorsLinesRow>(unityRow);
           Rows.Add(unityRow as UnityActorsLinesRow);
        }

        public override void AddBaseRow(MetaTableRow row)
        {
           var unityRow = ScriptableObject.CreateInstance<UnityActorsLinesRow>();
           unityRow.Row = row as ActorsLinesRow;
           AddRow<UnityActorsLinesRow>(unityRow);
           Rows.Add(unityRow);
        }

        public override void UpdateRow(string uuid, MetaTableRow baseRow)
        {
           foreach (var row in Rows)
            {
               if (row.Uuid.Equals(uuid))
                {
                   row.Row = baseRow as ActorsLinesRow;
                   row.Row.Uuid = uuid;
                   return;
                }
            }
        }

        [Button("添加行")]
        public void AddRow()
        {
           var unityRow = AddRow<UnityActorsLinesRow>();
           Rows.Add(unityRow);
        }

        [Button("刷新行")]
        public override void RefreshRows()
        {
           Rows = RefreshRows<UnityActorsLinesRow>();
        }

        public override void RemoveByUuid(string uuid)
        {
           var unityRow = GetUnityRowByUuid(uuid) as UnityActorsLinesRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
        }
#endif
    }
}

