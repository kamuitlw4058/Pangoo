// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo;

namespace Pangoo
{
    [Serializable]
    public partial class ItemsConfigTable : ExcelTableBase
    {
        [Serializable]
        public partial class ItemsConfigRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("编号")]
            [HideLabel]
            [JsonMember("ID")]
            [ExcelTableCol("ID","ID","int", "编号",3)]
            public int ID ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("能否拾取")]
            [HideLabel]
            [JsonMember("CanPickup")]
            [ExcelTableCol("CanPickup","CanPickup","bool", "能否拾取",4)]
            public bool CanPickup ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("位置坐标")]
            [HideLabel]
            [JsonMember("Pos")]
            [ExcelTableCol("Pos","Pos","Vector3", "位置坐标",5)]
            public Vector3 Pos ;
        }


        [TableList]
        public List<ItemsConfigRow> Rows = new();

        public override IReadOnlyList<ExcelRowBase> BaseRows{
          get{
              return Rows;
          }
        }

        public override IReadOnlyList<ExcelNamedRowBase> NamedBaseRows{
          get{
              return Rows;
          }
        }

        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,ItemsConfigRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as ItemsConfigTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<ItemsConfigRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as ItemsConfigRow);
        }

        #endif

        public ItemsConfigRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<ItemsConfigRow>(row_id);
          #else
          ItemsConfigRow row;
          if(Dict.TryGetValue(row_id,out row)){
              return row;
          }
          return null;
          #endif
         }

#if UNITY_EDITOR
        /// <summary> 从Excel文件重新构建数据 </summary>
        public virtual void LoadExcelFile(string excelFilePath)
        {
          Rows = LoadExcelFile<ItemsConfigRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

