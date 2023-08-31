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
    public partial class InstructionTable : ExcelTableBase
    {
        [Serializable]
        public partial class InstructionRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("指令类型")]
            [HideLabel]
            [JsonMember("InstructionType")]
            [ExcelTableCol("InstructionType","InstructionType","string", "指令类型",3)]
            public string InstructionType ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("指令参数")]
            [HideLabel]
            [JsonMember("Params")]
            [ExcelTableCol("Params","Params","string", "指令参数",4)]
            public string Params ;
        }


        [TableList]
        public List<InstructionRow> Rows = new();

        public override List<ExcelRowBase> BaseRows{
          get{
              List<ExcelRowBase> ret = new List<ExcelRowBase>();
              ret.AddRange(Rows);
              return ret;
          }
        }

        public override List<ExcelNamedRowBase> NamedBaseRows{
          get{
              List<ExcelNamedRowBase> ret = new List<ExcelNamedRowBase>();
              ret.AddRange(Rows);
              return ret;
          }
        }

        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,InstructionRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as InstructionTable;
          Rows.AddRange(table.Rows);
        }

        public InstructionRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<InstructionRow>(row_id);
          #else
          InstructionRow row;
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
          Rows = LoadExcelFile<InstructionRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

