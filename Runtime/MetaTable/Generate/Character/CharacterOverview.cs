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
        [CreateAssetMenu(fileName = "CharacterOverview", menuName = "MetaTable/CharacterOverview")]
    public partial class CharacterOverview : MetaTableOverview
    {


        [TableList(AlwaysExpanded = true)]
        public List<UnityCharacterRow> Rows = new();

        public override string TableName => "Character";

         public override IReadOnlyList<MetaTableUnityRow> UnityBaseRows => Rows;

        public override MetaTableBase ToTable()
        {
           return ToTable<CharacterTable>();
        }
#if UNITY_EDITOR

        public static UnityCharacterRow GetUnityRowById(int id, string packageDir = null)
        {
           var overviews = AssetDatabaseUtility.FindAsset<CharacterOverview>(packageDir);
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
           var overviews = AssetDatabaseUtility.FindAsset<CharacterOverview>(packageDir);
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
           return GetUuidDropdown<CharacterOverview>(excludeUuids: excludeUuids, packageDir: packageDir,AdditionalOptions:AdditionalOptions,includeUuids:includeUuids);
        }

        public static UnityCharacterRow GetUnityRowByUuid(string uuid, string packageDir = null)
        {
           return GetUnityRowByUuid<CharacterOverview, UnityCharacterRow>(uuid);
        }

        public static CharacterOverview GetOverviewByUuid(string uuid, string packageDir = null)
        {
           return GetOverviewByUuid<CharacterOverview>(uuid);
        }

        public override void AddRow(MetaTableUnityRow unityRow)
        {
           AddRow<UnityCharacterRow>(unityRow);
           Rows.Add(unityRow as UnityCharacterRow);
        }

        public override void AddBaseRow(MetaTableRow row)
        {
           var unityRow = ScriptableObject.CreateInstance<UnityCharacterRow>();
           unityRow.Row = row as CharacterRow;
           AddRow<UnityCharacterRow>(unityRow);
           Rows.Add(unityRow);
        }

        public override void UpdateRow(string uuid, MetaTableRow baseRow)
        {
           foreach (var row in Rows)
            {
               if (row.Uuid.Equals(uuid))
                {
                   row.Row = baseRow as CharacterRow;
                   row.Row.Uuid = uuid;
                   return;
                }
            }
        }

        [Button("添加行")]
        public void AddRow()
        {
           var unityRow = AddRow<UnityCharacterRow>();
           Rows.Add(unityRow);
        }

        [Button("刷新行")]
        public override void RefreshRows()
        {
           Rows = RefreshRows<UnityCharacterRow>();
        }

        public override void RemoveByUuid(string uuid)
        {
           var unityRow = GetUnityRowByUuid(uuid) as UnityCharacterRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
        }
#endif
    }
}

