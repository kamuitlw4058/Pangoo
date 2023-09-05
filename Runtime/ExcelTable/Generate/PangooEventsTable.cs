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
    public partial class PangooEventsTable : ExcelTableBase
    {
        [Serializable]
        public partial class PangooEventsRow : ExcelRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("事件的命名空间")]
            [HideLabel]
            [JsonMember("Namesapce")]
            [ExcelTableCol("Namesapce","Namesapce","string", "事件的命名空间",2)]
            public string Namesapce ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("事件名称")]
            [HideLabel]
            [JsonMember("EventName")]
            [ExcelTableCol("EventName","EventName","string", "事件名称",3)]
            public string EventName ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("描述")]
            [HideLabel]
            [JsonMember("desc")]
            [ExcelTableCol("Desc","desc","string", "描述",4)]
            public string Desc ;
        }


        [TableList]
        public List<PangooEventsRow> Rows = new();

        public override IReadOnlyList<ExcelRowBase> BaseRows{
          get{
              return Rows;
          }
        }


        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,PangooEventsRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as PangooEventsTable;
          Rows.AddRange(table.Rows);
        }

        public PangooEventsRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<PangooEventsRow>(row_id);
          #else
          PangooEventsRow row;
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
          Rows = LoadExcelFile<PangooEventsRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

