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
    public partial class TriggerEventTable : ExcelTableBase
    {
        [Serializable]
        public partial class TriggerEventRow : ExcelRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("Name")]
            [HideLabel]
            [JsonMember("Name")]
            [ExcelTableCol("Name","Name","string", "Name",2)]
            public string Name ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("触发类型")]
            [HideLabel]
            [JsonMember("TriggerType")]
            [ExcelTableCol("TriggerType","TriggerType","string", "触发类型",3)]
            public string TriggerType ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("指令列表")]
            [HideLabel]
            [JsonMember("InstructionList")]
            [ExcelTableCol("InstructionList","InstructionList","string", "指令列表",4)]
            public string InstructionList ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("触发器参数")]
            [HideLabel]
            [JsonMember("Params")]
            [ExcelTableCol("Params","Params","string", "触发器参数",5)]
            public string Params ;
        }


        [TableList]
        public List<TriggerEventRow> Rows = new();

        public override List<ExcelRowBase> BaseRows{
          get{
              List<ExcelRowBase> ret = new List<ExcelRowBase>();
              ret.AddRange(Rows);
              return ret;
          }
        }


        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,TriggerEventRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as TriggerEventTable;
          Rows.AddRange(table.Rows);
        }

        public TriggerEventRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<TriggerEventRow>(row_id);
          #else
          TriggerEventRow row;
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
          Rows = LoadExcelFile<TriggerEventRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

