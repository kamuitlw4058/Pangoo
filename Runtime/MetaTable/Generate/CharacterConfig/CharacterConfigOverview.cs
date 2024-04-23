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
        [CreateAssetMenu(fileName = "CharacterConfigOverview", menuName = "MetaTable/CharacterConfigOverview")]
    public partial class CharacterConfigOverview : MetaTableOverview
    {


        [TableList(AlwaysExpanded = true)]
        public List<UnityCharacterConfigRow> Rows = new();

        public override string TableName => "CharacterConfig";

         public override IReadOnlyList<MetaTableUnityRow> UnityBaseRows => Rows;

        public override MetaTableBase ToTable()
        {
           return ToTable<CharacterConfigTable>();
        }
#if UNITY_EDITOR

        public static IEnumerable GetUuidDropdown()
        {
           return GetUuidDropdown<CharacterConfigOverview>();
        }

        public static IEnumerable GetUuidDropdown(List<Tuple<string, string>> AdditionalOptions = null)
        {
           return GetUuidDropdown<CharacterConfigOverview>(AdditionalOptions:AdditionalOptions);
        }

        public static IEnumerable GetUuidDropdown(List<string> excludeUuids = null, string packageDir = null, List<Tuple<string, string>> AdditionalOptions = null, List<Tuple<string, string>> includeUuids = null)
        {
           return GetUuidDropdown<CharacterConfigOverview>(excludeUuids: excludeUuids, packageDir: packageDir,AdditionalOptions:AdditionalOptions,includeUuids:includeUuids);
        }

        public static UnityCharacterConfigRow GetUnityRowByUuid(string uuid, string packageDir = null)
        {
           return GetUnityRowByUuid<CharacterConfigOverview, UnityCharacterConfigRow>(uuid);
        }

        public static CharacterConfigOverview GetOverviewByUuid(string uuid, string packageDir = null)
        {
           return GetOverviewByUuid<CharacterConfigOverview>(uuid);
        }

        public override void AddRow(MetaTableUnityRow unityRow)
        {
           AddRow<UnityCharacterConfigRow>(unityRow);
           Rows.Add(unityRow as UnityCharacterConfigRow);
        }

        public override void AddBaseRow(MetaTableRow row)
        {
           var unityRow = ScriptableObject.CreateInstance<UnityCharacterConfigRow>();
           unityRow.Row = row as CharacterConfigRow;
           AddRow<UnityCharacterConfigRow>(unityRow);
           Rows.Add(unityRow);
        }

        public override void UpdateRow(string uuid, MetaTableRow baseRow)
        {
           foreach (var row in Rows)
            {
               if (row.Uuid.Equals(uuid))
                {
                   row.Row = baseRow as CharacterConfigRow;
                   row.Row.Uuid = uuid;
                   return;
                }
            }
        }

        [Button("添加行")]
        public void AddRow()
        {
           var unityRow = AddRow<UnityCharacterConfigRow>();
           Rows.Add(unityRow);
        }

        [Button("刷新行")]
        public override void RefreshRows()
        {
           Rows = RefreshRows<UnityCharacterConfigRow>();
        }

        public override void RemoveByUuid(string uuid)
        {
           var unityRow = GetUnityRowByUuid(uuid) as UnityCharacterConfigRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
        }
#endif
    }
}
