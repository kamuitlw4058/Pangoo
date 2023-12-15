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

         public override void RemoveRow(string uuid)
        {
           var unityRow = GetUnityRowByName(uuid) as UnitySoundRow;
            if(unityRow != null)
            {
                 Rows.Remove(unityRow);
                 AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(unityRow));
            }
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

