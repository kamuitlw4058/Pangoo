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
    public partial class RuntimeDataTable : ExcelTableBase
    {
        [Serializable]
        public partial class RuntimeDataRow : ExcelRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("键")]
            [HideLabel]
            [JsonMember("Key")]
            [ExcelTableCol("Key","Key","string", "键",2)]
            public string Key ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("类型")]
            [HideLabel]
            [JsonMember("Type")]
            [ExcelTableCol("Type","Type","string", "类型",3)]
            public string Type ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("值")]
            [HideLabel]
            [JsonMember("Value")]
            [ExcelTableCol("Value","Value","string", "值",4)]
            public string Value ;
        }


        [TableList]
        public List<RuntimeDataRow> Rows = new();

        public override List<ExcelRowBase> BaseRows{
          get{
              List<ExcelRowBase> ret = new List<ExcelRowBase>();
              ret.AddRange(Rows);
              return ret;
          }
        }


        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,RuntimeDataRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as RuntimeDataTable;
          Rows.AddRange(table.Rows);
        }

        public RuntimeDataRow GetRowById(int row_id){
          RuntimeDataRow row;
          if(Dict.TryGetValue(row_id,out row)){
              return row;
          }
          return null;
         }

#if UNITY_EDITOR
        /// <summary> 从Excel文件重新构建数据 </summary>
        public virtual void LoadExcelFile(string excelFilePath)
        {
          Rows = LoadExcelFile<RuntimeDataRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

