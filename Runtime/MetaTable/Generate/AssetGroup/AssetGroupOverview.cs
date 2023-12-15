// 本文件使用工具自动生成，请勿进行手动修改！

using System;
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

         public override void RemoveRow(string uuid)
        {
           var unityRow = GetUnityRowByName(uuid) as UnityAssetGroupRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
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

