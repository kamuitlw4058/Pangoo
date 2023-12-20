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
        [CreateAssetMenu(fileName = "InstructionOverview", menuName = "MetaTable/InstructionOverview")]
    public partial class InstructionOverview : MetaTableOverview
    {


        [TableList(AlwaysExpanded = true)]
        public List<UnityInstructionRow> Rows = new();

        public override string TableName => "Instruction";

         public override IReadOnlyList<MetaTableUnityRow> UnityBaseRows => Rows;

        public override MetaTableBase ToTable()
        {
           return ToTable<InstructionTable>();
        }
#if UNITY_EDITOR

        public static UnityInstructionRow GetUnityRowById(int id, string packageDir = null)
        {
           var overviews = AssetDatabaseUtility.FindAsset<InstructionOverview>(packageDir);
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
           var overviews = AssetDatabaseUtility.FindAsset<InstructionOverview>(packageDir);
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

        public static IEnumerable GetUuidDropdown(List<string> excludeUuids = null, string packageDir = null)
        {
           return GetUuidDropdown<InstructionOverview>(excludeUuids: excludeUuids, packageDir: packageDir);
        }

        public static UnityInstructionRow GetUnityRowByUuid(string uuid, string packageDir = null)
        {
           return GetUnityRowByUuid<InstructionOverview, UnityInstructionRow>(uuid);
        }

        public override void AddRow(MetaTableUnityRow unityRow)
        {
           AddRow<UnityInstructionRow>(unityRow);
           Rows.Add(unityRow as UnityInstructionRow);
        }

        public override void AddBaseRow(MetaTableRow row)
        {
           var unityRow = ScriptableObject.CreateInstance<UnityInstructionRow>();
           unityRow.Row = row as InstructionRow;
           AddRow<UnityInstructionRow>(unityRow);
           Rows.Add(unityRow);
        }

        public override void UpdateRow(string uuid, MetaTableRow baseRow)
        {
           foreach (var row in Rows)
            {
               if (row.Uuid.Equals(uuid))
                {
                   row.Row = baseRow as InstructionRow;
                   row.Row.Uuid = uuid;
                   return;
                }
            }
        }

        [Button("添加行")]
        public void AddRow()
        {
           var unityRow = AddRow<UnityInstructionRow>();
           Rows.Add(unityRow);
        }

        [Button("刷新行")]
        public override void RefreshRows()
        {
           Rows = RefreshRows<UnityInstructionRow>();
        }

        public override void RemoveByUuid(string uuid)
        {
           var unityRow = GetUnityRowByUuid(uuid) as UnityInstructionRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
        }
#endif
    }
}

