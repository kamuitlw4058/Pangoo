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

        #if UNITY_EDITOR
        public  override void RemoveId(int Id){
          var row = GetRowById<InstructionRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }
        #endif

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

