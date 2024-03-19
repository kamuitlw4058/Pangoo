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
        [CreateAssetMenu(fileName = "SoundOverview", menuName = "MetaTable/SoundOverview")]
    public partial class SoundOverview : MetaTableOverview
    {


        [TableList(AlwaysExpanded = true)]
        public List<UnitySoundRow> Rows = new();

        public override string TableName => "Sound";

         public override IReadOnlyList<MetaTableUnityRow> UnityBaseRows => Rows;

        public override MetaTableBase ToTable()
        {
           return ToTable<SoundTable>();
        }
#if UNITY_EDITOR

        public static IEnumerable GetUuidDropdown()
        {
           return GetUuidDropdown<SoundOverview>();
        }

        public static IEnumerable GetUuidDropdown(List<string> excludeUuids = null, string packageDir = null, List<Tuple<string, string>> AdditionalOptions = null, List<Tuple<string, string>> includeUuids = null)
        {
           return GetUuidDropdown<SoundOverview>(excludeUuids: excludeUuids, packageDir: packageDir,AdditionalOptions:AdditionalOptions,includeUuids:includeUuids);
        }

        public static UnitySoundRow GetUnityRowByUuid(string uuid, string packageDir = null)
        {
           return GetUnityRowByUuid<SoundOverview, UnitySoundRow>(uuid);
        }

        public static SoundOverview GetOverviewByUuid(string uuid, string packageDir = null)
        {
           return GetOverviewByUuid<SoundOverview>(uuid);
        }

        public override void AddRow(MetaTableUnityRow unityRow)
        {
           AddRow<UnitySoundRow>(unityRow);
           Rows.Add(unityRow as UnitySoundRow);
        }

        public override void AddBaseRow(MetaTableRow row)
        {
           var unityRow = ScriptableObject.CreateInstance<UnitySoundRow>();
           unityRow.Row = row as SoundRow;
           AddRow<UnitySoundRow>(unityRow);
           Rows.Add(unityRow);
        }

        public override void UpdateRow(string uuid, MetaTableRow baseRow)
        {
           foreach (var row in Rows)
            {
               if (row.Uuid.Equals(uuid))
                {
                   row.Row = baseRow as SoundRow;
                   row.Row.Uuid = uuid;
                   return;
                }
            }
        }

        [Button("添加行")]
        public void AddRow()
        {
           var unityRow = AddRow<UnitySoundRow>();
           Rows.Add(unityRow);
        }

        [Button("刷新行")]
        public override void RefreshRows()
        {
           Rows = RefreshRows<UnitySoundRow>();
        }

        public override void RemoveByUuid(string uuid)
        {
           var unityRow = GetUnityRowByUuid(uuid) as UnitySoundRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
        }
#endif
    }
}

