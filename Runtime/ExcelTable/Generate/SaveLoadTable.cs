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
    public partial class SaveLoadTable : ExcelTableBase
    {
        [Serializable]
        public partial class SaveLoadRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("Int类型的键")]
            [HideLabel]
            [JsonMember("int_key")]
            [ExcelTableCol("IntKey","int_key","string", "Int类型的键",3)]
            public string IntKey ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("Int类型的值")]
            [HideLabel]
            [JsonMember("int_value")]
            [ExcelTableCol("IntValue","int_value","int", "Int类型的值",4)]
            public int IntValue ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("Flaot类型的键")]
            [HideLabel]
            [JsonMember("float_key")]
            [ExcelTableCol("FloatKey","float_key","string", "Flaot类型的键",5)]
            public string FloatKey ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("Float类型的值")]
            [HideLabel]
            [JsonMember("float_value")]
            [ExcelTableCol("FloatValue","float_value","float", "Float类型的值",6)]
            public float FloatValue ;
        }


        [TableList]
        public List<SaveLoadRow> Rows = new();

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
        public Dictionary<int,SaveLoadRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as SaveLoadTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<SaveLoadRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as SaveLoadRow);
        }

        #endif

        public SaveLoadRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<SaveLoadRow>(row_id);
          #else
          SaveLoadRow row;
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
          Rows = LoadExcelFile<SaveLoadRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

