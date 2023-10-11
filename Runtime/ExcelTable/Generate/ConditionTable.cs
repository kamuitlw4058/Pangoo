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
    public partial class ConditionTable : ExcelTableBase
    {
        [Serializable]
        public partial class ConditionRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("条件类型")]
            [HideLabel]
            [JsonMember("ConditionType")]
            [ExcelTableCol("ConditionType","ConditionType","string", "条件类型",3)]
            public string ConditionType ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("条件参数")]
            [HideLabel]
            [JsonMember("Params")]
            [ExcelTableCol("Params","Params","string", "条件参数",4)]
            public string Params ;
        }


        [TableList]
        public List<ConditionRow> Rows = new();

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
        public Dictionary<int,ConditionRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as ConditionTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<ConditionRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as ConditionRow);
        }

        #endif

        public ConditionRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<ConditionRow>(row_id);
          #else
          ConditionRow row;
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
          Rows = LoadExcelFile<ConditionRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

