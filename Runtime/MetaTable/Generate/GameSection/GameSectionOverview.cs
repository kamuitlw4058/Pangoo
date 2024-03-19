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
        [CreateAssetMenu(fileName = "GameSectionOverview", menuName = "MetaTable/GameSectionOverview")]
    public partial class GameSectionOverview : MetaTableOverview
    {


        [TableList(AlwaysExpanded = true)]
        public List<UnityGameSectionRow> Rows = new();

        public override string TableName => "GameSection";

         public override IReadOnlyList<MetaTableUnityRow> UnityBaseRows => Rows;

        public override MetaTableBase ToTable()
        {
           return ToTable<GameSectionTable>();
        }
#if UNITY_EDITOR

        public static IEnumerable GetUuidDropdown()
        {
           return GetUuidDropdown<GameSectionOverview>();
        }

        public static IEnumerable GetUuidDropdown(List<string> excludeUuids = null, string packageDir = null, List<Tuple<string, string>> AdditionalOptions = null, List<Tuple<string, string>> includeUuids = null)
        {
           return GetUuidDropdown<GameSectionOverview>(excludeUuids: excludeUuids, packageDir: packageDir,AdditionalOptions:AdditionalOptions,includeUuids:includeUuids);
        }

        public static UnityGameSectionRow GetUnityRowByUuid(string uuid, string packageDir = null)
        {
           return GetUnityRowByUuid<GameSectionOverview, UnityGameSectionRow>(uuid);
        }

        public static GameSectionOverview GetOverviewByUuid(string uuid, string packageDir = null)
        {
           return GetOverviewByUuid<GameSectionOverview>(uuid);
        }

        public override void AddRow(MetaTableUnityRow unityRow)
        {
           AddRow<UnityGameSectionRow>(unityRow);
           Rows.Add(unityRow as UnityGameSectionRow);
        }

        public override void AddBaseRow(MetaTableRow row)
        {
           var unityRow = ScriptableObject.CreateInstance<UnityGameSectionRow>();
           unityRow.Row = row as GameSectionRow;
           AddRow<UnityGameSectionRow>(unityRow);
           Rows.Add(unityRow);
        }

        public override void UpdateRow(string uuid, MetaTableRow baseRow)
        {
           foreach (var row in Rows)
            {
               if (row.Uuid.Equals(uuid))
                {
                   row.Row = baseRow as GameSectionRow;
                   row.Row.Uuid = uuid;
                   return;
                }
            }
        }

        [Button("添加行")]
        public void AddRow()
        {
           var unityRow = AddRow<UnityGameSectionRow>();
           Rows.Add(unityRow);
        }

        [Button("刷新行")]
        public override void RefreshRows()
        {
           Rows = RefreshRows<UnityGameSectionRow>();
        }

        public override void RemoveByUuid(string uuid)
        {
           var unityRow = GetUnityRowByUuid(uuid) as UnityGameSectionRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
        }
#endif
    }
}

