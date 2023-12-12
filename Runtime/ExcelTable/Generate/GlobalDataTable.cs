// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.Common;

namespace Pangoo
{
    [Serializable]
    public partial class GlobalDataTable : ExcelTableBase
    {
        [Serializable]
        public partial class GlobalDataRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("键")]
            [HideLabel]
            [JsonMember("Key")]
            [ExcelTableCol("Key","Key","string", "键",3)]
            public string Key ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("类型")]
            [HideLabel]
            [JsonMember("Type")]
            [ExcelTableCol("Type","Type","string", "类型",4)]
            public string Type ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("值")]
            [HideLabel]
            [JsonMember("Value")]
            [ExcelTableCol("Value","Value","string", "值",5)]
            public string Value ;
        }


        [TableList]
        public List<GlobalDataRow> Rows = new();

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
        public Dictionary<int,GlobalDataRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as GlobalDataTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<GlobalDataRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as GlobalDataRow);
        }

        #endif

        public GlobalDataRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<GlobalDataRow>(row_id);
          #else
          GlobalDataRow row;
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
          Rows = LoadExcelFile<GlobalDataRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

