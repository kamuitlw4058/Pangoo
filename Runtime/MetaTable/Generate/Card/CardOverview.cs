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
        [CreateAssetMenu(fileName = "CardOverview", menuName = "MetaTable/CardOverview")]
    public partial class CardOverview : MetaTableOverview
    {


        [TableList(AlwaysExpanded = true)]
        public List<UnityCardRow> Rows = new();

        public override string TableName => "Card";

         public override IReadOnlyList<MetaTableUnityRow> UnityBaseRows => Rows;

        public override MetaTableBase ToTable()
        {
           return ToTable<CardTable>();
        }
#if UNITY_EDITOR

        public static IEnumerable GetUuidDropdown()
        {
           return GetUuidDropdown<CardOverview>();
        }

        public static IEnumerable GetUuidDropdown(List<Tuple<string, string>> AdditionalOptions = null)
        {
           return GetUuidDropdown<CardOverview>(AdditionalOptions:AdditionalOptions);
        }

        public static IEnumerable GetUuidDropdown(List<string> excludeUuids = null, string packageDir = null, List<Tuple<string, string>> AdditionalOptions = null, List<Tuple<string, string>> includeUuids = null)
        {
           return GetUuidDropdown<CardOverview>(excludeUuids: excludeUuids, packageDir: packageDir,AdditionalOptions:AdditionalOptions,includeUuids:includeUuids);
        }

        public static UnityCardRow GetUnityRowByUuid(string uuid, string packageDir = null)
        {
           return GetUnityRowByUuid<CardOverview, UnityCardRow>(uuid);
        }

        public static CardOverview GetOverviewByUuid(string uuid, string packageDir = null)
        {
           return GetOverviewByUuid<CardOverview>(uuid);
        }

        public override void AddRow(MetaTableUnityRow unityRow)
        {
           AddRow<UnityCardRow>(unityRow);
           Rows.Add(unityRow as UnityCardRow);
        }

        public override void AddBaseRow(MetaTableRow row)
        {
           var unityRow = ScriptableObject.CreateInstance<UnityCardRow>();
           unityRow.Row = row as CardRow;
           AddRow<UnityCardRow>(unityRow);
           Rows.Add(unityRow);
        }

        public override void UpdateRow(string uuid, MetaTableRow baseRow)
        {
           foreach (var row in Rows)
            {
               if (row.Uuid.Equals(uuid))
                {
                   row.Row = baseRow as CardRow;
                   row.Row.Uuid = uuid;
                   return;
                }
            }
        }

        [Button("添加行")]
        public void AddRow()
        {
           var unityRow = AddRow<UnityCardRow>();
           Rows.Add(unityRow);
        }

        [Button("刷新行")]
        public override void RefreshRows()
        {
           Rows = RefreshRows<UnityCardRow>();
        }

        public override void RemoveByUuid(string uuid)
        {
           var unityRow = GetUnityRowByUuid(uuid) as UnityCardRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
        }
#endif
    }
}

