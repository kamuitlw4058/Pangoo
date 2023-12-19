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
        [CreateAssetMenu(fileName = "VariablesOverview", menuName = "MetaTable/VariablesOverview")]
    public partial class VariablesOverview : MetaTableOverview
    {


        [TableList(AlwaysExpanded = true)]
        public List<UnityVariablesRow> Rows = new();

        public override string TableName => "Variables";

         public override IReadOnlyList<MetaTableUnityRow> UnityBaseRows => Rows;

        public override MetaTableBase ToTable()
        {
           return ToTable<VariablesTable>();
        }
#if UNITY_EDITOR

        public static UnityVariablesRow GetUnityRowById(int id, string packageDir = null)
        {
           var overviews = AssetDatabaseUtility.FindAsset<VariablesOverview>(packageDir);
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
           var overviews = AssetDatabaseUtility.FindAsset<VariablesOverview>(packageDir);
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
           return GetUuidDropdown<VariablesOverview>(excludeUuids: excludeUuids, packageDir: packageDir);
        }

        public static UnityVariablesRow GetUnityRowByUuid(string uuid, string packageDir = null)
        {
           return GetUnityRowByUuid<VariablesOverview, UnityVariablesRow>(uuid);
        }

         public override void RemoveRow(string uuid)
        {
           var unityRow = GetUnityRowByName(uuid) as UnityVariablesRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
        }

        public override void AddRow(MetaTableUnityRow unityRow)
        {
           AddRow<UnityVariablesRow>(unityRow);
           Rows.Add(unityRow as UnityVariablesRow);
        }

        public override void AddBaseRow(MetaTableRow row)
        {
           var unityRow = ScriptableObject.CreateInstance<UnityVariablesRow>();
           unityRow.Row = row as VariablesRow;
           AddRow<UnityVariablesRow>(unityRow);
           Rows.Add(unityRow);
        }

        public override void UpdateRow(string uuid, MetaTableRow baseRow)
        {
           foreach (var row in Rows)
            {
               if (row.Uuid.Equals(uuid))
                {
                   row.Row = baseRow as VariablesRow;
                   row.Row.Uuid = uuid;
                   return;
                }
            }
        }

        [Button("添加行")]
        public void AddRow()
        {
           var unityRow = AddRow<UnityVariablesRow>();
           Rows.Add(unityRow);
        }

        [Button("刷新行")]
        public override void RefreshRows()
        {
           Rows = RefreshRows<UnityVariablesRow>();
        }

        public override void RemoveByUuid(string uuid)
        {
           for (int i = 0; i < Rows.Count; i++)
           {
               if (Rows[i].Row.Uuid.Equals(uuid)){
                   Rows.Remove(Rows[i]);
               }
           }
        }
#endif
    }
}

