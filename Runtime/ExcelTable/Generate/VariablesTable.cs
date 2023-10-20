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
    public partial class VariablesTable : ExcelTableBase
    {
        [Serializable]
        public partial class VariablesRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("变量类型")]
            [HideLabel]
            [JsonMember("VariableType")]
            [ExcelTableCol("VariableType","VariableType","string", "变量类型",3)]
            public string VariableType ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("关键字")]
            [HideLabel]
            [JsonMember("Key")]
            [ExcelTableCol("Key","Key","string", "关键字",4)]
            public string Key ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("值类型")]
            [HideLabel]
            [JsonMember("ValueType")]
            [ExcelTableCol("ValueType","ValueType","string", "值类型",5)]
            public string ValueType ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("默认值")]
            [HideLabel]
            [JsonMember("DefaultValue")]
            [ExcelTableCol("DefaultValue","DefaultValue","string", "默认值",6)]
            public string DefaultValue ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("是否需要存档")]
            [HideLabel]
            [JsonMember("NeedSave")]
            [ExcelTableCol("NeedSave","NeedSave","bool", "是否需要存档",7)]
            public bool NeedSave ;
        }


        [TableList]
        public List<VariablesRow> Rows = new();

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
        public Dictionary<int,VariablesRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as VariablesTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<VariablesRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as VariablesRow);
        }

        #endif

        public VariablesRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<VariablesRow>(row_id);
          #else
          VariablesRow row;
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
          Rows = LoadExcelFile<VariablesRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

